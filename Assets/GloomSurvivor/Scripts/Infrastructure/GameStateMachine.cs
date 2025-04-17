using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace GloomSurvivor.Scripts.Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _currentState;

        public GameStateMachine()
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this),
            };
        }
        
        public void Enter<TState>() where TState : IState
        {
            _currentState?.Exit(); 
            var state = _states[typeof(TState)];
            _currentState = state;
            state.Enter();
        } 
    }
}