using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    /// <summary>
    /// 물고기 걸리면 찌 내려감 = OnFish = true
    /// 랜덤으루 찌 내리는 타이밍 만듦
    /// 찌 내려갓을때 클릭하면 원래대로 도라오고 물고기 터득 물고기 +1
    /// 
    /// </summary>
    public bool OnFish;
    private float timer;
    private float inFishTime = 5f;
    private int touchNum;
    private int sucessTouchNum = 5; // 물고기마다 다르게
    //물고기 매니지 머시기 하나 만들어서 물고기 오브젝트 데려와 터치횟수 다르게

    // Update is called once per frame
    private void Update()
    {
        
            
    }

    private void BeforeFishing()
    {
        // 애니메이터 idle
        timer = Random.Range(5f, 15f);
        if (timer < 0)
        {
            OnFish = true;
        }
        timer -= Time.deltaTime;
        
    }

    private void OnFishing()
    {
        // 애니메이터 변경 밑으루 들가게
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
    private void Fishing()  // 물고기ㅣ 걸렸을 때 그 터치스크린? 에 온클릭
    {
        if(OnFish)
    }
    */


}
