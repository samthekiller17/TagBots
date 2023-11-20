using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManejoDañoJugador : MonoBehaviour
{
    public float dañoBase = 10f;
    public float aumentoDaño = 5f;
    public float duracionAumento = 5f;
    private float dañoActual;

    private bool aumentoActivo = false;
    private float tiempoInicioAumento;

    public GameObject spritePowerUp; // Objeto GameObject del sprite del power-up
    public float tiempoMostrarSprite = 2f; // Tiempo que se mostrará el sprite en pantalla
    private bool mostrarSprite = false;
    private float tiempoMostrandoSprite;

    void Start()
    {
        dañoActual = dañoBase;
    }

    public void RecibirDaño()
    {
        Debug.Log("El jugador ha recibido " + dañoActual + " puntos de daño.");
    }

    public void ActivarAumentoTemporal()
    {
        aumentoActivo = true;
        tiempoInicioAumento = Time.time;
        dañoActual += aumentoDaño;
        Debug.Log("¡El jugador ha activado un aumento temporal de daño!");

        // Mostrar el sprite del power-up
        mostrarSprite = true;
        tiempoMostrandoSprite = Time.time;
        spritePowerUp.SetActive(true);
    }

    void Update()
    {
        if (aumentoActivo && Time.time - tiempoInicioAumento >= duracionAumento)
        {
            aumentoActivo = false;
            dañoActual -= aumentoDaño;
            Debug.Log("El aumento temporal de daño ha terminado.");
        }

        // Lógica para mostrar el sprite del power-up por un tiempo determinado
        if (mostrarSprite && Time.time - tiempoMostrandoSprite >= tiempoMostrarSprite)
        {
            mostrarSprite = false;
            spritePowerUp.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            ActivarAumentoTemporal();
            Destroy(other.gameObject);
        }
    }
}



