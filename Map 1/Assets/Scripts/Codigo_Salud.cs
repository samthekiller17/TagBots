/*using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Codigo_Salud : MonoBehaviour
{

    public float Salud = 100;   
    public float SaludMaxima = 100;

    [Header("Interfaz")]
    public Image BarraSalud;
    public Text TextoSalud;
    public PhotonView photonView;


    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)//Para activar todo lo de mi personaje
        {

            BarraSalud = GameObject.Find("Canvas/Salud/Barrra").GetComponent<Image>();
            TextoSalud = GameObject.Find("Canvas/Salud/TextoSalud").GetComponent<Text>();
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (BarraSalud && TextoSalud)
        {
            ActualizarInterfaz();
        }
        
    }
    
    public void RecibirDa�o(float da�o)
    {
        Salud -= da�o;
    }

    void ActualizarInterfaz()
    {
        BarraSalud.fillAmount = Salud / SaludMaxima;
        TextoSalud.text = "Salud : " + Salud.ToString("f0");    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Bala"))
        {
            RecibirDa�o(10);
        }
    }
}*/

using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Codigo_Salud : MonoBehaviourPunCallbacks
{
    public float Salud = 100;
    public float SaludMaxima = 100;
    public float tiempoRespawn = 1f; // Tiempo de respawn despu�s de morir

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
    }
}

