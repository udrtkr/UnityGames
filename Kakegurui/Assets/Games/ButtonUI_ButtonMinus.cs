using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUI_ButtonMinus : ButtonUI // ������Ʈ ���
{
    protected override void OnPressedMethod()
    {
        base.OnPressedMethod();
        transform.parent.gameObject.GetComponent<BetPanel>().ClickMinusButton(); // �θ�κ��� �޼��� ������
    }
}
