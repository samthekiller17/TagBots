using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour

{

    public GameObject bulletPrefab; // Prefab de la bala 

    public Transform ShootSpawn; // Punto desde donde se dispara la bala 

    public float fuerzaDisparo = 10f; // Fuerza con la que se dispara la bala 

    public float distanciaMaxima = 10f; // Distancia máxima que recorrerá la bala 

    public float tiempoDeVida = 2f; // Tiempo que la bala estará activa antes de desaparecer 



    void Update()

    {

        if (Input.GetButtonDown("Fire1")) // Puedes cambiar "Fire1" por el botón que desees utilizar para disparar 

        {

            DispararBala();

        }

    }



    void DispararBala()

    {

        GameObject bala = Instantiate(bulletPrefab, ShootSpawn.position, ShootSpawn.rotation);

        Rigidbody rb = bala.GetComponent<Rigidbody>();



        if (rb != null)

        {

            rb.AddForce(ShootSpawn.forward * fuerzaDisparo, ForceMode.Impulse);

            Destroy(bala, tiempoDeVida);

        }

        else

        {

            Debug.LogError("El prefab de la bala no tiene un componente Rigidbody.");

        }

    }

}
