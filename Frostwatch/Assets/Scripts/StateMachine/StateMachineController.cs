using System;
using System.Collections.Generic;
using UnityEngine;

namespace Q17pD.StateMachine
{
    public abstract class StateMachineController<TState> : MonoBehaviour where TState : Enum
    {
        protected Dictionary<TState, BaseState<TState>> States = new Dictionary<TState, BaseState<TState>>();
        protected BaseState<TState> CurrentState;
        protected bool _isOnTransition;

        protected void StartMachine(TState StartingState)
        {
            if (CurrentState != null) CurrentState.ChangeStateAction -= Transition2nextState;
            CurrentState = States[StartingState];
            CurrentState.ChangeStateAction += Transition2nextState;
            CurrentState.Enter2State();
        }
        private void Update() { if (!_isOnTransition) CurrentState.UpdateState(); }
        private void Transition2nextState(TState state)
        {
            _isOnTransition = true;
            CurrentState.ChangeStateAction -= Transition2nextState;
            CurrentState.Exit2State();
            CurrentState = States[state];
            CurrentState.ChangeStateAction += Transition2nextState;
            CurrentState.Enter2State();
            _isOnTransition = false;
        }

        protected void ChangeActionFromChildren(TState tState) { CurrentState.ChangeStateAction.Invoke(tState); }
    }
}
