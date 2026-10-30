using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Q17pD.Frostwatch.Inventory
{
    public class Rifle : InventoryItem
    {
        [SerializeField] private AudioClip _shoot, _reload, _aimAtItself;
        //[SerializeField] private MonstersHandler _monstersHandler;
        [SerializeField] private ParticleSystem _muzzleFlash;
        [SerializeField] private Player.Player _player;
        [SerializeField] private CursorHandler _cursorHandler;
        [SerializeField] private GameObject _ui;
        [SerializeField] private EndGame _endGame;
        protected override void Awake()
        {
            base.Awake();
            Shoot shoot = new Shoot(); shoot.SoundInit(_shoot, _reload);
            //AimAtItself aim = new AimAtItself(); aim.SoundInit(_aimAtItself);
            //Back back = new Back(); back.SoundInit(_aimAtItself);
            Suicide suicide = new Suicide(); suicide.SoundInit(_aimAtItself, _shoot);
            shoot.CustomInit(_muzzleFlash, _player.EventBus, _endGame);
            suicide.CustomInit(_muzzleFlash, _cursorHandler, _ui, _endGame);
            //aim.CustomInit(_drop, back, suicide);
            Actions.AddRange(new List<InventoryAction> { shoot, suicide });
        }
    }
    public class Shoot : InventoryAction
    {
        private AudioClip _shootSound, _reloadSound;
        private Player.Player _player;
        private AudioHandler _audioHandler;
        private ParticleSystem _muzzleFlash;
        private EventBus _eventBus;
        private int _shootDelay = 2, _shootAnim = 3, _reloadAnim = 4, _bulletsLeft = 5; //lol
        private WaitForSeconds _sleep;
        private bool _isShooting;
        private Coroutine _currentCoroutine;
        private EndGame _endGame;

        public override void Init(Player.Player player, AudioHandler handler)
        {
            _player = player;
            _audioHandler = handler;
            LocalizationKey = "Shoot";

        }
        public void SoundInit(AudioClip shootSound, AudioClip reloadSound)
        {
            _shootSound = shootSound;
            _reloadSound = reloadSound;
        }
        public void CustomInit(ParticleSystem muzzleFlash, EventBus bus, EndGame endGame) { _muzzleFlash = muzzleFlash; _eventBus = bus; _endGame = endGame; }
        public override void Act() { if (!_isShooting && _bulletsLeft > 0) _currentCoroutine = _player.StartCoroutine(ShootCoroutine()); }
        private IEnumerator ShootCoroutine()
        {
            _isShooting = true;
            _endGame._statistics.FirstOrDefault(x => x.Name == "StatShots").OptionalValue += 1;
            _audioHandler?.PlaySound(SoundType.SFX, _shootSound);
            _muzzleFlash.Play(); _muzzleFlash.GetComponentInChildren<ParticleSystem>().Play();
            _player?.PlayerAnimation.ChangeAnimation(_shootAnim);
            yield return new WaitForSeconds(_shootDelay);
            _eventBus.Shot?.Invoke(_player.CurrentCameraIndex);
            DecreaseBullets();

            _isShooting = false;
            _currentCoroutine = null;
        }
        private void DecreaseBullets()
        {
            _bulletsLeft--;

            if (_bulletsLeft <= 0) Reload();
            else _player?.PlayerAnimation.ChangeAnimation(2);
        } 

        private void Reload()
        {
            _bulletsLeft = 5;
            _player?.PlayerAnimation.ChangeAnimation(_reloadAnim);
        }
    }
    //public class AimAtItself : InventoryAction
    //{
    //    private AudioClip _actSound;
    //    private Player.Player _player;
    //    private AudioHandler _audioHandler;
    //    private Drop _drop;  Back _back; private Suicide _suicide;
    //    public override void Init(Player.Player player, AudioHandler handler)
    //    {
    //        _player = player;
    //        _audioHandler = handler;
    //        LocalizationKey = "Suicide";
    //    }
    //    public void SoundInit(AudioClip actSound) { _actSound = actSound; }
    //    public void CustomInit(Drop drop, Back back, Suicide suicide) { _drop = drop; _back = back; _suicide = suicide; }
    //    public override void Act() 
    //    {
    //        _audioHandler.PlaySound(SoundType.SFX, _actSound);
    //        _player.PlayerAnimation.ChangeAnimation(5);
    //        _drop.IsCustomConditionSatisfied = false;
    //        _back.IsCustomConditionSatisfied = true; _suicide.IsCustomConditionSatisfied = true;
    //        IsCustomConditionSatisfied = false;
    //    }
    //}
    //public class Back : InventoryAction
    //{
    //    private AudioClip _actSound;
    //    private Player.Player _player;
    //    private AudioHandler _audioHandler;
    //    public override void Init(Player.Player player, AudioHandler handler)
    //    {
    //        IsCustomConditionSatisfied = false;
    //        _player = player;
    //        _audioHandler = handler;
    //        LocalizationKey = "BackButton";
    //    }
    //    public void SoundInit(AudioClip actSound) { _actSound = actSound; }
    //    public override void Act() { _audioHandler.PlaySound(SoundType.SFX, _actSound); _player.PlayerItemHandler.DropItem(); }
    //}
    public class Suicide : InventoryAction
    {
        private AudioClip _actSound, _shootSound;
        private Player.Player _player;
        private AudioHandler _audioHandler;
        private ParticleSystem _muzzleFlash;
        private CursorHandler _cursorHandler;
        private GameObject _ui;
        private EndGame _endGame;
        public override void Init(Player.Player player, AudioHandler handler)
        {
            _player = player;
            _audioHandler = handler;
            LocalizationKey = "Suicide";
        }
        public void SoundInit(AudioClip actSound, AudioClip shoot) { _actSound = actSound; _shootSound = shoot; }
        public void CustomInit(ParticleSystem muzzleFlash, CursorHandler ch, GameObject ui, EndGame endGame) { _muzzleFlash = muzzleFlash; _cursorHandler = ch; _ui = ui; _endGame = endGame; }
        public override void Act() => _endGame.StartCoroutine(ActCoroutine());
        private IEnumerator ActCoroutine()
        {
            _audioHandler.PlaySound(SoundType.SFX, _actSound);
            _player.PlayerAnimation.ChangeAnimation(5);
            _cursorHandler.LockCursorToggle(true);
            _endGame._statistics.FirstOrDefault(x => x.Name == "StatShots").OptionalValue += 1;
            _ui.SetActive(false);
            yield return new WaitForSeconds(3f);
            _audioHandler.PlaySound(SoundType.SFX, _shootSound);
            yield return new WaitForSeconds(0.05f);
            _muzzleFlash.Play(); _muzzleFlash.GetComponentInChildren<ParticleSystem>().Play();
            yield return new WaitForSeconds(0.05f);
            _endGame.End(1);
        }
    }
}
