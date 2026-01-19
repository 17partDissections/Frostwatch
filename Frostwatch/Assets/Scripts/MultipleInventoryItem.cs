using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Q17pD.Frostwatch
{
    public class MultipleInventoryItem : InventoryItem
    {
        [SerializeField] private List<GameObject> _visualObjs;
        public void AddVisualObj() { _visualObjs.FirstOrDefault(x=>!x.activeSelf).SetActive(true); }
        public bool CanAddMoreVisualObj() { return _visualObjs.Any(x => !x.activeSelf); }
    }
}
