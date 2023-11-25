using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [Header("References")]
    public EnemyController controller;
    public Animator animator;

    void Start()
    {
        animator.keepAnimatorStateOnDisable = true;
    }

    void Update()
    {
        MovementAnimation();
    }

    void MovementAnimation()
    {
        animator.SetBool("isMoving", controller.IsMoving());
    }

    public void PlayDeathAnimation()
    {
        animator.Play("Die");
    }

    public void PlayAttackAnimation()
    {
        animator.Play("Attack");
    }

    public void SetIdleAnimation()
    {
        animator.Play("Idle");
    }
}
