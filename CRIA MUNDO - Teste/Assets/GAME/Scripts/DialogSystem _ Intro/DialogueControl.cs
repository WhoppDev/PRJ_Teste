using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueControl : MonoBehaviour
{

    [Header("Components")]
    public GameObject dialogueObj; //Janela do di�logo
    public TextMeshProUGUI dialogueText; // Texto do di�logo
    public TMP_Text speechText; // Texto de quem est� falando
    public TMP_Text actorName; // Texto de quem est� falando

    [Header("Settings")]
    public float typingSpeed; // Velocidade emq ue vai aparecer as letras

    //Variaveis de Controle
    private bool isShowing; // Verifica se a janela de di�logo est� aberta
    private int index; // Index do texto
    private string[] sentences; // Frases do di�logo

    public static DialogueControl instance;


    private void Awake()
    {
        instance = this;
    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence() 
    { 
    
    }   

    public void Speech(string[] txt)
    {
        if(!isShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            StartCoroutine(TypeSentence());
            isShowing = true;
        }
    }
}
