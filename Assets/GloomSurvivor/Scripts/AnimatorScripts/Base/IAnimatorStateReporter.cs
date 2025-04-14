using static Infrastructure.Enums;

namespace GloomSurvivor.Scripts.AnimatorScripts.Base
{
    public interface IAnimatorStateReporter
    {
        void EnteredState(int stateHash);
        void ExitedState(int stateHash);
        AnimatorState State { get; }
    }
}