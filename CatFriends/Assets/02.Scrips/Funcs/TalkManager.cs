using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TalkManager : MonoBehaviour, IPointerClickHandler
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

    [SerializeField] private Text Story;
    private int idx;
    private string thema; // 

    List<Dictionary<string, object>> talkData = new List<Dictionary<string, object>>(); // 상황에 맞게 csv 데이터 가져옴, 형식은 data[listindex]["dictionaryKey=csv의 열 0번째"]

    private void Awake()
    {
    }
    
    public void GetTalkData(FriendInfo friendInfo) // 버튼 눌렀을 때 가저와보자
    {
        talkData = CSVReader.Read("TalkData/" + friendInfo.talkDataName);
        thema = friendInfo.idTalk.ToString();
        
    }
    public string GetTalk(int talkIndex)
    {
        return talkData[talkIndex][thema].ToString(); 
    }

    public void SetStoryText(FriendInfo friendInfo)
    {
        Story.text = talkData[idx][friendInfo.idTalk.ToString()].ToString();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        idx++;
        // 즐찾참고 프렌드 인포 그 다른데서 가져오자 어디엿지 먼 유야이에서
    }
}