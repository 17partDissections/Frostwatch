using UnityEngine;
using DG.Tweening;
using Zenject;

namespace Q17pD.Frostwatch
{
    public class PickupableObject : InteractiveObject
    {
        [SerializeField] private int _index;
        [SerializeField] private GameObject _objToMove;
        [SerializeField] private Transform _moveFinalTransform;
        [SerializeField] private AudioClip _pickupSound;
        private Vector3 _originalPos;
        private bool _isMoving;
        private Player.PlayerItemHandler _playerIH;

        [Inject] private void Construct(Player.Player player)
        {
            _playerIH = player.PlayerItemHandler;
            _originalPos = transform.position;
            if (_objToMove == null) _objToMove = gameObject;
        }
        public override void OnMouseEnter()
        {
            base.OnMouseEnter();
            if (!_playerIH.IsPlayerHoldingItem())
            {
                _isMoving = true;
                _objToMove.transform.DOMove(_moveFinalTransform.position, 0.1f).OnComplete(() => _isMoving = false);
            }
        }
        public override void OnMouseExit()
        {
            base.OnMouseExit();
            if (!_playerIH.IsPlayerHoldingItem())
            {
                _isMoving = true;
                _objToMove.transform.DOMove(_originalPos, 0.1f).OnComplete(() => _isMoving = false);
            }
        }
        public override void OnMouseDown()
        {
            base.OnMouseDown();
            if (!_playerIH.IsPlayerHoldingItem())
            {
                _isMoving = true;
                _objToMove.transform.DOMove
                (
                    _player.PickupableObjectsFinalTransform.position, 0.2f
                ).OnComplete
                    (
                        () =>
                        {
                            _isMoving = false;
                            _audioHandler.PlaySFX(_pickupSound, 0);
                            _playerIH.AddItem(_index);
                            transform.position = _originalPos;
                            gameObject.SetActive(false);
                        }
                    );
            }
        }
    }
}
