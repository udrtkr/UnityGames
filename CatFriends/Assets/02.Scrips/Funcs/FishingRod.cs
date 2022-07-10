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
    public bool UpFish;
    public float timer;
    private float inFishTime = 5f;
    public int touchNum;
    private int sucessTouchNum = 5; // 물고기마다 다르게
    private Animator animator;
    public RodState rodState;
    public float UpFishTime;
    //물고기 매니지 머시기 하나 만들어서 물고기 오브젝트 데려와 터치횟수 다르게


    private void Awake()
    {
        rodState = RodState.Idle;
        animator = GetComponent<Animator>();
        timer = Random.Range(5f, 10f);
        UpFishTime = GetAnimatorNameTime("UpFish");
    }
    // Update is called once per frame
    private void Update()
    {
        UpdateRodState();
    }

    private void Idle()
    {
        // 애니메이터 idle
        animator.Play("Idle");
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }

        else
        {
            OnFish = true;
            animator.SetBool("OnFish", true);
            
        }
        
        
    }

    private void OnFishing()
    {
        // 애니메이터 변경 밑으루 들가게
        //timer = inFishTime;
        
        if (timer > 0)
        {
            tryFishing();
            timer -= Time.deltaTime;
            if (touchNum > sucessTouchNum)
            {
                touchNum = 0;
                UpFish = true;
                animator.SetBool("UpFish", true);
            }
        }
        else
        {
            touchNum = 0;
            UpFish = false;
            OnFish = false;
            animator.SetBool("OnFish", false);
        }

    }

    private void tryFishing()
    {
        if (Input.GetMouseButtonDown(0))
            touchNum++;
    }

    private void UpFishing()
    {
        animator.SetBool("UpFish", true);
        
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0;
            OnFish = false;
            animator.SetBool("OnFish", OnFish);
            UpFish = false;
            animator.SetBool("UpFish", UpFish);
            timer = Random.Range(5f, 1f);

        }
        // 물고기 위로 푸슝 파슝
    }

    

    private void UpdateRodState()
    {
        switch (rodState)
        {
            case RodState.Idle:
                Idle();
                if (OnFish)
                {
                    rodState++;
                    timer = inFishTime;
                }
                break;
            case RodState.OnFish:
                OnFishing();
                if (UpFish)
                {
                    rodState++;
                    timer = UpFishTime;
                }
                if (!OnFish)
                {
                    rodState--;
                    timer = Random.Range(10f, 10f);
                }
                break;
            case RodState.UpFish:
                UpFishing();
                if (!UpFish)
                {
                    rodState = RodState.Idle;
                    timer = Random.Range(10f, 10f);
                }
                break;
        }
    }

    public enum RodState
    {
        Idle,
        OnFish,
        UpFish,
    }

    public float GetAnimatorNameTime(string AnimationName)
    {
        float time = 0;
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == AnimationName)
            {
                time = ac.animationClips[i].length;
            }
        }
        return time;
    }
    /*
    private void Fishing()  // 물고기ㅣ 걸렸을 때 그 터치스크린? 에 온클릭
    {
        if(OnFish)
    }
    */


}
