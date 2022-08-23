using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ESPManager : MonoBehaviour
{
    private static ESPManager _instance;
    public static ESPManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ESPManager>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<ESPManager>();
                }
            }

            return _instance;
        }
    }
    public Button BetButton;

    public bool moveOK=true;

    public GameObject[] ESP_Cards;
    public GameObject ESP_Map;
    public GameObject[] ESP_Maps = new GameObject[5];
    
    public GameObject[] ESP_Cards_Opp;
    public GameObject ESP_Map_Opp;
    public GameObject[] ESP_Maps_Opp = new GameObject[5];

    public GameObject[] ESP_Cards_Dealer;
    public GameObject ESP_Map_Dealer;
    public GameObject[] ESP_Maps_Dealer = new GameObject[5];

    public Vector3[] ESP_Cards_PosInit = new Vector3[5];
    public Vector3[] ESP_Cards_Opp_PosInit = new Vector3[5];
    public Vector3[] ESP_Cards_Dealer_PosInit = new Vector3[5];

    public int[] result_Player = new int[5];
    public int[] result_Opp = new int[5];
    public int[] result_Dealer = new int[5];


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        for (int i = 0; i < ESP_Cards.Length; i++)
        {
            ESP_Cards_PosInit[i] = ESP_Cards[i].transform.position;
            ESP_Cards_Opp_PosInit[i] = ESP_Cards_Opp[i].transform.position;
            ESP_Cards_Dealer_PosInit[i] = ESP_Cards_Dealer[i].transform.position;
        }

        BetButton.interactable = false;
    }

    private void Update()
    {
        for(int i = 0; i < result_Player.Length; i++)
        {
            result_Player[i] = ESP_Maps[i].GetComponent<ESP_Map>().cardid;
        }

        if (!result_Player.Contains(0))
        {
            BetButton.interactable = true;
        }
    }
    public void Init() // 리셋 누를 때
    {
        BetButton.interactable = false;
        moveOK = true;
        for (int i = 0; i < ESP_Cards.Length; i++)
        {
            ESP_Cards[i].transform.position = ESP_Cards_PosInit[i];
            ESP_Cards_Opp[i].transform.position = ESP_Cards_Opp_PosInit[i];
            ESP_Cards_Dealer[i].transform.position = ESP_Cards_Dealer_PosInit[i];
            ESP_Cards_Dealer[i].transform.rotation = Quaternion.Euler(0,0,180); // 뒤집어서 시작

            ESP_Maps[i].GetComponent<ESP_Map>().Clear();
        }
    }
}
