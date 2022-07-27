using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    private static TalkManager _instance;
    public static TalkManager instance
    {
        get 
        {
            if (_instance == null)
                _instance = new TalkManager();
            return _instance;
        }
    }

    Dictionary<int, string[]> talkData_CatWhite = new Dictionary<int, string[]>();

    private void Awake()
    {
        Make_talkData_CatWhite();
    }

    private void Make_talkData_CatWhite()
    {
        talkData_CatWhite.Add(1, new string[] { "�ȳ�, ����...", "...���ķ� ���� ����..." });
        talkData_CatWhite.Add(2, new string[] {"���� �Ұ��Ұ�...", "���?" });
    }

    public string GetTalk(FriendType friendType, int id, int talkIndex)
    {
        if (friendType == FriendType.CatWhite)
            return talkData_CatWhite[id][talkIndex];
        else
            return "";
    }
}
