using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SireneScript : MonoBehaviour
{
    public GameObject luzVermelha, luzAzul, luzVermelha2;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Light());   
    }

    public IEnumerator Light()
    {
        yield return new WaitForSeconds(time);
        luzVermelha.SetActive(true);
        luzVermelha2.SetActive(true);
        luzAzul.SetActive(false);

        yield return new WaitForSeconds(time);
        luzVermelha.SetActive(false);
        luzVermelha2.SetActive(false);
        luzAzul.SetActive(true);
        StartCoroutine(Light());
    }
}
