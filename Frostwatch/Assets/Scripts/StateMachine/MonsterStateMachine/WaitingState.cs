using System.Collections;
using UnityEngine;

namespace Q17pD.StateMachine.Local
{
    public class WaitingState : BaseState<MonsterStateMachine.MonsterStates>
    {
        private MonsterStateMachine _stateMachine;
        private Coroutine _waitingCoroutine;
        public WaitingState(MonsterStateMachine.MonsterStates state, MonsterStateMachine CurrentStateMachine) : base(state) { _stateMachine = CurrentStateMachine; }
        public override void Enter2State()
        {
            _waitingCoroutine = _stateMachine.StartCoroutine(WaitingCoroutine());
        }
        public override void Exit2State()
        {
            if (_waitingCoroutine != null)
            {
                _stateMachine.StopCoroutine(_waitingCoroutine);
                _waitingCoroutine = null;
            }
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
