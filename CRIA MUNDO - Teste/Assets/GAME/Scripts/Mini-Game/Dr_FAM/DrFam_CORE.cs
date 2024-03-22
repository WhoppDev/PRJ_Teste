using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrFam_CORE : MonoBehaviour
{
    public bool startDrFamMiniGame = false;
    public DrFam_Manager drFamMananager;

    public static DrFam_CORE instance;
    public GameObject[] sitSpace;
    public bool[] chairOccupied;

    public GameObject[] camasDisponiveis;
    public bool[] camasOcupadas;

    public int happyPoint;
    public int angryPoint;
    public TMP_Text happyPointTxt;
    public TMP_Text angryPointTxt;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        chairOccupied = new bool[sitSpace.Length];
        camasOcupadas = new bool[camasDisponiveis.Length];
    }

    public void AtualizarHud()
    {
        happyPointTxt.text = happyPoint.ToString();
        angryPointTxt.text = angryPoint.ToString();
    }
}
