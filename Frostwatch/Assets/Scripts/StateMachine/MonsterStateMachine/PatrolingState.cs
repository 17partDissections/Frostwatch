using UnityEngine;

namespace Q17pD.StateMachine.Local
{
    public class PatrolingState : BaseState<MonsterStateMachine.MonsterStates>
    {
        private MonsterStateMachine _stateMachine;
        private float _i;
        public PatrolingState(MonsterStateMachine.MonsterStates state, MonsterStateMachine CurrentStateMachine) : base(state) { _stateMachine = CurrentStateMachine; }
        public override void Enter2State()
        {
            _stateMachine.Agent.isStopped = false;
            _stateMachine.CrawlToggle(2);
            _stateMachine.Agent.SetDestination(_stateMachine.CurrentDestinationPoint.position);
           _i = Time.time;
        }

        public override void Exit2State()
        {
        }

        public override void UpdateState()
        {
            if (Time.time - _i < 0.5f)
                return;
            if (_stateMachine.Agent.remainingDistance < 0.1 && !_stateMachine.Agent.isStopped)
            {
                _stateMachine.Agent.isStopped = true;
                ChangeStateAction(MonsterStateMachine.MonsterStates.WaitingState);
            }
        }
    }
}
