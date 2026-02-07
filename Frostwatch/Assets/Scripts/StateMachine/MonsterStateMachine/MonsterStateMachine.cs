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
        [HideInInspector] public int Lifetime;
        [HideInInspector] public List<Transform> PatrolPoints;
        [HideInInspector] public Transform CurrentDestinationPoint;
        [HideInInspector] public NavMeshAgent Agent;
        [HideInInspector] public bool AfterAttack = true;
        [SerializeField] private bool _hasCrawlAnim;
        private Animator _animator;
        private int _state, _crawl, _walking, _running, _hit, _death, _attack;
        private bool _isRunning;
        private Vector3 _spawnPoint;

        public enum MonsterStates
        {
            WaitingState,
            PatrolingState,
            RunningOnSpawnState,
            AttackingState,
            ScreamingState,
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            if (_animator == null) _animator = GetComponentInChildren<Animator>();
            _state = Animator.StringToHash("State"); _crawl = Animator.StringToHash("Crawl");
            _walking = Animator.StringToHash("Walking"); _running = Animator.StringToHash("Running");
            _hit = Animator.StringToHash("Hit"); _death = Animator.StringToHash("Death");
            _attack = Animator.StringToHash("Attack");
            Agent = GetComponent<NavMeshAgent>();
            WaitingState WaitingState = new WaitingState(MonsterStates.WaitingState, this);
            States.Add(MonsterStates.WaitingState, WaitingState);
            PatrolingState PatrolingState = new PatrolingState(MonsterStates.PatrolingState, this);
            States.Add(MonsterStates.PatrolingState, PatrolingState);
            AttackingState AttackingState = new AttackingState(MonsterStates.AttackingState, this);
            States.Add(MonsterStates.AttackingState, AttackingState);
            StartMachine(MonsterStates.WaitingState); StartCoroutine(CheckAgentVelocityCoroutine());
        }
        private void OnEnable()
        {
            _spawnPoint = transform.position;
            StartCoroutine(LifetimeCoroutine());
        }

        public void ChangeStateFromMachine(MonsterStates state) { ChangeActionFromChildren(state); }
        public void StartCoroutineFromMachine(IEnumerator enumerator) { StartCoroutine(enumerator); }
        private IEnumerator CheckAgentVelocityCoroutine()
        {
            while (true)
            {
                yield return new WaitUntil(() => Agent.velocity.magnitude > 0.1f);
                _animator.SetBool(_running, _isRunning); _animator.SetBool(_walking, !_isRunning);
                yield return new WaitWhile(() => Agent.velocity.magnitude > 0);
                _animator.SetBool(_running, false); _animator.SetBool(_walking, false);
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
                _animator.SetBool(_crawl, value);
                _animator.SetBool(_state, !value);
            }
        }
        public void RunToggle(int intv) 
        {
            bool value = false;
            if (intv == 0) value = false;
            else if (intv == 1) value = true;
            _animator.SetBool(_running, value);
            _animator.SetBool(_walking, !value);
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

