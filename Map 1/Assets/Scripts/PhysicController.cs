using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicController : MonoBehaviour
{
        public GameObject balaPrefab;
        public Transform spawn;

        void Update() {
        if(Input.GetButtonDown("Fire1")){
                Fire();
        }        
        }
        void Fire(){
                GameObject newBala = Instantiate(balaPrefab);
                newBala.transform.position = spawn.position;
                newBala.transform.rotation = spawn.rotation;
                newBala.GetComponent<Rigidbody>().velocity =spawn.forward*20;
        }
        private void OnTriggerEnter(Collider other){
            if(other.tag =="coin"){
                Destroy(other.gameObject);
            }
            if(other.gameObject.name=="cambio"){
                other.GetComponent<MeshRenderer>().material.color =Color.blue;
                other.transform.rotation = Quaternion.Euler(90,0,0);
                gameObject.name="santiago";
            }
            if(other.gameObject.name=="enemi"){
                Debug.Log("Damage");
            }
        }
        private void OnCollisionEnter(Collision collision) {
                if(collision.gameObject.name=="enemi"){
                        Debug.Log("Damage");       
                } 
        }
}
