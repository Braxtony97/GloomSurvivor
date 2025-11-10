using System;
using System.Collections.Generic;
using Assets.GloomSurvivor.Scripts.UI.Services.Factory;
using GloomSurvivor.Scripts.Data.SaveLoad;
using GloomSurvivor.Scripts.Infrastructure.Factory;
using GloomSurvivor.Scripts.Infrastructure.Interfaces;
using GloomSurvivor.Scripts.Infrastructure.StaticData;
using GloomSurvivor.Scripts.Services;
using GloomSurvivor.Scripts.Services.PersistentProgress;

namespace GloomSurvivor.Scripts.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;

        public GameStateMachine(SceneLoader sceneLoader, ServiceLocator serviceLocator)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, serviceLocator),
                [typeof(LoadSceneState)] = new LoadSceneState(this, sceneLoader, serviceLocator.ResolveSingle<IGameFactory>(),
                serviceLocator.ResolveSingle<IPersistentProgressService>(),
                serviceLocator.ResolveSingle<IStaticDataService>(), serviceLocator.ResolveSingle<IUIFactory>()),
                [typeof(LoadProgressState)] = new LoadProgressState(this, serviceLocator.ResolveSingle<IPersistentProgressService>(),
                serviceLocator.ResolveSingle<ISaveLoadService>())
            };
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : IPayloadState<TPayload>
        {
            var state = GetState<TState>();
            state.Enter(payload);
        }

        public void Enter<TState>() where TState : IState
        {
            var state = GetState<TState>();
            state.Enter();
        }

        private TState GetState<TState>() where TState : IExitableState
        {
            _currentState?.Exit();
            var state = (TState)_states[typeof(TState)];
            _currentState = state;
            return state;
        }
    }
}