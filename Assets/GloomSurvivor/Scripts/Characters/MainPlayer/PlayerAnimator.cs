using GloomSurvivor.Scripts.AnimatorScripts.Base;
using GloomSurvivor.Scripts.Infrastructure;
using UnityEngine;

namespace GloomSurvivor.Scripts.Characters.MainPlayer
{
    public class PlayerAnimator : MonoBehaviour, IAnimatorStateReporter
    {
        private static readonly int _hitHash = Animator.StringToHash("Hit");
        private static readonly int _dieHash = Animator.StringToHash("Die");
        private static readonly int _attackNormalHash = Animator.StringToHash("Attack_Normal");

        public bool IsAttacking { get; set; }
        
        private Animator _animator;


        private void Awake() => 
            _animator = GetComponent<Animator>();

        public void EnteredState(int stateHash)
        {
            throw new System.NotImplementedException();
        }

        public void ExitedState(int stateHash)
        {
            throw new System.NotImplementedException();
        }

        public Enums.AnimatorState State { get; }

        public void PlayHit() => 
            _animator.SetTrigger(_hitHash);

        public void PlayDeath() => 
            _animator.SetTrigger(_dieHash);
        
        public void PlayAttack() => 
            _animator.SetTrigger(_attackNormalHash); 
    }
}