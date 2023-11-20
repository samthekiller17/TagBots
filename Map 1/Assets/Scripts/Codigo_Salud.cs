using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Codigo_Salud : MonoBehaviourPunCallbacks
{
    public float Salud = 100;
    public float SaludMaxima = 100;
    public float tiempoRespawn = 1f; // Tiempo de respawn despu�s de morir
    public float Defensa = 0; // Agregar una variable para la defensa
    private bool defensaActiva = false;
    private float duracionDefensa = 10f; // Duraci�n de la defensa activa en segundos
    private float tiempoRestanteDefensa = 0f; // Tiempo restante de la defensa activa

    [Header("Interfaz")]
    public Image BarraSalud;
    public Text TextoSalud;
    public PhotonView photonView;

    private bool estaVivo = true;
    private Vector3 posicionInicial; // Guardar la posici�n inicial del jugador

    void Start()
    {
        if (photonView.IsMine)
        {
            BarraSalud = GameObject.Find("Canvas/Salud/Barrra").GetComponent<Image>();
            TextoSalud = GameObject.Find("Canvas/Salud/TextoSalud").GetComponent<Text>();
            posicionInicial = transform.position; // Guardar la posici�n inicial
        }
    }

    void Update()
    {
        if (BarraSalud && TextoSalud)
        {
            ActualizarInterfaz();
        }
        if (defensaActiva)
        {
            if (tiempoRestanteDefensa > 0)
            {
                tiempoRestanteDefensa -= Time.deltaTime;
                if (tiempoRestanteDefensa <= 0)
                {
                    tiempoRestanteDefensa = 0;
                    Defensa = 0; // Volver a la defensa normal despu�s de que termine el tiempo de duraci�n
                    defensaActiva = false;
                    Debug.Log("Defensa desactivada");
                }
            }
        }
    }

    public void RecibirDa�o(float da�o)
    {
        if (!estaVivo) return; // Si ya est� muerto, no recibir m�s da�o

        Salud -= da�o;

        if (Salud <= 0)
        {
            Salud = 0;
            estaVivo = false;
            photonView.RPC("Morir", RpcTarget.AllBuffered); // Llamar al m�todo Morir en todos los clientes
        }
    }

    [PunRPC]
    void Morir()
    {
        Salud = SaludMaxima; // Restaurar la salud
        StartCoroutine(RespawnearDespuesDeTiempo(tiempoRespawn)); // Respawnear despu�s de un tiempo determinado
    }

    IEnumerator RespawnearDespuesDeTiempo(float tiempoDeEspera)
    {
        yield return new WaitForSeconds(tiempoDeEspera);

        // Encuentra todos los puntos de respawn disponibles
        GameObject[] respawnPoints = GameObject.FindGameObjectsWithTag("Respawn");

        if (respawnPoints.Length > 0)
        {
            // Escoge un punto de respawn aleatorio
            int randomIndex = Random.Range(0, respawnPoints.Length);
            Vector3 respawnPosition = respawnPoints[randomIndex].transform.position;

            // Respawnear el jugador en el punto seleccionado
            estaVivo = true;
            Salud = SaludMaxima;
            transform.position = respawnPosition; // Mover al jugador al punto de respawn
        }
        else
        {
            Debug.LogError("No se encontraron puntos de respawn. Aseg�rate de asignar el tag 'Respawn' a los objetos correspondientes.");
        }
    }

    void ActualizarInterfaz()
    {
        BarraSalud.fillAmount = Salud / SaludMaxima;
        TextoSalud.text = "Salud : " + Salud.ToString("f0");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (estaVivo && other.transform.CompareTag("Bala"))
        {
            RecibirDa�o(10);
        }
        if (estaVivo && other.transform.CompareTag("defensa"))
        {
            AumentarDefensa(5); // Aumentar la defensa por 5 unidades cuando toque un power-up con la etiqueta "defensa"
            other.gameObject.SetActive(false); // Desactivar el power-up
        }
    }

    void AumentarDefensa(float aumento)
    {
        Defensa += aumento;
        Debug.Log("Defensa aumentada a: " + Defensa);
        if (!defensaActiva)
        {
            defensaActiva = true;
            tiempoRestanteDefensa = duracionDefensa;
            Debug.Log("Defensa activada por " + duracionDefensa + " segundos");
        }
        else
        {
            tiempoRestanteDefensa = duracionDefensa; // Reiniciar el tiempo si se recoge otro power-up de defensa mientras est� activa
        }
    }
}