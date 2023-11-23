using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAnimator : MonoBehaviour
{
    [Header("References")]
    public PlayerController controller;
    public Animator animator;
    public TwoBoneIKConstraint GunPositionConstraint;
    public GameObject GunConstraintTarget;

    void Update()
    {
        MovementAnimation();
    }

    void MovementAnimation()
    {
        animator.SetBool("isMoving", controller.IsMoving());
    }

    public void PlayMeleeAttackAnimation()
    {
        animator.Play("Hammer Attack");
    }

    public void PlayShootAnimation()
    {
        animator.Play("PistolShoot");

        GunConstraintTarget.transform.DOLocalMoveZ(0.2f, 0.1f).OnComplete(() => {
            GunConstraintTarget.transform.DOLocalMoveZ(0.3f, 0.03f);
        });
    }

    public void SetWeaponIdleAnimation(bool state = false)
    {
        animator.SetBool("weaponState", state);

        if (state == false)
            GunPositionConstraint.weight = 0;
        else
            GunPositionConstraint.weight = 1;
    }

    public void PlayDeathAnimation()
    {
        animator.Play("Die");
    }
}
