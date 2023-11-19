using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [Header("References")]
    public EnemyController controller;
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
