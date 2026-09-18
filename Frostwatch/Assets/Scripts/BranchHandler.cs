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
        private Player.Player _player;

        [Inject] private void Construct(EventBus bus, Player.Player player)
        {
            bus.BranchThrownIntoForest += SpawnBranchBasedOnCamera;
            _player = player;
        }
        private IEnumerator Start()
        {
            WaitForSeconds cooldown = new WaitForSeconds(_cooldown);
            while (true) { yield return cooldown; SpawnBranchRandom(); }
        }
        private void SpawnBranchBasedOnCamera() { SpawnBranch(_player.CurrentCameraIndex); }
        private void SpawnBranchRandom() { SpawnBranch(UnityEngine.Random.Range(0, 4)); }
        private void SpawnBranch(int sideIndex)
        {
            GameObject branch = _branches.FirstOrDefault(x => !x.activeSelf);
            if (branch == null) return;
            List<BranchSpawnPoint> validPoints = _spawnPoints.Where(x => !x.IsBusy && x.SideIndex == sideIndex).ToList();
            if (validPoints.Count == 0) return;
            BranchSpawnPoint point = validPoints[UnityEngine.Random.Range(0, validPoints.Count)];
            point.IsBusy = true;
            branch.transform.position = point.transform.position + new Vector3(UnityEngine.Random.Range(0f, 1f), 0, UnityEngine.Random.Range(0f, 1f));
            PickupableObject pickupable = branch.GetComponentInChildren<PickupableObject>();
            if (pickupable != null && pickupable.ObjToMove != null) pickupable.ObjToMove.transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(-360, 360), 0);
            branch.SetActive(true);
        }
        //this IS fucking insane
    }
}