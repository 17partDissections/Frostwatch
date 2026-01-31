using Q17pD.StateMachine.Local;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Q17pD.Frostwatch
{
    public class MonstersHandler : MonoBehaviour
    {
        [SerializeField] private List<MonsterStateMachine> _monsters;
        [SerializeField] private List<MonsterPoints> _monsterPoints;
        [SerializeField] private int _sleepMin, _sleepMax;
        private Player.Player _player;

        [Inject] private void Construct(Player.Player player) {  _player = player; }

        private IEnumerator Start()
        {
            while(true)
            {
                int s = UnityEngine.Random.Range(_sleepMin, _sleepMax);
                Debug.Log(s);
                //yield return new WaitForSeconds(UnityEngine.Random.Range(30, 150));
                yield return new WaitForSeconds(s);
                if (_monsters.Any(x => !x.gameObject.activeSelf)) 
                {
                    Debug.Log("spawn");
                    MonsterStateMachine choosedMonster = _monsters.Where(x => !x.gameObject.activeSelf).ToList()[UnityEngine.Random.Range(0, _monsters.Count(x => !x.gameObject.activeSelf))];
                    MonsterPoints choosedVector = _monsterPoints.Where(x => !x.VectorBusy).ToList()[UnityEngine.Random.Range(0, _monsterPoints.Count(x => !x.VectorBusy))];
                    choosedVector.VectorBusy = true;
                    choosedMonster.Lifetime = UnityEngine.Random.Range(30, 32);
                    Debug.Log(choosedMonster.Lifetime);
                    choosedMonster.transform.position = choosedVector.SpawnPoints[UnityEngine.Random.Range(0, choosedVector.SpawnPoints.Count)].position;
                    choosedMonster.PatrolPoints = choosedVector.PatrolPoints;
                    choosedMonster.Player = _player;
                    choosedMonster.gameObject.SetActive(true);
                }
            }
        }
        public void RemoveMonster(MonsterStateMachine monster)
        {
            monster.gameObject.SetActive(false);
            foreach (MonsterPoints vector in _monsterPoints) if (vector.VectorBusy && monster.PatrolPoints[0] == vector.PatrolPoints[0]) vector.VectorBusy = false;
        }
    }
}
