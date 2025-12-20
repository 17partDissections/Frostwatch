using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Q17pD.Frostwatch
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private int _itemIndex;
        [SerializeField] private string _itemName;
        [SerializeField] private string _itemDescription;
        [SerializeField] private bool _collectable;
        [Range(0, 5)][SerializeField] private int _amount = 1;
        [SerializeField] private Transform  _moveFinalTransform;
        [SerializeField] private AudioClip _itemEnterSound, _itemPickUpSound, _itemDropSound;
        private Outline _outline;
        private Vector3 _originalPos;
        private Player.Player _player;
        private AudioHandler _audioHandler;

       [Inject] private void Construct(Player.Player player, AudioHandler audioHandler)
        {
            _outline = GetComponent<Outline>();
            _outline.enabled = false;
            if(_collectable) 
            {
                _originalPos = transform.position;
                _player = player;
            }
            _audioHandler = audioHandler;
        }

        private void OnMouseEnter()
        {
            _outline.enabled = true;
            _audioHandler.PlaySFX(_itemEnterSound, 0);
            if(_collectable) transform.DOMove(_moveFinalTransform.position, 0.1f);
        }
        private void OnMouseExit()
        {
            _outline.enabled = false;
            if(_collectable) transform.DOMove(_originalPos, 0.1f);
        }
        private void OnMouseDown()
        {
            if(_collectable)
            {
                _audioHandler.PlaySFX(_itemPickUpSound, 0);
            }
        }
    }
}
