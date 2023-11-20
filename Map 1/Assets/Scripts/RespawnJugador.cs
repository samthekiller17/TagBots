using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RespawnJugador : MonoBehaviour
{
    public int vidaInicial = 100;
    private int vidaActual;
    private bool estaVivo = true;

    void Start()
    {
        vidaActual = vidaInicial;
    }

    void RecibirDanio(int danio)
    {
        vidaActual -= danio;
        Debug.Log("El jugador ha recibido " + danio + " puntos de daño. Vida restante: " + vidaActual);

        // Verificar si el jugador está muerto
        if (vidaActual <= 0 && estaVivo)
        {
            estaVivo = false;
            StartCoroutine(RespawnearDespuesDeTiempo(3f)); // Respawnear después de 3 segundos
        }
    }

    IEnumerator RespawnearDespuesDeTiempo(float tiempoDeEspera)
    {
        yield return new WaitForSeconds(tiempoDeEspera);

        // Simular el respawn del jugador restaurando su vida y posición (puedes ajustar según tu juego)
        vidaActual = vidaInicial;
        estaVivo = true;
        Debug.Log("El jugador ha respawnado con vida completa.");

        // Colocar al jugador en una posición de respawn (puedes definir tu propia lógica para esto)
        transform.position = Vector3.zero; // Ejemplo: Respawnear en la posición (0,0,0)
    }
}

