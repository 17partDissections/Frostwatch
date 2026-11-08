using System.Collections.Generic;
using UnityEngine;

namespace Q17pD.Frostwatch
{
    public class MonsterPoints : MonoBehaviour
    {
        public int Vector;
        [HideInInspector] public bool VectorBusy;
        public List<Transform> SpawnPoints, PatrolPoints;
    }
}
