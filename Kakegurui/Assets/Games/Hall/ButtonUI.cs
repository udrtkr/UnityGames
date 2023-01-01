using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler // ���콺�� ��ư ������ ���� ��, �� �� ���� �̺�Ʈ �������̽� ���
{
    bool isPressed = false;
    float timeLimit = 0.1f; // ��ư ������ ���� �� �ݺ��Ǵ� �ð� ����
    float timeadd = 0.1f; // �帥 �ð� ������ ����, ó�� ������ �� �ٷ� �ݿ��ǰ�
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData) // ������ ���� ��, Ư�� �޼��� ����
    {
        isPressed = true;
    }
    protected virtual void OnPressedMethod() // ������ ���� �� ������ �޼���, ����Ͽ� �� ��ư�� ��� ����
    {

    }

    public void OnPointerUp(PointerEventData eventData) // ���� ��, ����Ͽ� �� ��ư�� ��� ����
    {
        isPressed = false;
        timeadd = timeLimit; // �帥 �ð� ����
    }

    // Update is called once per frame
    protected void Update() // ��� ���� protected��
    {
        if (isPressed) // ������ ���� ��
        {
            if(timeadd < timeLimit) // �ð��� �� �귶���� deltaTime +
                timeadd += Time.deltaTime;
            else // �ð� �� �귶���� timeadd ���� �� �޼��� ����
            {
                timeadd = 0f;
                OnPressedMethod();
            }
        }
    }
}
