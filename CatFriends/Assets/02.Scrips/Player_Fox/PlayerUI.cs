using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;
    public bool IsGroomingOk; // ���ٵ�� ok (�÷��̾ �������� ��)
    public FriendInfo friendInfo; // ģ�� ������ ������ playerUI���� ����ؾ��� : �̸�,

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

    public bool GetGrooming() // �÷��̾� ��Ʈ�ѷ����� ���
    {
        return isGroomingHand;
    }

    public void SetGroomingHand() // �׷�� ��ư�� ��Ŭ�� ==> �׷�� on
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
            // �Ͼ �ð����� forcestop true��
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
