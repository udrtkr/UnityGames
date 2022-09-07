using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// 시작할 때 코루틴으로 먼저 카드 분배
/// 상대 카드도 랜덤으로
/// 랜덤으로 하려면 list에 배열 저장, 랜덤으로 꺼내서 remove while(array 배열 영댈 때 까지) init에서 설정
/// </summary>

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
    public Button resetButton;
    public Button BetButton;
    public GameObject winText;

    public bool moveOK;

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

    public int[] oriarr = { 1, 2, 3, 4, 5 };

    public int[] result_Player = new int[5];
    public int[] result_Opp = new int[5];
    public int[] result_Dealer = new int[5];

    public List<int> result_Opp_list;
    public List<int> result_Dealer_list;

    public int Win;

    private void SetResult_DealerAndOpp()
    {
        int i = 0;
        while(result_Dealer_list.Count > 0)
        {
            int listidxOpp = UnityEngine.Random.Range(0, result_Opp_list.Count);
            result_Opp[i] = result_Opp_list[listidxOpp];
            result_Opp_list.RemoveAt(listidxOpp);

            int listidxDealer = UnityEngine.Random.Range(0, result_Dealer_list.Count);
            result_Dealer[i] = result_Dealer_list[listidxDealer];
            result_Dealer_list.RemoveAt(listidxDealer);

            i++;
        }
    }

    public void SetCards_DealerAndOpp()
    {
        StartCoroutine(E_SetDealerAndOppCard());
    }
    IEnumerator E_SetDealerAndOppCard()
    {
        yield return new WaitForSeconds(1);

        yield return StartCoroutine(E_SetDealerCard());
        yield return null;

        moveOK = true;

        yield return new WaitForSeconds(1);

        yield return StartCoroutine(E_SetOppCard());

    }
    IEnumerator E_SetDealerCard()
    {
        int idx = 0;
        while (idx < 5)
        {
            Vector3 dir = (ESP_Maps_Dealer[idx].transform.position - ESP_Cards_Dealer[result_Dealer[idx] - 1].transform.position + new Vector3(0f, 0.01f, 0)).normalized;
            ESP_Cards_Dealer[result_Dealer[idx] - 1].transform.position += 0.003f * dir;

            if (Vector3.Magnitude(ESP_Maps_Dealer[idx].transform.position - ESP_Cards_Dealer[result_Dealer[idx] - 1].transform.position) < 0.01006f)
            {
                idx++;
                Debug.Log("ok");
                continue;
            }
            yield return null;
        }
    }
    IEnumerator E_SetOppCard()
    {
        for (int i = 0; i < result_Opp.Length; i++)
        {
            ESP_Cards_Opp[result_Opp[i] - 1].transform.position = ESP_Maps_Opp[i].transform.position;

            yield return new WaitForSeconds(1f);
        }
    }

    public void BetClick()
    {
        int playerScore = 0;
        int oppScore = 0;
        for(int i=0; i<result_Dealer.Length; i++)
        {
            if(result_Player[i] == result_Dealer[i])
                playerScore++;
            if(result_Opp[i] == result_Dealer[i])
                oppScore++;
        }
        if (playerScore > oppScore)
            Win = 1; // 이겼을 때
        else if (playerScore < oppScore)
            Win = -1; // 졌을 때
        else
            Win = 0; // 비겼을 때

        switch (Win)
        {
            case 0:
                winText.GetComponent<Text>().text = "Draw";
                break;
            case 1:
                winText.GetComponent<Text>().text = "Win";
                break;
            case -1:
                winText.GetComponent<Text>().text = "Lose";
                break ;
            default:
                break;
        }

        StartCoroutine(DealerCardBack());
    }

    IEnumerator DealerCardBack()
    {
        yield return new WaitForSeconds(1);
        int i = 0;
        while (i < result_Dealer.Length)
        {
            ESP_Cards_Dealer[result_Dealer[i]-1].transform.eulerAngles -= new Vector3(0, 0, 2f);
            if (ESP_Cards_Dealer[result_Dealer[i]-1].transform.eulerAngles.z < 2)
            {
                i++;
                continue;
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);

        winText.SetActive(true);
        resetButton.interactable = true;
    }


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

        result_Opp_list = new List<int>(oriarr);
        result_Dealer_list = new List<int>(oriarr);

        winText.SetActive(false);
        resetButton.interactable = false;
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

        SetResult_DealerAndOpp();
        SetCards_DealerAndOpp();
    }

    private void Update()
    {
        if (!result_Player.Contains(0))
        {
            BetButton.interactable = true;
        }
        else
        {
            BetButton.interactable = false;
        }
    }

    public void SetResult_Player(int mapid, int cardid)
    {
        result_Player[mapid] = cardid;
    }
    public void ResetESP() // 리셋 누를 때
    {
        BetButton.interactable = false;
        resetButton.interactable = false;
        moveOK = false;
        for (int i = 0; i < ESP_Cards.Length; i++)
        {
            ESP_Cards[i].transform.position = ESP_Cards_PosInit[i];
            ESP_Cards_Opp[i].transform.position = ESP_Cards_Opp_PosInit[i];
            ESP_Cards_Dealer[i].transform.position = ESP_Cards_Dealer_PosInit[i];
            ESP_Cards_Dealer[i].transform.rotation = Quaternion.Euler(0,0,180); // 뒤집어서 시작

            ESP_Maps[i].GetComponent<ESP_Map>().Clear();

            result_Player[i] = 0;
            result_Opp[i] = 0;
            result_Dealer[i] = 0;
        }

        result_Opp_list = new List<int>(oriarr);
        result_Dealer_list = new List<int>(oriarr);
        SetResult_DealerAndOpp();
        SetCards_DealerAndOpp();

        winText.SetActive(false);
    }


}
