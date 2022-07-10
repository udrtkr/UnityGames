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
    public bool UpFish;
    public float timer;
    private float inFishTime = 5f;
    public int touchNum;
    private int sucessTouchNum = 5; // ����⸶�� �ٸ���
    private Animator animator;
    public RodState rodState;
    public float UpFishTime;
    //����� �Ŵ��� �ӽñ� �ϳ� ���� ����� ������Ʈ ������ ��ġȽ�� �ٸ���


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
        // �ִϸ����� idle
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
        // �ִϸ����� ���� ������ �鰡��
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
        // ����� ���� Ǫ�� �Ľ�
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
    private void Fishing()  // ������ �ɷ��� �� �� ��ġ��ũ��? �� ��Ŭ��
    {
        if(OnFish)
    }
    */


}
