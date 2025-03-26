using UnityEngine;

public class CannonAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    private readonly int _attackHash = Animator.StringToHash("Attack");

    public void PlayAttack() => 
        animator.SetTrigger(_attackHash);
}