using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESP_Map : MonoBehaviour
{
    public LayerMask card;
    public bool isIn;
    public GameObject Incard;
    public int cardid;
    public int mapid;


    private void OnTriggerEnter(Collider other)
    {
        if (!isIn)
        {
            Incard = other.gameObject;
            cardid = Incard.GetComponent<ESP_Card>().CardID;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isIn)
        {
            Incard = null;
            cardid = 0;
        }
    }

    public void Clear()
    {
        isIn = false;
        Incard = null;
    }

    private void Update()
    {
        
        if(Incard != null && Input.GetMouseButtonUp(0))
        {
            Incard.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.01f, gameObject.transform.position.z);
            ESPManager.Instance.SetResult_Player(mapid, cardid);
            isIn = true;
        }
    }
}

