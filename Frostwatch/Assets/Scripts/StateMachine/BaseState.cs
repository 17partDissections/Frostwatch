using System;

namespace Q17pD.StateMachine
{
    public abstract class BaseState<TState> where TState : Enum
    {
        public TState StateName;
        public Action<TState> ChangeStateAction;
        protected bool _isOnTransition;

        public BaseState(TState state) { StateName = state; }
        public abstract void Enter2State();
        public abstract void UpdateState();
        public abstract void Exit2State();
    }
}
