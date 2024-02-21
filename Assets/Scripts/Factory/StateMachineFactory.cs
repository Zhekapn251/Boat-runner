using Infrastructure;
using Infrastructure.Interfaces;
using UnityEngine;
using Zenject;

namespace Factory
{
    public class StateMachineFactory
    {
        private readonly IInstantiator _instantiator;

        public StateMachineFactory(IInstantiator instantiator) => 
            this._instantiator = instantiator;

        public TState Create<TState>() where TState : IState =>
            _instantiator.Instantiate<TState>();
    }
}