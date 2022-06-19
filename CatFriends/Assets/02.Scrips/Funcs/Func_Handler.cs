using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Grooming, Fishing ,...
/// </summary>
public class Func_Handler : MonoBehaviour
{
    public static Func_Handler instance;
    public GameObject GroomingHand;

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
    }

    public void SetGroomingHand()
    {
        Instantiate(GroomingHand);
    }

}
