using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESPManager : MonoBehaviour
{
    private static ESPManager _instance;
    public static ESPManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ESPManager();

            return _instance;
        }
    }

    public bool moveOK=true;
    public GameObject[] ESP_Cards;
    public GameObject[] ESP_Cards_Opp;
    public Vector3[] ESP_Cards_PosInit = new Vector3[5];
    public Vector3[] ESP_Cards_Opp_PosInit = new Vector3[5];
    private Vector3 Init_ESPcardsPos = new Vector3(-0.9f, 0.841f, -0.325f);

    private void Start()
    {
        for (int i = 0; i < ESP_Cards.Length; i++)
        {
            ESP_Cards_PosInit[i] = ESP_Cards[i].transform.position;
            ESP_Cards_Opp_PosInit[i] = ESP_Cards_Opp[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init()
    {
        for (int i = 0; i < ESP_Cards.Length; i++)
        {
            ESP_Cards[i].transform.position = ESP_Cards_PosInit[i];
        }
    }
}
