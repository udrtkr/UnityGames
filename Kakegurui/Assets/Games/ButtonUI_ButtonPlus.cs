using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonUI_ButtonPlus : ButtonUI // 만들어 둔 컴포넌트 상속
{
    protected override void OnPressedMethod()
    {
        base.OnPressedMethod();

        transform.parent.gameObject.GetComponent<BetPanel>().ClickPlusButton(); // 부모로부터 메서드 가져옴
    }
}
