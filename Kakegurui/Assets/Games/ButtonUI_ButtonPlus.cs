using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonUI_ButtonPlus : ButtonUI // ����� �� ������Ʈ ���
{
    protected override void OnPressedMethod()
    {
        base.OnPressedMethod();

        transform.parent.gameObject.GetComponent<BetPanel>().ClickPlusButton(); // �θ�κ��� �޼��� ������
    }
}
