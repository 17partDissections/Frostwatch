using System.Collections.Generic;
using UnityEngine;

namespace Q17pD.Frostwatch.Inventory
{
    public class Rifle : InventoryItem
    {
        [SerializeField] private ParticleSystem _muzzleFlash;
        [SerializeField] private AudioClip _shoot, _reload, _aimAtItself;
        //[SerializeField] private MonstersHandler _monstersHandler;
        [SerializeField] private Player.Player _player;
        protected override void Awake()
        {
            base.Awake();
            Shoot shoot = new Shoot(); shoot.SoundInit(_shoot, _reload);
            AimAtItself aim = new AimAtItself(); aim.SoundInit(_aimAtItself);
            Back back = new Back(); back.SoundInit(_aimAtItself);
            Suicide suicide = new Suicide(); suicide.SoundInit(_shoot);
            Actions.AddRange(new List<InventoryAction> { shoot, aim, back, suicide });
        }
    }
    public class Shoot : InventoryAction
    {
        private AudioClip _shootSound, _reloadSound;
        private Player.Player _player;
        private AudioHandler _audioHandler;
        public override void Init(Player.Player player, AudioHandler handler)
        {
            _player = player;
            _audioHandler = handler;
            LocalizationKey = "Shoot";
        }
        public void SoundInit(AudioClip shootSound, AudioClip reloadSound) { _shootSound = shootSound; _reloadSound = reloadSound; }
        public override void Act() { _audioHandler.PlaySFX(_shootSound, 0);  }
    }
    public class AimAtItself : InventoryAction
    {
        private AudioClip _actSound;
        private Player.Player _player;
        private AudioHandler _audioHandler;
        public override void Init(Player.Player player, AudioHandler handler)
        {
            _player = player;
            _audioHandler = handler;
            LocalizationKey = "Suicide";
        }
        public void SoundInit(AudioClip actSound) { _actSound = actSound; }
        public override void Act() { _audioHandler.PlaySFX(_actSound, 0);  }
    }
    public class Back : InventoryAction
    {
        private AudioClip _actSound;
        private Player.Player _player;
        private AudioHandler _audioHandler;
        public override void Init(Player.Player player, AudioHandler handler)
        {
            IsCustomConditionSatisfied = false;
            _player = player;
            _audioHandler = handler;
            LocalizationKey = "BackButton";
        }
        public void SoundInit(AudioClip actSound) { _actSound = actSound; }
        public override void Act() { _audioHandler.PlaySFX(_actSound, 0); _player.PlayerItemHandler.DropItem(); }
    }
    public class Suicide : InventoryAction
    {
        private AudioClip _actSound;
        private Player.Player _player;
        private AudioHandler _audioHandler;
        public override void Init(Player.Player player, AudioHandler handler)
        {
            IsCustomConditionSatisfied = false;
            _player = player;
            _audioHandler = handler;
            LocalizationKey = "Suicide";
        }
        public void SoundInit(AudioClip actSound) { _actSound = actSound; }
        public override void Act() { _audioHandler.PlaySFX(_actSound, 0); _player.PlayerItemHandler.DropItem(); }
    }
}
