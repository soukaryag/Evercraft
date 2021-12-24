using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator animator;
    private string currentState;
    public virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void changeAnimation(string newState) {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }
}
