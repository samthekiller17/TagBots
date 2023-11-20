using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rot : MonoBehaviour
{
    public float rootSpeed;
    void Update()
    {
        transform.Rotate(Vector3.up*Time.deltaTime*rootSpeed);
    }
}
