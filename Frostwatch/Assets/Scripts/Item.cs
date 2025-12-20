using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Q17pD.Frostwatch
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private int _index;
        [SerializeField] private string _localeKey;
        [SerializeField] private bool _isCollectable;
        [Range(0, 5)][SerializeField] private int _amount = 1;
        [SerializeField] private Transform  _moveFinalTransform;
        [SerializeField] private AudioClip _enterSound, _pickUpSound, _dropSound;
        private Outline _outline;
        private Vector3 _originalPos;
        private Player.Player _player;
        private AudioHandler _audioHandler;

       [Inject] private void Construct(Player.Player player, AudioHandler audioHandler)
        {
            _outline = GetComponent<Outline>();
            _outline.enabled = false;
            if(_isCollectable) 
            {
                _originalPos = transform.position;
                _player = player;
            }
            _audioHandler = audioHandler;
        }

        private void OnMouseEnter()
        {
            _outline.enabled = true;
            _audioHandler.PlaySFX(_enterSound, 0);
            if(_isCollectable) transform.DOMove(_moveFinalTransform.position, 0.1f);
        }
        private void OnMouseExit()
        {
            _outline.enabled = false;
            if(_isCollectable) transform.DOMove(_originalPos, 0.1f);
        }
        private void OnMouseDown()
        {
            if(_isCollectable && !_player.PlayerItemHandler.IsPlayerHoldingItem())
            {
                _audioHandler.PlaySFX(_pickUpSound, 0);
                _player.PlayerItemHandler.AddItem(_index);
            }

        }
    }
}
