using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // Velocidad de rotación en grados por segundo

    void Update()
    {
        // Rotar el objeto alrededor del eje Y a la velocidad especificada
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
