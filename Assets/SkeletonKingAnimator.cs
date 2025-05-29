using System;
using GloomSurvivor.Scripts.AnimatorScripts.Base;
using UnityEngine;
using static GloomSurvivor.Scripts.Infrastructure.Enums;

public class SkeletonKingAnimator : MonoBehaviour, IAnimatorStateReporter
{
    public event Action<AnimatorState> StateEntered;
    public event Action<AnimatorState> StateExited;
    public AnimatorState State { get; private set; }
    
    [SerializeField] private Animator _animatorController;
    [SerializeField] private CharacterController _characterController;
    
    private static readonly int MoveHash = Animator.StringToHash("Walking");
    private static readonly int AttackHash = Animator.StringToHash("AttackNormal");
    private static readonly int HitHash = Animator.StringToHash("Hit");
    private static readonly int DieHash = Animator.StringToHash("Die");
 
    private readonly int _idleStateHash = Animator.StringToHash("Idle");
    private readonly int _attackStateHash = Animator.StringToHash("Attack");
    private readonly int _walkingStateHash = Animator.StringToHash("Walk");
    private readonly int _deathStateHash = Animator.StringToHash("Die");

    public void EnteredState(int stateHash)
    {
        State = GetAnimatorState(stateHash);
        StateEntered?.Invoke(State);
    }
    
    public void ExitedState(int stateHash) => 
        StateExited?.Invoke(GetAnimatorState(stateHash));

    private void Update()
    {
        _animatorController.SetFloat(MoveHash, _characterController.velocity.magnitude, 0.1f, Time.deltaTime);
    }
    
    public bool IsAttacking => 
        State == AnimatorState.Attack;
    
    public void PlayHit() => 
        _animatorController.SetTrigger(HitHash);
 
    public void PlayAttack() => 
        _animatorController.SetTrigger(AttackHash);

    public void PlayDeath() =>  
        _animatorController.SetTrigger(DieHash);

    public void ResetToIdle() => 
        _animatorController.Play(_idleStateHash, -1);

    private AnimatorState GetAnimatorState(int stateHash)
    {
        AnimatorState state;

        if (stateHash == _idleStateHash)
            state = AnimatorState.Idle;
        else if (stateHash == _attackStateHash)
            state = AnimatorState.Attack;
        else if (stateHash == _walkingStateHash)
            state = AnimatorState.Walking;
        else if (stateHash == _deathStateHash)
            state = AnimatorState.Died;
        else
            state = AnimatorState.Unknown;

        return state;
    }
}
