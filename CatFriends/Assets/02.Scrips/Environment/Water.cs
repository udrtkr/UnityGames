using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private float waterDensity = 0.9998f;
    private float vol_xz;
    public LayerMask playerLayer;
    private GameObject player;
    private Rigidbody rb;
    private BoxCollider bc;
    private Transform tr;

    public bool isOn;

    private void OnTriggerStay(Collider other)
    {
        if (1 << other.gameObject.layer == playerLayer)
        {
            isOn = true;
            other.gameObject.GetComponent<PlayerController>().UnderTheSea(true);
            tr = other.gameObject.transform;
            rb = other.gameObject.GetComponent<Rigidbody>();
            bc = other.gameObject.GetComponent<BoxCollider>();
            vol_xz = (bc.size.x * bc.size.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (1 << other.gameObject.layer == playerLayer)
        {
            isOn = false;
            other.gameObject.GetComponent<PlayerController>().UnderTheSea(false);
        }
    }
    private void FixedUpdate()
    {
        if (isOn)
        {
            rb.AddForce(Vector3.up * (transform.position.y - tr.position.y) * vol_xz * waterDensity * 9.81f,
                        ForceMode.Acceleration);
            // rb.AddForce(Vector3.up * rb.mass * waterDensity * 9.81f,
            //            ForceMode.Acceleration);
        }
    }

}
