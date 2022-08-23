using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESP_Map : MonoBehaviour
{
    public LayerMask card;
    public bool isIn;
    public ESPcardType cardtype;
    public GameObject Incard;
    private void OnTriggerStay(Collider other)
    {
        if (!isIn)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Card"))
            {
                if (Input.GetMouseButtonUp(0))
                {
                    other.gameObject.transform.Translate(gameObject.transform.position, Space.World);
                    isIn = true;
                    cardtype = other.GetComponent<ESP_Card>().cardType;
                    Incard = other.GetComponent<GameObject>();
                }
            }
        }
    }

    private void Update()
    {
        /*
        if(Incard != null)
            Incard.transform.position = gameObject.transform.position;
        */
    }
}

