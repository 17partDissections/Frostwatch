using Q17pD.Frostwatch;
using Q17pD.Frostwatch.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Q17pD.StateMachine.Local
{
    public class MonsterStateMachine : StateMachineController<MonsterStateMachine.MonsterStates>
    {
        [HideInInspector] public Player Player;
        [HideInInspector] public int Lifetime, Vector;
        [HideInInspector] public MonsterPoints CurrentMonsterPoints;
        [HideInInspector] public MonstersHandler MonstersHandler;
        [HideInInspector] public List<Transform> PatrolPoints;
        [HideInInspector] public Transform CurrentDestinationPoint;
        [HideInInspector] public NavMeshAgent Agent;
        [HideInInspector] public bool IsAttacking, IsDied, AfterAttack = true;
        [SerializeField] private bool _hasCrawlAnim;
        [HideInInspector] public Animator Animator;
        private int _state, _crawl, _walking, _running, _hit, _attack;
        private bool _isRunning;
        private Vector3 _spawnPoint;

        public enum MonsterStates
        {
            WaitingState,
            PatrolingState,
            AttackingState,
            DeathState,
            ScreamingState,
        }

        private void Start()
        {
            Animator = GetComponent<Animator>();
            if (Animator == null) Animator = GetComponentInChildren<Animator>();
            _state = Animator.StringToHash("State");
            _crawl = Animator.StringToHash("Crawl");
            _walking = Animator.StringToHash("Walking");
            _running = Animator.StringToHash("Running");
            //_hit = Animator.StringToHash("Hit");
            //_attack = Animator.StringToHash("Attack");
            States.Add(MonsterStates.WaitingState, new WaitingState(MonsterStates.WaitingState, this));
            States.Add(MonsterStates.PatrolingState,new PatrolingState(MonsterStates.PatrolingState, this));
            States.Add(MonsterStates.AttackingState, new AttackingState(MonsterStates.AttackingState, this));
            States.Add(MonsterStates.DeathState, new DeathState(MonsterStates.DeathState, this));
            StartMachine(MonsterStates.WaitingState);
            StartCoroutine(CheckAgentVelocityCoroutine());
        }
        private void OnEnable()
        {
            _spawnPoint = transform.position;
            Agent = GetComponent<NavMeshAgent>();
            Agent.enabled = true; Agent.isStopped = false;
            StartCoroutine(LifetimeCoroutine());

        }

        public void ChangeStateFromMachine(MonsterStates state) { ChangeActionFromChildren(state); }

        //for what fucking purpose i made this void 
        //if i can just _stateMachine.StartCoroutine(...);
        //the most ironic is i made it TEN MONTHS AGO and didnt even notice it all this time
        //...and you now what? ill just leave it here 
        public void StartCoroutineFromMachine(IEnumerator enumerator) { StartCoroutine(enumerator); }
        private IEnumerator CheckAgentVelocityCoroutine()
        {
            while (true)
            {
                yield return new WaitUntil(() => Agent.velocity.magnitude > 0.1f);
                Animator.SetBool(_running, _isRunning); Animator.SetBool(_walking, !_isRunning);
                yield return new WaitWhile(() => Agent.velocity.magnitude > 0);
                Animator.SetBool(_running, false); Animator.SetBool(_walking, false);
            }
        }
        public void CrawlToggle(int intv)
        {
            if (_hasCrawlAnim)
            {
                bool value = false;
                if (intv == 0) value = false;
                else if (intv == 1) value = true;
                else if (intv == 2) value = Random.Range(0, 2) == 1;
                Animator.SetBool(_crawl, value);
                Animator.SetBool(_state, !value);
            }
        }
        public void RunToggle(int intv) 
        {
            bool value = false;
            if (intv == 0) value = false;
            else if (intv == 1) value = true;
            Animator.SetBool(_running, value);
            Animator.SetBool(_walking, !value);
        }
        private IEnumerator LifetimeCoroutine()
        {
            yield return new WaitForSeconds(Lifetime);
            Agent.speed = Agent.speed * 3;
            CurrentDestinationPoint = Player.transform;
            ChangeActionFromChildren(MonsterStates.AttackingState);
        }
    }
}

