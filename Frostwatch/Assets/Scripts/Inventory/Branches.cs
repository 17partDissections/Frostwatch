using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Q17pD.Frostwatch.Inventory
{
    public class Branches : InventoryItem
    {
        [SerializeField] private List<GameObject> _visualObjs;
        [SerializeField] private AudioClip _dropIntoForestSound, _dropIntoFireSound;
        [SerializeField] private Player.Player _player;
        public int HasObjs()
        {
            int a = 0;
            foreach (GameObject obj in _visualObjs) if(obj.activeSelf) a++;
            return a;
        }
        public void AddVisualObj() { _visualObjs.FirstOrDefault(x=>!x.activeSelf).SetActive(true); }
        public void RemoveVisualObj() 
        { 
            _visualObjs.LastOrDefault(x=>x.activeSelf).SetActive(false);
            if (!_visualObjs.Any(x => x.activeSelf)) _player.EventBus.OutOfBranches?.Invoke(-1);
        }
        public bool CanAddMoreVisualObj() { return _visualObjs.Any(x => !x.activeSelf); }

        protected override void Awake()
        {
            base.Awake();
            DropIntoForest dropIntoForest = new DropIntoForest();
            dropIntoForest.SoundInit(_dropIntoForestSound);
            dropIntoForest.CustomInit(this, _player.EventBus);

            DropIntoFire dropIntoFire = new DropIntoFire();
            dropIntoFire.SoundInit(_dropIntoFireSound);
            dropIntoFire.CustomInit(this, _player.EventBus);
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
            _audioHandler.PlaySFX(_actSound, 0);
            _branches.RemoveVisualObj();
            _eventBus.BranchThrownIntoForest?.Invoke();
        }
    }
    public class DropIntoFire : InventoryAction
    {
        private AudioClip _actSound;
        private AudioHandler _audioHandler;
        private Branches _branches;
        private EventBus _eventBus;

        public override void Init(Player.Player player, AudioHandler handler)
        {
            _audioHandler = handler;
            LocalizationKey = "DropFire";
        }
        public void SoundInit(AudioClip actSound) { _actSound = actSound; }
        public void CustomInit(Branches branches, EventBus eventBus) { _branches = branches; _eventBus = eventBus; }
        public override void Act()
        {
            _audioHandler.PlaySFX(_actSound, 0);
            _branches.RemoveVisualObj();
            _eventBus.BranchThrownIntoFire?.Invoke();
        }
    }
}
