using System.Collections.Generic;
using UnityEngine;

namespace Q17pD.Frostwatch
{
    public class MonsterPoints : MonoBehaviour
    {
        [HideInInspector] public bool VectorBusy;
        public List<Transform> SpawnPoints, PatrolPoints;
    }
}
