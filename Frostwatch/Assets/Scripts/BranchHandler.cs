using Q17pD.Frostwatch.Interactive;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Q17pD.Frostwatch
{
    public class BranchHandler : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _branches;
        [SerializeField] private List<BranchSpawnPoint> _spawnPoints;
        [Range(1, 120)][SerializeField] private int _cooldown;

        [Inject] private void Construct(EventBus bus) { bus.BranchThrownIntoForest += SpawnBranch; }
        private IEnumerator Start()
        {
            WaitForSeconds cooldown = new WaitForSeconds(_cooldown);
            while (true)
            {
                yield return cooldown;
                SpawnBranch();
            }
        }
        public void SpawnBranch()
        {
            GameObject branch = _branches.FirstOrDefault(x => !x.activeSelf);
            List<BranchSpawnPoint> freePoints = _spawnPoints.Where(x => !x.IsBusy).ToList();
            if (freePoints.Count == 0) return;
            BranchSpawnPoint point = freePoints[UnityEngine.Random.Range(0, freePoints.Count)];
            point.IsBusy = true;
            branch.transform.position = point.transform.position + new Vector3(UnityEngine.Random.Range(0f, 1f), 0, UnityEngine.Random.Range(0f, 1f));
            branch.GetComponentInChildren<PickupableObject>().ObjToMove.transform.Rotate(0, UnityEngine.Random.Range(-360, 360), 0);
            branch.SetActive(true);
        }
    }
}
