using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Q17pD.Frostwatch.Inventory
{
    public class MultipleInventoryItem : InventoryItem
    {
        [SerializeField] private List<GameObject> _visualObjs;
        public int HasObjs()
        {
            int a = 0;
            foreach (GameObject obj in _visualObjs) if(obj.activeSelf) a++;
            return a;
        }
        public void AddVisualObj() { _visualObjs.FirstOrDefault(x=>!x.activeSelf).SetActive(true); }
        public void RemoveVisualObj() { _visualObjs.LastOrDefault(x=>x.activeSelf).SetActive(false); }
        public bool CanAddMoreVisualObj() { return _visualObjs.Any(x => !x.activeSelf); }
        
    }
}
