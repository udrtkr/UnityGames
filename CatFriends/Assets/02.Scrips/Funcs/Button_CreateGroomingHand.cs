using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_CreateGroomingHand : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject handPrefab;
    private GameObject hand;
    public bool isGroomingHand = false;

    public void SetGroomingHand()
    {
        isGroomingHand = !isGroomingHand;
        if (!isGroomingHand)
        {
            hand = Instantiate(handPrefab,
                                new Vector3(player.transform.position.x, 3, player.transform.position.z - 5),
                                Quaternion.identity);
        }
        else
        {
            Destroy(hand);
        }
    }
}
