using Q17pD.Frostwatch;
using System.Collections;
using UnityEngine;

namespace Q17pD.StateMachine.Local
{
    public class DeathState : BaseState<MonsterStateMachine.MonsterStates>
    {
        private MonsterStateMachine _stateMachine;
        public DeathState(MonsterStateMachine.MonsterStates state, MonsterStateMachine CurrentStateMachine) : base(state) { _stateMachine = CurrentStateMachine; }
        public override void Enter2State()
        {
            _stateMachine.IsAttacking = false; _stateMachine.IsDied = true;
            _stateMachine.Agent.isStopped = true;
            _stateMachine.Agent.ResetPath();
            _stateMachine.Agent.enabled = false;
            _stateMachine.StopAllCoroutines();
            _stateMachine.StartCoroutine(DeathCoroutine());
        }

        public override void Exit2State()
        {
        }

        public override void UpdateState()
        {
        }
        private IEnumerator DeathCoroutine()
        {
            _stateMachine.Animator.SetTrigger(Animator.StringToHash("Death"));
            yield return new WaitForSeconds(5);
            //spawn food
            yield return new WaitForSeconds(25);
            _stateMachine.MonstersHandler.RemoveMonster(_stateMachine);
        }
    }
}
