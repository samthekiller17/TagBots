using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float CantidadDa�o;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<Codigo_Salud>())
        {
            other.GetComponent<Codigo_Salud>().RecibirDa�o(CantidadDa�o);
        }
    }
}
