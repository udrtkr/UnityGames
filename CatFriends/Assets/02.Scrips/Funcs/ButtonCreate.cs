using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCreate : MonoBehaviour
{
    public GameObject toolPrefab;
    public GameObject player;

    public void SetTool()
    {
        Instantiate(toolPrefab, 
                    new Vector3(player.transform.position.x, 3, player.transform.position.z - 5), 
                    Quaternion.identity);
    }
}
