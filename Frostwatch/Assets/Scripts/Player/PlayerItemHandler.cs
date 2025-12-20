using System.Collections.Generic;
using UnityEngine;

namespace Q17pD.Frostwatch
{
    public class PlayerItemHandler : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _items;
        private int _currentItemIndex;

        public bool IsPlayerHoldingItem() { foreach(GameObject obj in _items) if(obj.activeSelf) return true; return false; }
        public void AddItem(int index) {_items[index].SetActive(true); _currentItemIndex = index; }
    }
}
