using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Q17pD.Frostwatch.Inventory
{
    public class Rifle : InventoryItem
    {
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
            shoot.CustomInit(_player.EventBus);
            aim.CustomInit(_drop, back, suicide);
            Actions.AddRange(new List<InventoryAction> { shoot, aim, back, suicide });
        }
    }
    public class Shoot : InventoryAction
    {
        private AudioClip _shootSound, _reloadSound;
        private Player.Player _player;
        private AudioHandler _audioHandler;
        private EventBus _eventBus;

        private float _shootDelay = 1f;
        private int _shootAnim = 2;
        private int _reloadAnim = 4;

        private int _bulletsLeft = 5;
        private WaitForSeconds _sleep;
        private bool _isShooting;
        private Coroutine _currentCoroutine;

        public override void Init(Player.Player player, AudioHandler handler)
        {
            _player = player;
            _audioHandler = handler;
            LocalizationKey = "Shoot";

            _sleep = new WaitForSeconds(_shootDelay);
        }
        public void SoundInit(AudioClip shootSound, AudioClip reloadSound)
        {
            _shootSound = shootSound;
            _reloadSound = reloadSound;
        }
        public void CustomInit(EventBus bus) { _eventBus = bus; }
        public override void Act() { if (!_isShooting && _bulletsLeft > 0) _currentCoroutine = _player.StartCoroutine(ShootCoroutine()); }
        private IEnumerator ShootCoroutine()
        {
            _isShooting = true;

            _audioHandler?.PlaySound(SoundType.SFX, _shootSound);
            _player?.PlayerAnimation.ChangeAnimation(_shootAnim);

            yield return _shootDelay;

            _eventBus?.Shot.Invoke(_player.CurrentCameraIndex);
            DecreaseBullets();

            _isShooting = false;
            _currentCoroutine = null;
        }
        private void DecreaseBullets()
        {
            _bulletsLeft--;

            if (_bulletsLeft <= 0) Reload();
            else _player?.PlayerAnimation.ChangeAnimation(1);
        } 

        private void Reload()
        {
            _bulletsLeft = 5;
            _player?.PlayerAnimation.ChangeAnimation(_reloadAnim);
        }
    }
    public class AimAtItself : InventoryAction
    {
        private AudioClip _actSound;
        private Player.Player _player;
        private AudioHandler _audioHandler;
        private Drop _drop;  Back _back; private Suicide _suicide;
        public override void Init(Player.Player player, AudioHandler handler)
        {
            _player = player;
            _audioHandler = handler;
            LocalizationKey = "Suicide";
        }
        public void SoundInit(AudioClip actSound) { _actSound = actSound; }
        public void CustomInit(Drop drop, Back back, Suicide suicide) { _drop = drop; _back = back; _suicide = suicide; }
        public override void Act() 
        {
            _audioHandler.PlaySound(SoundType.SFX, _actSound);
            _player.PlayerAnimation.ChangeAnimation(2);
            _drop.IsCustomConditionSatisfied = false;
            _back.IsCustomConditionSatisfied = true; _suicide.IsCustomConditionSatisfied = true;
            IsCustomConditionSatisfied = false;
        }
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
        public override void Act() { _audioHandler.PlaySound(SoundType.SFX, _actSound); _player.PlayerItemHandler.DropItem(); }
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
        public override void Act() { _audioHandler.PlaySound(SoundType.SFX, _actSound); _player.PlayerItemHandler.DropItem(); }
    }
}
