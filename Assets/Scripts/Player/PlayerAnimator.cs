using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("References")]
    public PlayerController controller;
    public Animator animator;

    void Update()
    {
        MovementAnimation();
    }

    void MovementAnimation()
    {
        animator.SetBool("isMoving", controller.IsMoving());
    }
}