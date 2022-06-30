using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBoatDetector : MonoBehaviour
{
    public bool isDetect;
    [SerializeField] private LayerMask Player;
    private void OnTriggerStay(Collider other)
    {
        if(1 << other.gameObject.layer == Player)
        {
            isDetect = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (1 << other.gameObject.layer == Player)
        {
            isDetect = false;
        }
    }
}
