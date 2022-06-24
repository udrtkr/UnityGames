using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private float waterDensity = 0.9998f;
    private float vol_xz;
    [SerializeField] private LayerMask playerLayer;
    private GameObject player;
    private Rigidbody rb;
    private BoxCollider bc;

    public bool isOn;

    private void OnTriggerStay(Collider other)
    {
        if (1 << other.gameObject.layer == playerLayer)
        {
            isOn = true;
            player = other.gameObject;
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
        }
    }
    private void Update()
    {
        if (isOn)
        {
            //rb.AddForce(Vector3.up * (transform.position.y - player.transform.position.y) * vol_xz * waterDensity * 9.81f,
            //            ForceMode.Force);
            rb.AddForce(Vector3.up * rb.mass * waterDensity * 9.81f,
                        ForceMode.Force);
        }
    }

}
