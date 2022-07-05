using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;
    public bool IsGroomingOk; // 쓰다듬기 ok (플레이어가 멈춰있을 때)
    public bool IsFishingOK;
    public FriendInfo friendInfo; // 친구 정보를 가져와 playerUI에서 사용해야함 : 이름,

    [SerializeField] private Button groomingButton;
    [SerializeField] private Button fishingButton;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject handPrefab;
    [SerializeField] private GameObject TalkPanel;
    private GameObject groomingHand;
    public bool isGroomingHand;
    public bool isFishing;

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;

        fishingButton.interactable = false;
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
    public void SetFishingOK(bool TorF)
    {
        fishingButton.interactable = TorF;
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

    public void SetFishing() // 피싱 버튼 온클릭 ==> 피싱 on 피싱 여부에 따라 카메라 전환 on
    {
        isFishing = !isFishing;
        if (isFishing)
        {
            CameraManager.instance.ShowFishingView();
            groomingButton.interactable = false;
        }
        else
            CameraManager.instance.ShowMainView();
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

    

   
}
