using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;
    public bool IsGroomingOk; // ���ٵ�� ok (�÷��̾ �������� ��)
    public bool IsFishingOK;
    public FriendInfo friendInfo; // ģ�� ������ ������ playerUI���� ����ؾ��� : �̸�,

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
    // ���ѵ� �׷���ϱ� ����ϰ�, UI off �� �� Ŀ�� ������, ���� ī�޶� Ŀ���� ���� �����̰� �ִ� �ּ� ���ؼ�
    // �׷���� Ŀ�� ������, Ŀ���� ���� �ڵ� �����̰�
    public void SetGroomingOk(bool isIdle) // �÷��̾���Ʈ�ѿ��� isIdle �� ��, isGroomingOk �� button interact ��Ʈ��
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

    public void SetGroomingHand() // �׷�� ��ư�� ��Ŭ�� ==> �׷�� on
    {
        if (!isGroomingHand)
        {
            isGroomingHand = true;
            player.GetComponent<PlayerController>().forceStop = true;
            groomingHand = ToolManager.instance.SpawnTool(FuncToolType.GroomingHand, new Vector3(player.transform.position.x, 3, player.transform.position.z - 5), Quaternion.identity);
            gameObject.SetActive(false);
        }
        
    }
    public void SetGroomingHandOff() // �÷��̾� ��Ʈ�ѿ��� esc Ű ������ �� �������
    {
        if (isGroomingHand)
        {
            isGroomingHand = false;
            player.GetComponent<PlayerController>().forceStop = false;
            Destroy(groomingHand);
            gameObject.SetActive(true);
        }
    }

    public void SetFishing() // �ǽ� ��ư ��Ŭ�� ==> �ǽ� on �ǽ� ���ο� ���� ī�޶� ��ȯ on
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
