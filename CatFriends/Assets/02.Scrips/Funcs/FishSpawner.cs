using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public static FishSpawner instance;
    Dictionary<FishPercent, GameObject[]> Fishes = new Dictionary<FishPercent, GameObject[]>();

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }

    public void Finish()
    {
        Destroy(gameObject);
    }
}
