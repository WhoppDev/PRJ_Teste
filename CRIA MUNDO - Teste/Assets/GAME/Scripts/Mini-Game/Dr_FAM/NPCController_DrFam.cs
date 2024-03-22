using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCController_DrFam : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;
    public Transform target;
    public Vector3 inicialPosition; //Para o NPC retornar ao ponto inicial apos medicado
    public SpriteRenderer spriteRenderer; 
    public MedicamentoDATA[] medicamentos;
    public SpriteRenderer medicamentoSprite;
    public ParticleSystem happy;

    public float timeToTake = 5f;
    public float transitionDuration = 10f;

    public float atendimentoRange;
    public LayerMask playerLayer;

    private Coroutine courotine;
    public int occupiedChairIndex = -1;

    public MedicamentoDATA medicamentoDesejado;
    public PlayerDrFam_Controller player;
    public bool playerDeitado = false;
    public bool playerSentado = false;



    void Start()
    {
        target = GameObject.FindGameObjectWithTag("target_DrFAM").transform;
        inicialPosition = transform.position;
    }

    private void Update()
    {
        if (!playerSentado)
        {
            float distance = Vector3.Distance(this.transform.position, target.transform.position);
            if (distance <= 10)
            {
                for (int i = 0; i < DrFam_CORE.instance.sitSpace.Length; i++)
                {
                    if (!DrFam_CORE.instance.chairOccupied[i])
                    {
                        DrFam_CORE.instance.chairOccupied[i] = true;

                        agent.enabled = false;
                        anim.SetBool("isSitting", true);
                        agent.velocity = Vector3.zero;
                        this.transform.rotation = Quaternion.Euler(0f, DrFam_CORE.instance.sitSpace[i].transform.eulerAngles.y, 0f);

                        this.transform.position = DrFam_CORE.instance.sitSpace[i].transform.position;

                        occupiedChairIndex = i;

                        break;
                    }
                    else if (i == DrFam_CORE.instance.sitSpace.Length - 1)
                    {
                        Destroy();
                    }
                }
                playerSentado = true;
            }
        }
    }


    private void FixedUpdate()
    {
        SeeThePlayer();
    }

    public void MoveToTarget()
    {
        if (agent != null && target != null)
        {
            agent.SetDestination(target.position);
        }

        
    }

    public void Destroy()
    {
        DrFam_CORE.instance.angryPoint++;
        DrFam_CORE.instance.AtualizarHud();
        Destroy(this.gameObject);
    }

    void SeeThePlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, atendimentoRange, playerLayer);
        if (hits.Length >= 1 && courotine == null)
        {
            player = hits[0].GetComponent<PlayerDrFam_Controller>();
            courotine = StartCoroutine(StartTimer());
        }
        else if (hits.Length <= 0 && courotine != null)
        {
            StopCoroutine(courotine);
            courotine = null;
        }

        if (!agent.isOnNavMesh)
        {
            TeleportToNearestNavMesh();
        }

    }

    private void TeleportToNearestNavMesh()
    {
        Vector3 validPosition = FindNearestNavMeshPoint(transform.position);
        transform.position = validPosition;
    }

    private Vector3 FindNearestNavMeshPoint(Vector3 position)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, 100f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            return position;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, atendimentoRange);
    }

    private IEnumerator StartTimer()
    {
        float elapsedTime = 0f;

        if (playerDeitado)
        {
            if(player.medicamentosEscolhido == medicamentoDesejado)
            {
                while (elapsedTime < transitionDuration)
                {
                    float newValue = Mathf.Lerp(0f, 3.17f, elapsedTime / transitionDuration);
                    spriteRenderer.material.SetFloat("_RemovedSegment", newValue);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                this.agent.enabled = true;
                this.anim.SetBool("isSitting", false);
                this.anim.SetBool("isLaying", false);
                this.agent.velocity = Vector3.zero;
                happy.Play();

                agent.SetDestination(inicialPosition);

                Destroy(player.medicamenoObj);
                LiberarCama();
                DrFam_CORE.instance.happyPoint++;
                DrFam_CORE.instance.AtualizarHud();
                player.medicamentosEscolhido = null;
            }
        }
        else
        {
            while (elapsedTime < transitionDuration)
            {
                float newValue = Mathf.Lerp(0f, 3.17f, elapsedTime / transitionDuration);
                spriteRenderer.material.SetFloat("_RemovedSegment", newValue);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            DeitarNaCama();
        }
    }



    void DeitarNaCama()
    {
        for (int i = 0; i < DrFam_CORE.instance.camasDisponiveis.Length; i++)
        {
            if (!DrFam_CORE.instance.camasOcupadas[i])
            {
                DrFam_CORE.instance.camasOcupadas[i] = true;

                this.agent.enabled = false;
                this.anim.SetBool("isSitting", false);
                this.anim.SetBool("isLaying", true);
                this.agent.velocity = Vector3.zero;
                this.transform.rotation = Quaternion.Euler(0f, DrFam_CORE.instance.camasDisponiveis[i].transform.eulerAngles.y, 0f);

                this.transform.position = DrFam_CORE.instance.camasDisponiveis[i].transform.position;
                playerDeitado = true;
                SelectMedicine();

                if (occupiedChairIndex != -1)
                {
                    DrFam_CORE.instance.chairOccupied[occupiedChairIndex] = false;
                    occupiedChairIndex = -1;
                }

                break;
            }
        }
    }


    public void LiberarCama()
    {
        for (int i = 0; i < DrFam_CORE.instance.camasDisponiveis.Length; i++)
        {
            DrFam_CORE.instance.camasOcupadas[i] = false;
            break;
        }
    }

    void SelectMedicine()
        {
            int randomMedicine = Random.Range(0, medicamentos.Length);
            medicamentoSprite.sprite = medicamentos[randomMedicine].medicamentoImg;
            if (medicamentoDesejado == null)
            {
                medicamentoDesejado = medicamentos[randomMedicine];
            }
        }
}