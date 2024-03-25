using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodaGigante : MonoBehaviour
{
    public float rotationSpeed = 10.0f;
    public Transform[] seats;

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0f); //Rotaciona a Montanha Russa

        foreach (Transform seat in seats) //Rotaciona os bancos em relação a Montanha Russa
        {
            Quaternion desiredRotation = Quaternion.Euler(-90, transform.eulerAngles.y, 50);
            seat.rotation = desiredRotation;
            seat.Rotate(0, 0, transform.eulerAngles.y);
        }
    }
}
