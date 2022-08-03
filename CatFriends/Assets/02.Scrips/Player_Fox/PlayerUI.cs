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
    [SerializeField] private GameObject TalkPanel;
    private GameObject groomingHand;
    private GameObject fishingRod;
    public bool isGroomingHand;
    public bool isFishing;

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;

        fishingButton.interactable = false;

    }
    // 새총도 그루밍하구 비슷하게, UI off 일 때 커서 없어짐, 메인 카메라 커서에 따라 움직이게 최대 최소 정해서
    // 그루밍은 커서 없어짐, 커서에 따라 핸드 움직이게
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
        if (!isGroomingHand)
        {
            isGroomingHand = true;
            player.GetComponent<PlayerController>().forceStop = true;
            groomingHand = ToolManager.instance.SpawnTool(FuncToolType.GroomingHand, new Vector3(player.transform.position.x, 3, player.transform.position.z - 5), Quaternion.identity);
            gameObject.SetActive(false);
        }
        
    }
    public void SetGroomingHandOff() // 플레이어 컨트롤에서 esc 키 눌렀을 때 원래대로
    {
        if (isGroomingHand)
        {
            isGroomingHand = false;
            player.GetComponent<PlayerController>().forceStop = false;
            Destroy(groomingHand);
            gameObject.SetActive(true);
        }
    }

    public void SetFishing() // 피싱 버튼 온클릭 ==> 피싱 on 피싱 여부에 따라 카메라 전환 on
    {
        if (!isFishing)
        {
            isFishing = true;
            player.GetComponent<PlayerController>().forceStop = true;
            CameraManager.instance.ShowFishingView();
            fishingRod = ToolManager.instance.SpawnTool(FuncToolType.FishingRod, new Vector3(24, 0.52f, -40), Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    public void SetFishingOff()
    {
        if (isFishing)
        {
            isFishing = false;
            CameraManager.instance.ShowMainView();
            Destroy(fishingRod);
            gameObject.SetActive(true);
        }
    }

    public void SetTalk(bool TorF)
    {
        player.GetComponent<PlayerController>().forceStop = TorF;
        TalkPanel.SetActive(TorF);
        if (TorF)
        {
            TalkPanel.GetComponent<TalkPanel>().friendInfo = friendInfo;
            TalkPanel.GetComponent<TalkPanel>().SetName();
            TalkManager.instance.GetTalkData(friendInfo);
            // TalkPanel.GetComponentInChildren<Text>().text = friendInfo.frirendName;
        }
    }

    

   
}
