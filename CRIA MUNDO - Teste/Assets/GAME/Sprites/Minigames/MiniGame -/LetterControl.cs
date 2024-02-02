using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LetterControl : MonoBehaviour
{
    public TextMeshProUGUI letterText; // Letra
    public Button letterButton; // Bot�o da letra

    private WordGameManager wordGameManager;

    private void OnEnable()
    {
        wordGameManager = FindObjectOfType<WordGameManager>();
        letterButton.onClick.AddListener(() => LetterClick());
    }

    public void LetterClick()
    {
        wordGameManager.word.text += letterText.text;
      //  wordGameManager.clicks++;
    }


}
