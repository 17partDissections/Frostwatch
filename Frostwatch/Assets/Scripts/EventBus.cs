using System;
using UnityEngine;

namespace Q17pD.Frostwatch
{
    public class EventBus
    {
        public Action<int> Shot;
        public Action<bool, float> CampfireStateChanged;
        public Action BranchThrownIntoForest;
        public Action BranchThrownIntoFire;
        public Action<int> OutOfBranches;
    }
}
