using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUI_ButtonMinus : ButtonUI // 컴포넌트 상속
{
    protected override void OnPressedMethod()
    {
        base.OnPressedMethod();
        transform.parent.gameObject.GetComponent<BetPanel>().ClickMinusButton(); // 부모로부터 메서드 가져옴
    }
}
