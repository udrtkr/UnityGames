using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPlace : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerUI.instance.SetFishingOK(true);
            Debug.Log("ok");
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(!PlayerUI.instance.isFishing)
                PlayerUI.instance.SetFishingOK(false);
        }
    }
}
