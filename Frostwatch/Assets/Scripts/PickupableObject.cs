using UnityEngine;
using DG.Tweening;
using Zenject;
using UnityEditor.Experimental.GraphView;

namespace Q17pD.Frostwatch
{
    public class PickupableObject : InteractiveObject
    {
        [SerializeField] private int _index;
        public GameObject ObjToMove;
        [SerializeField] private Transform _moveFinalTransform;
        [SerializeField] private AudioClip _pickupSound;
        private Vector3 _originalPos;
        private bool _isMoving;

        private void Awake() { if (ObjToMove == null) ObjToMove = gameObject; }
        private void OnEnable()
        {
            _originalPos = ObjToMove.transform.position;
            ObjToMove.transform.position = _moveFinalTransform.position;
            ObjToMove.transform.DOMove(_originalPos, 0.25f);
        }
        public override void OnMouseEnter()
        {
            base.OnMouseEnter();
            if (_ignorePlayerHoldingItem || !_player.IsHoldingItem)
            {
                _isMoving = true;
                ObjToMove.transform.DOMove(_moveFinalTransform.position, 0.1f).OnComplete(() => _isMoving = false);
            }

        }
        public override void OnMouseExit()
        {
            base.OnMouseExit();
            if (_ignorePlayerHoldingItem || !_player.IsHoldingItem)
            {
                _isMoving = true;
                ObjToMove.transform.DOMove(_originalPos, 0.1f).OnComplete(() => _isMoving = false);
            }
        }
        public override void OnMouseDown()
        {
            _player.PlayerCanvasHandler.ClearInfo();
            _outline.enabled = false;
            _cursorHandler.SetCursor("Default");
            if (_ignorePlayerHoldingItem || !_player.IsHoldingItem)
            {
                _isMoving = true;
                ObjToMove.transform.DOMove
                (
                    _player.PickupableObjectsFinalTransform.position, 0.2f
                ).OnComplete
                    (
                        () =>
                        {
                            _isMoving = false;
                            _audioHandler.PlaySFX(_pickupSound, 0);
                            ObjToMove.transform.position = _originalPos;
                            _player.PlayerItemHandler.AddItem(_index, gameObject);
                            gameObject.SetActive(false);
                        }
                    );
            }
        }
    }
}
