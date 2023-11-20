using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public PhotonView photonView;
    public GameObject camara;
    public TextMeshPro textNickName;

    private void Start()
    {
        if(photonView.IsMine)//Para activar todo lo de mi personaje
        {
            textNickName.text = PlayerPrefs.GetString("nickname");
            photonView.Owner.NickName = PlayerPrefs.GetString("nickname");
            camara.SetActive(true);
        }
        else//Para activar cosas del otro jugador
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
            textNickName.text = photonView.Owner.NickName;
            camara.SetActive(false);
        }
    }


    void Update()
    {
        if(photonView.IsMine)
        {
            float H = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
            float V = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            transform.Translate(H, 0, V);
        }
    }

}
