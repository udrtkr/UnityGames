using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Manager : MonoBehaviour
{
    private static Character_Manager _instance;
    public static Character_Manager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Character_Manager>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<Character_Manager>();
                }
            }

            return _instance;
        }

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

        if (Player == null)
        {
            Player = GameObject.Find("Player");
        }
        if(Opp == null)
        {
            Opp = GameObject.Find("Opp");
        }
    }

    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject Opp;

    public void SetAnimation(int WinWho)
    {
        AnimatorSetting animatorPlayer = Player.GetComponent<AnimatorSetting>();
        AnimatorSetting animatorOpp = Opp.GetComponent<AnimatorSetting>();
        switch (WinWho)
        {
            case 1:
                {
                    animatorPlayer.SetTrigger("Win");
                    animatorOpp.SetTrigger("Lose");
                    break;
                }
            case -1:
                { 
                    animatorPlayer.SetTrigger("Lose");
                    animatorOpp.SetTrigger("Win");
                    break;
                }
            default:
                break;
        }
    }
}