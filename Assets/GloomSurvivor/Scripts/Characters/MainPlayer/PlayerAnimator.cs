using GloomSurvivor.Scripts.AnimatorScripts.Base;
using GloomSurvivor.Scripts.Infrastructure;
using UnityEngine;

namespace GloomSurvivor.Scripts.Characters.MainPlayer
{
    public class PlayerAnimator : MonoBehaviour, IAnimatorStateReporter
    {
        public void EnteredState(int stateHash)
        {
            throw new System.NotImplementedException();
        }

        public void ExitedState(int stateHash)
        {
            throw new System.NotImplementedException();
        }

        public Enums.AnimatorState State { get; }
    }
}