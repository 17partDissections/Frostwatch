using System;
using System.Collections.Generic;
using UnityEngine;

namespace Q17pD.Frostwatch
{
    public abstract class InventoryItem : MonoBehaviour
    {
        public int _index;
        [SerializeField] protected string _localizationKey;
        [SerializeField] private AudioClip _dropSound;
        public List<ActionVectors> ActionsVectors;
        public List<InventoryAction> Actions = new List<InventoryAction>();
        protected virtual void Awake() 
        {
            Drop drop = new Drop();
            drop.DropInit(_dropSound);
            Actions.Add(drop);
        }
    }
    [Serializable] public class ActionVectors { public List<bool> Vectors = new List<bool>{false, false, false, false}; }
    public class Drop : InventoryAction
    {
        private AudioClip _dropSound;
        private Player.Player _player;
        private AudioHandler _audioHandler;
        public override void Init(Player.Player player, AudioHandler handler)
        {
            
            _player = player;
            _audioHandler = handler;
            LocalizationKey = "Drop";
        }
        public void DropInit(AudioClip dropSound) { _dropSound = dropSound; }
        public override void Act() { _audioHandler.PlaySFX(_dropSound, 0); _player.PlayerItemHandler.DropItem(); }
    }
}
