using Firebase.Auth;
using Firebase.Database;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomType = Customize.CustomType;

public class ClouthColorChange : MonoBehaviourPunCallbacks
{
    [SerializeField] Slider rgbSlider;
    [SerializeField] Color selectedColor;

    [SerializeField] Customize customize;
    public SkinnedMeshRenderer customSelect;

    public void RGBSlider()
    {
        if (customize == null)
        {
            Debug.LogError("Customize não encontrado neste objeto.");
            return;
        }

        // Obtendo o SkinnedMeshRenderer do objeto selecionado no Customize
        SkinnedMeshRenderer selectedRenderer = customize.GetSelectedObjectSkinnedMeshRenderer();
        if (selectedRenderer == null)
        {
            Debug.LogError("SkinnedMeshRenderer não encontrado no objeto selecionado no Customize.");
            return;
        }

        // Calculando a cor
        var hue = rgbSlider.value;
        selectedColor = Color.HSVToRGB(hue, 1f, 1f);

        // Obtendo o material do SkinnedMeshRenderer
        Material rendererMaterial = selectedRenderer.material;

        // Aplicando a cor ao material
        rendererMaterial.color = selectedColor;

        // Convertendo a cor para hexadecimal
        string hexColor = ColorUtility.ToHtmlStringRGB(selectedColor);

        // Exibindo a cor hexadecimal no console
        Debug.Log("Cor em hexadecimal: #" + hexColor);
    }

}
