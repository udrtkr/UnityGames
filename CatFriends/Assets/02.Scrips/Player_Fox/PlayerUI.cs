using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;
    public bool IsGroomingOk; // æ≤¥ŸµÎ±‚ ok («√∑π¿ÃæÓ∞° ∏ÿ√Á¿÷¿ª ∂ß)
    public Button groomingButton;
    public GameObject groomingHand;

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
    }
}
