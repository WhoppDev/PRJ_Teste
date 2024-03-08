using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrFam_ProgressBar : MonoBehaviour
{
    public float totalTime = 5f; // Tempo total de preenchimento da barrinha em segundos
    private float currentTime = 0f; // Tempo atual
    public Transform maskTransform; // Transform da m�scara que cobre o sprite original

    void Update()
    {
        // Se o tempo atual for menor que o tempo total, atualiza o preenchimento da m�scara
        if (currentTime < totalTime)
        {
            currentTime += Time.deltaTime; // Incrementa o tempo atual
            float fillAmount = currentTime / totalTime; // Calcula a propor��o preenchida da barrinha
            maskTransform.localScale = new Vector3(fillAmount, 1f, 1f); // Define a escala da m�scara para controlar o preenchimento
        }
        else
        {
            // Se o tempo acabou, pode-se executar a l�gica necess�ria, como chamar um m�todo para encerrar a contagem ou realizar outra a��o
            Debug.Log("Tempo acabou!");
        }
    }
}
