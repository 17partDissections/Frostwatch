using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Q17pD.Frostwatch
{
    public abstract class InventoryItem : MonoBehaviour
    {
        public int _index;
        [SerializeField] protected string _localizationKey;
        public List<ActionVectors> ActionsVectors;
        public List<InventoryAction> Actions = new List<InventoryAction>();
        protected virtual void Awake() { Actions.Add(new Drop()); }
    }
    [Serializable] public class ActionVectors { public List<bool> Vectors = new List<bool>{false, false, false, false}; }
    public class Drop : InventoryAction
    {
        private Player.Player _player;
        public override void Init(Player.Player player)
        {
            _player = player;
            LocalizationKey = "Drop";
        }
        public override void Act() { _player.PlayerItemHandler.DropItem(); }
    }
}
