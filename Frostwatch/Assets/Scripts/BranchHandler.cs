using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using Q17pD.Frostwatch.Interactive;

namespace Q17pD.Frostwatch
{
    public class BranchHandler : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _branches;
        [SerializeField] private List<BranchSpawnPoint> _spawnPoints;
        [Range(30, 120)][SerializeField] private int _cooldown;
        private IEnumerator Start()
        {
            WaitForSeconds cooldown = new WaitForSeconds(_cooldown);
            while (true)
            {
                yield return cooldown;
                GameObject freeBranch = _branches.FirstOrDefault(x => !x.activeSelf);
                if (freeBranch == null) continue;
                List<BranchSpawnPoint> freePoints = _spawnPoints.Where(x => !x.IsBusy).ToList();
                if (freePoints.Count == 0) continue;
                BranchSpawnPoint point = freePoints[Random.Range(0, freePoints.Count)];
                point.IsBusy = true;
                freeBranch.transform.position = point.transform.position + new Vector3(Random.Range(0f, 1f), 0, Random.Range(0f, 1f));
                freeBranch.GetComponentInChildren<PickupableObject>().ObjToMove.transform.Rotate(new Vector3(0, Random.Range(-360, 360), 0));
                freeBranch.SetActive(true);
            }
        }
    }
}
