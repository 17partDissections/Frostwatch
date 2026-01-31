using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;

namespace Q17pD.Frostwatch.Interactive
{
    public class PickupableObject : InteractiveObject
    {
        [SerializeField] private int _index;
        public GameObject ObjToMove;
        [SerializeField] private Transform _moveFinalTransform;
        [SerializeField] private AudioClip _pickupSound;
        public bool IsMultiple;
        [SerializeField] private List<GameObject> _visualObjs;
        private Vector3 _originalPos;
        private bool _isMoving;
        private Collider _collider;

        private void Awake()
        {
            if (ObjToMove == null) ObjToMove = gameObject;
            if (IsMultiple)
            {
                _collider = GetComponent<Collider>();
                _collider.enabled = false;
                foreach (GameObject obj in _visualObjs) obj.SetActive(false);
            }
        }
        private void OnEnable()
        {
            _originalPos = ObjToMove.transform.position;
            ObjToMove.transform.position = _moveFinalTransform.position;
            ObjToMove.transform.DOMove(_originalPos, 0.25f);
        }
        public override void OnMouseEnter()
        {
            base.OnMouseEnter();
            if (IsMultiple) 
            {
                CheckMultipleCollider();
                if(_visualObjs.All(x=>x.activeSelf) && _player.IsHoldingItem) _cursorHandler.SetCursor("Forbidden");
                return;
            }
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
            if(IsMultiple && _player.IsHoldingItem && _visualObjs.All(x=>x.activeSelf)) return;
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
                            _player.PlayerItemHandler.AddItem(_index, this);
                            gameObject.SetActive(false);
                        }
                    );
            }
        }
        public int HasObjs()
        {
            int a = 0;
            foreach (GameObject obj in _visualObjs) if(obj.activeSelf) a++;
            return a;
        }
        public void AddVisualObj() { _visualObjs.FirstOrDefault(x=>!x.activeSelf).SetActive(true); CheckMultipleCollider(); }
        public void ClearVisualObjs() { foreach (GameObject obj in _visualObjs) obj.SetActive(false); }
        public void CheckMultipleCollider()
        {
            if (_visualObjs.Any(x => x.activeSelf) && !_collider.enabled) _collider.enabled = true;
            else if (!_visualObjs.Any(x => x.activeSelf) && _collider.enabled) _collider.enabled = false;
        }
    }
}
