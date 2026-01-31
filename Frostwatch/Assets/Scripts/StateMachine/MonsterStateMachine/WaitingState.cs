using System.Collections;
using UnityEngine;

namespace Q17pD.StateMachine.Local
{
    public class WaitingState : BaseState<MonsterStateMachine.MonsterStates>
    {
        private MonsterStateMachine _stateMachine;
        public WaitingState(MonsterStateMachine.MonsterStates state, MonsterStateMachine CurrentStateMachine) : base(state) { _stateMachine = CurrentStateMachine; }
        public override void Enter2State()
        {
            _stateMachine.StartCoroutine(WaitingCoroutine());
        }
        public override void Exit2State()
        {
        }
        public override void UpdateState()
        {
        }
        private IEnumerator WaitingCoroutine()
        {
            WaitForSeconds sleep = new WaitForSeconds(Random.Range(2, 6));
            yield return sleep;
            bool x = false;
            while(!x)
            {
                Transform temp = null;
                temp = _stateMachine.PatrolPoints[Random.Range(0, _stateMachine.PatrolPoints.Count)];
                if(temp != _stateMachine.CurrentDestinationPoint) { _stateMachine.CurrentDestinationPoint = temp; x = true; }
            }
            ChangeStateAction(MonsterStateMachine.MonsterStates.PatrolingState);
        }
    }
}
