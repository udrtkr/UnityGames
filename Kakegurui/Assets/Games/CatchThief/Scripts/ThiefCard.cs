using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefCard : MonoBehaviour
{
    public ThiefCardType cardType;

    public void Reset()
    {
        transform.eulerAngles = new Vector3(-90, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public enum ThiefCardType
{
    A,
    Joker
}