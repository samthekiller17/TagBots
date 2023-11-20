using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejoJuego : MonoBehaviour
{
    public float vidaInicial = 100f;
    public float vidaActual;
    public float puntos = 0f;
    public float puntosGanar = 100f;
    public float vidaPerdida = 20f;

    // Aquí puedes definir referencias a las interfaces, mostrar mensajes de fin de juego, etc.

    void Start()
    {
        vidaActual = vidaInicial; // Establecer la vida inicial al comenzar el juego
    }

    // Lógica para recibir daño
    public void RecibirDanio(float cantidadDanio)
    {
        vidaActual -= cantidadDanio;

        // Verificar si la vida llega a cero o menos (pierde el juego)
        if (vidaActual <= 0)
        {
            PerderJuego();
        }
    }

    // Lógica para ganar puntos
    public void GanarPuntos(float cantidadPuntos)
    {
        puntos += cantidadPuntos;

        // Verificar si se alcanza la cantidad de puntos para ganar (gana el juego)
        if (puntos >= puntosGanar)
        {
            GanarJuego();
        }
    }

    void PerderJuego()
    {
        // Aquí puedes manejar el fin del juego cuando el jugador pierde
        Debug.Log("¡Has perdido! Tu vida ha llegado a cero.");
        // Por ejemplo, cargar una pantalla de Game Over o reiniciar el nivel
        SceneManager.LoadScene("GameOverScene");
    }

    void GanarJuego()
    {
        // Aquí puedes manejar la victoria del juego cuando el jugador gana
        Debug.Log("¡Has ganado! Has alcanzado la cantidad de puntos necesarios.");
        // Por ejemplo, cargar una pantalla de victoria o avanzar al siguiente nivel
        SceneManager.LoadScene("VictoryScene");
    }
}

