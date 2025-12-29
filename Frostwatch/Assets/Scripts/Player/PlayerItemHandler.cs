using System.Collections.Generic;
using UnityEngine;

namespace Q17pD.Frostwatch.Player
{
    public class PlayerItemHandler : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _items;
        private int _currentItemIndex = -1;
        private PlayerAnimation _playerAnimation;
        private void Start() { _playerAnimation = GetComponent<PlayerAnimation>(); }
        public bool IsPlayerHoldingItem() { if (_currentItemIndex != -1) return true; return false; }
        public void AddItem(int index)
        {
            _currentItemIndex = index;
            _items[_currentItemIndex].SetActive(true);
            _playerAnimation.ChangeAnimation(_currentItemIndex);
        }
        public void DropItem() { _items[_currentItemIndex].SetActive(false); _currentItemIndex = -1; }
        public int GetItemAmount() { return _items.Count; }
    }
}
