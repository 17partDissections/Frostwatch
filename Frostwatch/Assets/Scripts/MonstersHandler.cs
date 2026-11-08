using Q17pD.StateMachine.Local;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Q17pD.Frostwatch
{
    public class MonstersHandler : MonoBehaviour
    {
        [SerializeField] private List<MonsterStateMachine> _monsters;
        [SerializeField] private List<MonsterPoints> _monsterPoints;
        [SerializeField] private int _sleepMin, _sleepMax, _lifetimeMin, _lifetimeMax;
        private Player.Player _player;

        [Inject] private void Construct(Player.Player player, EventBus bus) { _player = player; bus.Shot += Try2KillMonster; }
        public void StartMonstersCoroutine() { StartCoroutine(MonstersCoroutine()); }
        private IEnumerator MonstersCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(_sleepMin, _sleepMax));
                List<MonsterStateMachine> availableMonsters = _monsters.Where(x => !x.gameObject.activeSelf).ToList();
                List<MonsterPoints> availablePoints = _monsterPoints.Where(x => !x.VectorBusy).ToList();
                if (availableMonsters.Count == 0 || availablePoints.Count == 0) continue;
                MonsterStateMachine monster = availableMonsters[Random.Range(0, availableMonsters.Count)];
                MonsterPoints points = availablePoints[Random.Range(0, availablePoints.Count)];
                points.VectorBusy = true;
                monster.Lifetime = Random.Range(_lifetimeMin, _lifetimeMax);
                monster.Vector = points.Vector;
                monster.transform.position = points.SpawnPoints[Random.Range(0, points.SpawnPoints.Count)].position;
                monster.PatrolPoints = points.PatrolPoints;
                monster.Player = _player;
                monster.CurrentMonsterPoints = points;
                monster.MonstersHandler = this;
                monster.gameObject.SetActive(true);
            }
        }
        private void Try2KillMonster(int shotDirection)
        {
            foreach (MonsterStateMachine monster in _monsters)
            {
                if (!monster.gameObject.activeInHierarchy || !monster.IsAttacking) continue;
                if (shotDirection == monster.Vector) { monster.ChangeStateFromMachine(MonsterStateMachine.MonsterStates.DeathState); }
            }
        }
        public void RemoveMonster(MonsterStateMachine monster)
        {
            MonsterPoints points = monster.CurrentMonsterPoints;
            points.VectorBusy = false;
            monster.CurrentMonsterPoints = null;
            monster.gameObject.SetActive(false);
        }
    }
}
