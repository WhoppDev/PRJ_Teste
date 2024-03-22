using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrFam_Manager : MonoBehaviour
{
    public Transform originalPosition;
    public GameObject NPC;

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC_DrFAM"))
        {
            for (int i = 0; i < DrFam_CORE.instance.sitSpace.Length; i++)
            {
                if (!DrFam_CORE.instance.chairOccupied[i])
                {
                    NPC = other.gameObject;
                    DrFam_CORE.instance.chairOccupied[i] = true;

                    other.GetComponent<NPCController_DrFam>().agent.enabled = false;
                    other.GetComponent<NPCController_DrFam>().anim.SetBool("isSitting", true);
                    other.GetComponent<NPCController_DrFam>().agent.velocity = Vector3.zero;
                    other.transform.rotation = Quaternion.Euler(0f, DrFam_CORE.instance.sitSpace[i].transform.eulerAngles.y, 0f);

                    other.transform.position = DrFam_CORE.instance.sitSpace[i].transform.position;

                    other.GetComponent<NPCController_DrFam>().occupiedChairIndex = i;

                    break;
                }
                else if (i == DrFam_CORE.instance.sitSpace.Length - 1)
                {
                    StartCoroutine(Destroy());
                }
            }
        }
    }*/

    /*public void LiberarCama(GameObject npc)
    {
        for (int i = 0; i < DrFam_CORE.instance.camasDisponiveis.Length; i++)
        {
                DrFam_CORE.instance.camasOcupadas[i] = false;
                break;
        }
    }*/



    public IEnumerator Destroy()
    {
        DrFam_CORE.instance.angryPoint++;
        DrFam_CORE.instance.AtualizarHud();
        yield return new WaitForSeconds(10);
        Destroy(NPC);
    }
}
