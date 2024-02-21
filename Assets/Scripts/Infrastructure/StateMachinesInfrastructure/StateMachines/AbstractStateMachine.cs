using System;
using System.Collections.Generic;
using Infrastructure.Interfaces;
using UnityEngine;

namespace Infrastructure.StateMachinesInfrastructure.StateMachines
{
    public abstract class AbstractStateMachine<TState> where TState : class, IState
    {
        private readonly Dictionary<Type, TState> _states;
        public TState CurrentState { get; private set; }
        public event Action<IState> OnStateChanged;
        protected AbstractStateMachine() => _states = new Dictionary<Type, TState>();
        public void RegisterState(TState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void EnterState<T>() where T : class, TState
        {
            TState state = ChangeState<T>();
            state.Enter();
        }
        private T ChangeState<T>() where T : class, TState
        {
            CurrentState?.Exit();

            T state = GetState<T>();
            CurrentState = state;
            OnStateChanged?.Invoke(CurrentState);
            return state;
        }

        public T GetState<T>() where T : class, TState => _states[typeof(T)] as T;
        
        public void ClearStates()
        {
            _states.Clear();
        }
        
        public void PrintStates()
        {
            foreach (var state in _states)
            {
                Debug.Log(state.Key);
            }
        }
           
    }
}