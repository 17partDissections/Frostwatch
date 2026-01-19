using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Q17pD.Frostwatch.Player
{
    public class PlayerItemHandler : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _items;
        private List<GameObject> _pickupableObjs = new List<GameObject>();
        private int _currentItemIndex = -1;
        private Player _player;
        private AudioHandler _audioHandler;
        [Inject]private void Construct(AudioHandler audioHandler)
        {
            _player = GetComponent<Player>();
            _audioHandler = audioHandler;
            for (int i = 0; i < _items.Count; i++) _pickupableObjs.Add(null);
        }
        public void AddItem(int index, GameObject invoker)
        {            
            _currentItemIndex = index;
            if(_items[_currentItemIndex].TryGetComponent<MultipleInventoryItem>(out MultipleInventoryItem MII)) MII.AddVisualObj();
            else _pickupableObjs[_currentItemIndex] = invoker;
            _items[_currentItemIndex].SetActive(true);
            _player.PlayerAnimation.ChangeAnimation(_currentItemIndex);
            _player.IsHoldingItem = true;
        }
        public void ContinueAddingItem()
        {
            Debug.Log("asd");
            _items[_currentItemIndex].TryGetComponent<InventoryItem>(out InventoryItem inventoryItem);
            foreach (InventoryAction action in inventoryItem.Actions) { action.Init(_player, _audioHandler); }
            _player.PlayerCanvasHandler.UpdateActions(_player.CurrentCameraIndex, inventoryItem.Actions, inventoryItem.ActionsVectors);
        }
        public void DropItem() 
        {
            _items[_currentItemIndex].SetActive(false);
            _pickupableObjs[_currentItemIndex].SetActive(true);
            _currentItemIndex = -1;
            _player.PlayerAnimation.ChangeAnimation(_currentItemIndex);
            _player.PlayerCanvasHandler.HideActions();
            _player.IsHoldingItem = false;
        }
        public int GetItemAmount() { return _items.Count; }
    }
}
