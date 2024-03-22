using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrFam_Controller : MonoBehaviour
{
    public Transform spawnPointMedicamento;

    public MedicamentoDATA medicamentosEscolhido;
    public GameObject medicamenoObj;


    public void PegarObjeto(MedicamentoDATA medicamentoSelecionado)
    {
        GameObject newObject = Instantiate(medicamentoSelecionado.medicamentoObj, spawnPointMedicamento.position, Quaternion.identity);
        newObject.transform.parent = spawnPointMedicamento;
        medicamenoObj = newObject;
        medicamentosEscolhido = medicamentoSelecionado;
    }
}
