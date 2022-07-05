using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector_Friends : MonoBehaviour
{
    public static Detector_Friends instance;
    public bool isDetect;
    public GameObject Friend;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Friend"))
        {
            isDetect = true;
            Friend = other.gameObject;
            Friend.GetComponent<FriendsController>().Deteted(isDetect, transform.position);
            FriendsUI.instance.SetUp(other.gameObject.transform.position, Friend.GetComponent<Friend>().info);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Friend"))
        {
            isDetect = false;
            Friend.GetComponent<FriendsController>().Deteted(isDetect, Vector3.zero);
            FriendsUI.instance.Clear();
        }
    }

    public Vector3 GetPlayerPosition()
    {
        return gameObject.transform.position;
    }


}
