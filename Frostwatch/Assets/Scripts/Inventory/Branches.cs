using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Q17pD.Frostwatch.Inventory
{
    public class Branches : InventoryItem
    {
        public Player.Player Player;
        [SerializeField] private List<GameObject> _visualObjs;
        [SerializeField] private AudioClip _dropIntoForestSound, _dropIntoFireSound;
        [SerializeField] private EndGame _endGame;

        public int HasObjs()
        {
            int a = 0;
            foreach (GameObject obj in _visualObjs) if (obj.activeSelf) a++;
            return a;
        }
        public void AddVisualObj() { _visualObjs.FirstOrDefault(x => !x.activeSelf).SetActive(true); }
        public void RemoveVisualObj()
        {
            GameObject obj = _visualObjs.LastOrDefault(x => x.activeSelf);
            obj.SetActive(false);
            if (!_visualObjs.Any(x => x.activeSelf)) Player.EventBus.OutOfBranches?.Invoke(-1);
        }
        protected override void Awake()
        {
            base.Awake();
            DropIntoForest dropIntoForest = new DropIntoForest();
            dropIntoForest.SoundInit(_dropIntoForestSound);
            dropIntoForest.CustomInit(this, Player.EventBus);
            DropIntoFire dropIntoFire = new DropIntoFire();
            dropIntoFire.SoundInit(_dropIntoFireSound);
            dropIntoFire.CustomInit(this, Player.EventBus, _endGame);
            Actions.AddRange(new List<InventoryAction> { dropIntoForest, dropIntoFire });
        }
    }
    public class DropIntoForest : InventoryAction
    {
        private AudioClip _actSound;
        private AudioHandler _audioHandler;
        private Branches _branches;
        private EventBus _eventBus;

        public override void Init(Player.Player player, AudioHandler handler)
        {
            _audioHandler = handler;
            LocalizationKey = "DropForest";
        }
        public void SoundInit(AudioClip actSound) { _actSound = actSound; }
        public void CustomInit(Branches branches, EventBus eventBus) { _branches = branches; _eventBus = eventBus; }
        public override void Act()
        {
            _audioHandler.PlaySound(SoundType.SFX, _actSound);
            _eventBus.BranchThrownIntoForest?.Invoke();
            _branches.RemoveVisualObj();
            if (_branches.HasObjs() == 0) _branches.Player.PlayerItemHandler.ClearHeldItem();
        }
    }

    public class DropIntoFire : InventoryAction
    {
        private AudioClip _actSound;
        private AudioHandler _audioHandler;
        private Branches _branches;
        private EventBus _eventBus;
        private EndGame _endGame;

        public override void Init(Player.Player player, AudioHandler handler)
        {
            _audioHandler = handler;
            LocalizationKey = "DropFire";
        }
        public void SoundInit(AudioClip actSound) { _actSound = actSound; }
        public void CustomInit(Branches branches, EventBus eventBus, EndGame endGame) { _branches = branches; _eventBus = eventBus; _endGame = endGame; }
        public override void Act()
        {
            _endGame._statistics.FirstOrDefault(x => x.Name == "StatBranches").OptionalValue += 1;
            _audioHandler.PlaySound(SoundType.SFX, _actSound, 0.5f);
            _eventBus.BranchThrownIntoFire?.Invoke();
            _branches.RemoveVisualObj();
            if (_branches.HasObjs() == 0) _branches.Player.PlayerItemHandler.ClearHeldItem();
        }
    }
}