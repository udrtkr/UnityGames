using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler // 마우스로 버튼 누르고 있을 때, 뗄 때 위한 이벤트 인터페이스 상속
{
    bool isPressed = false;
    float timeLimit = 0.1f; // 버튼 누르고 있을 때 반복되는 시간 설정
    float timeadd = 0.1f; // 흐른 시간 측정할 변수, 처음 눌렀을 땐 바로 반영되게
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData) // 누르고 있을 때, 특정 메서드 실행
    {
        isPressed = true;
    }
    protected virtual void OnPressedMethod() // 누르고 있을 때 실행할 메서드, 상속하여 각 버튼에 기능 구현
    {

    }

    public void OnPointerUp(PointerEventData eventData) // 뗐을 때, 상속하여 각 버튼에 기능 구현
    {
        isPressed = false;
        timeadd = timeLimit; // 흐른 시간 리셋
    }

    // Update is called once per frame
    protected void Update() // 상속 위해 protected로
    {
        if (isPressed) // 누르고 있을 때
        {
            if(timeadd < timeLimit) // 시간이 덜 흘렀으면 deltaTime +
                timeadd += Time.deltaTime;
            else // 시간 다 흘렀으면 timeadd 리셋 후 메서드 실행
            {
                timeadd = 0f;
                OnPressedMethod();
            }
        }
    }
}
