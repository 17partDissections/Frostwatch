using Q17pD.Frostwatch.Interactive;
using Q17pD.Frostwatch.Inventory;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Q17pD.Frostwatch.Player
{
    public class PlayerItemHandler : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _items;
        private List<PickupableObject> _pickupableObjs = new List<PickupableObject>();
        private PickupableObject _branches;
        private int _currentItemIndex = -1;
        private Player _player;
        private AudioHandler _audioHandler;
        [Inject]private void Construct(AudioHandler audioHandler, PickupableObject branches)
        {
            _branches = branches;
            _player = GetComponent<Player>();
            _audioHandler = audioHandler;
            for (int i = 0; i < _items.Count; i++) _pickupableObjs.Add(null);
        }
        public void AddItem(int index, PickupableObject invoker)
        {            
            _currentItemIndex = index;
            if (_items[_currentItemIndex].TryGetComponent<Branches>(out Branches branches))
            {
                if(invoker.IsMultiple) { for (int i = 0; i < invoker.HasObjs(); i++) { branches.AddVisualObj(); } invoker.ClearVisualObjs(); }
                else branches.AddVisualObj();
            }
            else _pickupableObjs[_currentItemIndex] = invoker;
            _items[_currentItemIndex].SetActive(true);
            _player.PlayerAnimation.ChangeAnimation(_currentItemIndex);
            _player.IsHoldingItem = true;
        }
        public void ContinueAddingItem()
        {
            _items[_currentItemIndex].TryGetComponent<InventoryItem>(out InventoryItem inventoryItem);
            foreach (InventoryAction action in inventoryItem.Actions) { action.Init(_player, _audioHandler); }
            _player.PlayerCanvasHandler.UpdateActions(_player.CurrentCameraIndex, inventoryItem.Actions, inventoryItem.ActionsVectors);
        }
        public void DropItem() 
        {
            if(_pickupableObjs[_currentItemIndex] == null) _pickupableObjs[_currentItemIndex] = _branches;
            _pickupableObjs[_currentItemIndex].gameObject.SetActive(true);
            if(_items[_currentItemIndex].TryGetComponent<Branches>(out Branches MII)) 
            {
                int a = 0;
                int pickupableVisualObjs = _pickupableObjs[_currentItemIndex].HasObjs();
                int MIIObjs = MII.HasObjs();
                if (pickupableVisualObjs > 0 && (pickupableVisualObjs + MIIObjs > 5)) a = (pickupableVisualObjs + MIIObjs) - 5;
                for (int i = 0; i < MIIObjs - a; i++) { _pickupableObjs[_currentItemIndex].AddVisualObj(); MII.RemoveVisualObj(); }
                if (pickupableVisualObjs > 0 && (pickupableVisualObjs + MIIObjs > 5)) return;
            }
            
            _items[_currentItemIndex].SetActive(false);
            _currentItemIndex = -1;
            _player.PlayerAnimation.ChangeAnimation(_currentItemIndex);
            _player.PlayerCanvasHandler.HideActions();
            _player.IsHoldingItem = false;
        }
        public int GetItemAmount() { return _items.Count; }
    }
}
