using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public bool GetBool(string name) => animator.GetBool(name);

    public float GetFloat(string name) => animator.GetFloat(name);

    public int GetInt(string name) => animator.GetInteger(name);

    public void SetBool(string name, bool value) => animator.SetBool(name, value);

    public void SetFloat(string name, float value) => animator.SetFloat(name, value);

    public void SetInt(string name, int value) => animator.SetInteger(name, value);

    public void SetTrigger(string name) => animator.SetTrigger(name);

    public float GetAnimatorNameTime(string AnimationName)
    {
        float time = 0;
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        for(int i = 0; i< ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == AnimationName)
            {
                time = ac.animationClips[i].length;
            }
        }
        return time;
    }

    public float GetCurrentAniTime()
    {
        return animator.GetCurrentAnimatorClipInfo(0).Length;
    }
}
