using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Q17pD.Frostwatch
{
    public abstract class InventoryItem : MonoBehaviour
    {
        public int _index;
        [SerializeField] protected string _localizationKey;
        public List<InventoryAction> Actions = new List<InventoryAction>();
        protected virtual void Awake() { Actions.Add(new Drop()); }
    }
    public class Drop : InventoryAction
    {
        private Player.PlayerItemHandler _playerIH;
        public override void Init(Player.PlayerItemHandler playerIH)
        {
            _playerIH = playerIH;
            LocalizationKey = "Drop";
        }
        public override void Act() { _playerIH.DropItem(); }
    }
}
