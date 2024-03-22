using System.Collections;
using UnityEngine;

public class MedicamentoDrFam : MonoBehaviour
{
    public float atendimentoRange; //Range do raio de colis�o do player com o balc�o de medicamento
    public LayerMask playerLayer;
    public MedicamentoDATA medicamento;
    public PlayerDrFam_Controller player;
    public SpriteRenderer spriteRenderer;
    public float transitionDuration = 10f;

    private Coroutine courotine;

    private void FixedUpdate()
    {
        SeeThePlayer();
    }

    private void SeeThePlayer()
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
    }

    private IEnumerator StartTimer()
    {
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            float newValue = Mathf.Lerp(0f, 3.17f, elapsedTime / transitionDuration);
            spriteRenderer.material.SetFloat("_RemovedSegment", newValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.material.SetFloat("_RemovedSegment", 3.17f);

        if(player.medicamentosEscolhido != null)
            {
            Destroy(player.medicamenoObj);
            player.medicamentosEscolhido = null;
            }
        player.PegarObjeto(medicamento);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, atendimentoRange);
    }
}
