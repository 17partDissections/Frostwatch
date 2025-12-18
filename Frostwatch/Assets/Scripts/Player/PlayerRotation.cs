using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Q17pD.Frostwatch.Player
{
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField] private float _cooldown = 0.25f;
        private Player _player;
        private bool _isBlending;
        private WaitForSeconds _cooldownWFS;
        private void Start() { _player = GetComponentInParent<Player>(); _cooldownWFS = new WaitForSeconds(_cooldown); }
        public void Rotate(RotationType rotationType)
        {
            if(!_isBlending)
            {
                if(_player.CurrentCameraIndex != 0) _player.Cameras[_player.CurrentCameraIndex].Priority = _player.Cameras[0].Priority;
                int newIndex = _player.CurrentCameraIndex;
                if (rotationType == RotationType.Right) newIndex = _player.CurrentCameraIndex == 3 ? 0 : newIndex + 1;
                else newIndex = _player.CurrentCameraIndex == 0 ? 3 : newIndex - 1;
                if(newIndex != 0) _player.Cameras[0].Priority = _player.Cameras[newIndex].Priority;
                _player.Cameras[newIndex].Priority = _player.Cameras.Count;
                _player.CurrentCameraIndex = newIndex;
                StartCoroutine(BlendingCoroutine());
            } 
        }
        private IEnumerator BlendingCoroutine()
        {
            _isBlending = true;
            yield return _cooldownWFS;
                while (_player.Brain.IsBlending) yield return null;
            yield return _cooldownWFS;
            _isBlending = false;
        }
    }
    public enum RotationType { Left, Right }
}
