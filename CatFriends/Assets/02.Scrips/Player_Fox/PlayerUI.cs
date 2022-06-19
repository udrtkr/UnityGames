using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;
    public bool IsGroomingOk; // ���ٵ�� ok (�÷��̾ �������� ��)
    public Button groomingButton;
    public GameObject groomingHand;

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
    }
}
