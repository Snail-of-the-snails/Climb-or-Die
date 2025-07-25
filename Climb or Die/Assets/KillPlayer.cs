using System;
using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit Something!");

        int layerNum = collision.gameObject.layer;

        if (layerNum == LayerMask.NameToLayer("Jeff")) {
            rb.AddForce(Vector3.back * 100000000000);
        }
    }
}
