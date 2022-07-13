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
    private int sucessTouchNum; // 물고기마다 다르게
    private Animator animator;
    public RodState rodState;
    public float UpFishTime;
    public float v;
    public float a;
    //물고기 매니지 머시기 하나 만들어서 물고기 오브젝트 데려와 터치횟수 다르게

    private GameObject fish;


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
        

        if (timer > 0)
        {
            if(timer >= 0.2f)
                getFish();  
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
    private void getFish() // UpFishing 에서 사용
    {
        v += a * Time.deltaTime;
        fish.GetComponent<Transform>().position += new Vector3(0, v, 0) * Time.deltaTime;
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
                    fish = FishSpawner.instance.SpawnRandomFish();
                    sucessTouchNum = fish.GetComponent<Fish>().touchNum;
                }
                break;
            case RodState.OnFish:
                OnFishing();
                if (UpFish) // 낚시 성공, Upfish 상태로 갈 때
                {
                    rodState++;
                    timer = UpFishTime;
                    v = 20f; a = -30f; // 물고기 움직이는 속도 지정
                    // v = 5f; a = 1f;
                }
                if (!OnFish) // 시간 초과, idle로 돌아갈 때
                {
                    rodState--;
                    timer = Random.Range(10f, 10f);
                    Destroy(fish);
                    //fish = null;
                }
                break;
            case RodState.UpFish:
                UpFishing();
                if (!UpFish) // 낚은 후 idle로 돌아갈 때
                {
                    rodState = RodState.Idle;
                    timer = Random.Range(10f, 10f);
                    // 타이머 시간만큼 fish 위로 방
                    Destroy(fish);
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


}
