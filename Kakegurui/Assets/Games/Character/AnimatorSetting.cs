using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSetting : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public int GetInteger(string name) => animator.GetInteger(name);
    public float GetFloat(string name) => animator.GetFloat(name);
    public bool GetBool(string name) => animator.GetBool(name);
    public void SetInteger(string name, int integer) => animator.SetInteger(name, integer);
    public void SetFloat(string name, float floating) => animator.SetFloat(name, floating);
    public void SetBool(string name, bool booling) => animator.SetBool(name, booling);
    public void SetTrigger(string name) => animator.SetTrigger(name);
}
