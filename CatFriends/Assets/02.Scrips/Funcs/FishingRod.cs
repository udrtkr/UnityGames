using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    /// <summary>
    /// ����� �ɸ��� �� ������ = OnFish = true
    /// �������� �� ������ Ÿ�̹� ����
    /// �� ���������� Ŭ���ϸ� ������� ������� ����� �͵� ����� +1
    /// 
    /// </summary>
    public bool OnFish;
    private float timer;
    private float inFishTime = 5f;
    private int touchNum;
    private int sucessTouchNum = 5; // ����⸶�� �ٸ���
    //����� �Ŵ��� �ӽñ� �ϳ� ���� ����� ������Ʈ ������ ��ġȽ�� �ٸ���

    // Update is called once per frame
    private void Update()
    {
        
            
    }

    private void BeforeFishing()
    {
        // �ִϸ����� idle
        timer = Random.Range(5f, 15f);
        if (timer < 0)
        {
            OnFish = true;
        }
        timer -= Time.deltaTime;
        
    }

    private void OnFishing()
    {
        // �ִϸ����� ���� ������ �鰡��
        timer = inFishTime;
        timer -= Time.deltaTime;
        if(timer > 0)
        {
            if(touchNum > sucessTouchNum)
            {

                OnFish = false;
            }
        }

    }

    private void tryFishing()
    {
        if (Input.GetMouseButtonDown(0))
            touchNum++;
    }

    /*
    private void Fishing()  // ������ �ɷ��� �� �� ��ġ��ũ��? �� ��Ŭ��
    {
        if(OnFish)
    }
    */


}
