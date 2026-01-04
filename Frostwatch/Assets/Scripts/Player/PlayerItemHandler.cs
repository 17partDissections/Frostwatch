using System.Collections.Generic;
using UnityEngine;

namespace Q17pD.Frostwatch.Player
{
    public class PlayerItemHandler : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _items;
        private int _currentItemIndex = -1;
        private Player _player;
        private void Start() { _player = GetComponent<Player>(); } 
        public bool IsPlayerHoldingItem() { if (_currentItemIndex != -1) return true; return false; }
        public void AddItem(int index)
        {
            _currentItemIndex = index;
            _items[_currentItemIndex].SetActive(true);
            _player.PlayerAnimation.ChangeAnimation(_currentItemIndex);
            _items[_currentItemIndex].TryGetComponent<InventoryItem>(out InventoryItem inventoryItem);
            foreach (InventoryAction action in inventoryItem.Actions) { action.Init(this); }
            _player.PlayerCanvasHandler.SetActions(inventoryItem.Actions);

        }
        public void DropItem() { _items[_currentItemIndex].SetActive(false); _currentItemIndex = -1; _player.PlayerAnimation.ChangeAnimation(_currentItemIndex); }
        public int GetItemAmount() { return _items.Count; }
    }
}
