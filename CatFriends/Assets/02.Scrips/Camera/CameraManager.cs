using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public Camera camMain;
    public Camera camFishing;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
        camMain.enabled = true;
        camFishing.enabled = false;
    }
    public void ShowMainView()
    {
        camMain.enabled = true;
        camFishing.enabled = false;
    }

    public void ShowFishingView()
    {
        camMain.enabled = false;
        camFishing.enabled = true;
    }
}
