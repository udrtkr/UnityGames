using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;
    public bool IsGroomingOk; // 쓰다듬기 ok (플레이어가 멈춰있을 때)
    public FriendInfo friendInfo; // 친구 정보를 가져와 playerUI에서 사용해야함 : 이름,

    [SerializeField] private Button groomingButton;
    [SerializeField] private Button fishingButton;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject handPrefab;
    [SerializeField] private GameObject TalkPanel;
    private GameObject groomingHand;
    public bool isGroomingHand = false;

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
    }

    public void SetGroomingOk(bool isIdle) // 플레이어컨트롤에서 isIdle 일 때, isGroomingOk 와 button interact 컨트롤
    {
        if (isIdle)
        {
            IsGroomingOk = true;
            groomingButton.interactable = true;
        }
        else
        {
            IsGroomingOk = false;
            groomingButton.interactable = false;
        }
    }

    public bool GetGrooming() // 플레이어 컨트롤러에서 사용
    {
        return isGroomingHand;
    }

    public void SetGroomingHand() // 그루밍 버튼의 온클릭 ==> 그루밍 on
    {
        isGroomingHand = !isGroomingHand;
        if (isGroomingHand)
        {
            groomingHand = Instantiate(handPrefab,
                                new Vector3(player.transform.position.x, 3, player.transform.position.z - 5),
                                Quaternion.identity);
        }
        else
        {
            // 일어날 시간까지 forcestop true로
            Destroy(groomingHand);
        }
    }

    public void SetTalk(bool TorF)
    {
        TalkPanel.SetActive(TorF);
        if (TorF)
        {
            TalkPanel.GetComponent<TalkPanel>().friendInfo = friendInfo;
            TalkPanel.GetComponent<TalkPanel>().SetName();
            // TalkPanel.GetComponentInChildren<Text>().text = friendInfo.frirendName;
        }
    }

    public void SetTalkFinish()
    {
        TalkPanel.SetActive(false);
    }
}
