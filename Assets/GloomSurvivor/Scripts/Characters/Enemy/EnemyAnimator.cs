using System;
using GloomSurvivor.Scripts.AnimatorScripts.Base;
using GloomSurvivor.Scripts.Infrastructure;
using UnityEditor.Animations;
using UnityEngine;

namespace GloomSurvivor.Scripts.Characters.Enemy
{
    public class EnemyAnimator : MonoBehaviour, IAnimatorStateReporter
    {
        public Enums.AnimatorState State { get; private set; }
        
        public event Action<Enums.AnimatorState> StateEntered;
        public event Action<Enums.AnimatorState> StateExited;
        
        private static readonly int _dieHash = Animator.StringToHash("Die");
        private static readonly int _hitHash = Animator.StringToHash("Hit");
        private static readonly int _attackDefaultHash = Animator.StringToHash("Attack_Default");
        private static readonly int _attackChargeHash = Animator.StringToHash("Attack_Charge");
        private static readonly int _isMovingHash = Animator.StringToHash("IsMoving");
        private static readonly int _speedHash = Animator.StringToHash("Speed");

        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _moveStateHash = Animator.StringToHash("MoveBlend");
        private readonly int _defaultAttackStateHash = Animator.StringToHash("Attack_Default"); 
        private readonly int _chargeAttackStateHash = Animator.StringToHash("Attack_Charge"); 
        private readonly int _dieStateHash = Animator.StringToHash("Die"); 

        private Animator _animator;

        private void Awake() => 
            _animator = GetComponent<Animator>();

        #region SetParameters

        public void Move(float speed)
        {
            _animator.SetBool(_isMovingHash, true);
            _animator.SetFloat(_speedHash, speed);
        }

        public void PlayDeath() => 
            _animator.SetTrigger(_dieHash);

        public void PlayHit() => 
            _animator.SetTrigger(_hitHash);

        public void StopMoving() => 
            _animator.SetBool(_isMovingHash, false);

        public void PlayDefaultAttack() => 
            _animator.SetTrigger(_attackDefaultHash);

        #endregion
        
        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash) => 
            StateExited?.Invoke(State);

        private Enums.AnimatorState StateFor(int stateHash)
        {
            Enums.AnimatorState state;
            if (stateHash == _idleStateHash)
                state = Enums.AnimatorState.Idle;
            else if (stateHash == _moveStateHash)
                state = Enums.AnimatorState.Walking;
            else if (stateHash == _defaultAttackStateHash)
                state = Enums.AnimatorState.AttackDefault;
            else if (stateHash == _dieStateHash)
                state = Enums.AnimatorState.Died;
            else
                state = Enums.AnimatorState.Unknown;

            return state;
        }
    }
}