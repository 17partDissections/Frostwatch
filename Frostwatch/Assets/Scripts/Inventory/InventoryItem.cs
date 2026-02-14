using System;
using System.Collections.Generic;
using UnityEngine;

namespace Q17pD.Frostwatch.Inventory
{
    public abstract class InventoryItem : MonoBehaviour
    {
        public int _index;
        [SerializeField] protected string _localizationKey;
        [SerializeField] private AudioClip _dropSound;
        public List<ActionVectors> ActionsVectors;
        public List<InventoryAction> Actions = new List<InventoryAction>();
        protected Drop _drop = new Drop();
        protected virtual void Awake() 
        {
            _drop.SoundInit(_dropSound);
            Actions.Add(_drop);
        }
    }
    [Serializable] public class ActionVectors { public List<bool> Vectors = new List<bool>{false, false, false, false}; }
    public class Drop : InventoryAction
    {
        private AudioClip _actSound;
        private Player.Player _player;
        private AudioHandler _audioHandler;
        public override void Init(Player.Player player, AudioHandler handler)
        {
            _player = player;
            _audioHandler = handler;
            LocalizationKey = "Drop";
        }
        public void SoundInit(AudioClip actSound) { _actSound = actSound; }
        public override void Act() { _audioHandler.PlaySFX(_actSound, 0); _player.PlayerItemHandler.DropItem(); }
    }
}
