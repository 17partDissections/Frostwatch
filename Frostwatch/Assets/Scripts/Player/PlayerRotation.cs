using System.Collections;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace Q17pD.Frostwatch.Player
{
    public class PlayerRotation : MonoBehaviour
    {
        public float Cooldown = 0.25f;
        [SerializeField] private AudioClip _rotateSound;
        private Player _player;
        private AudioHandler _audioHandler;
        private WaitForSeconds _cooldownWFS;
        [Inject] private void Construct(AudioHandler audioHandler) 
        {
            _audioHandler = audioHandler;
            _player = GetComponent<Player>();
            _cooldownWFS = new WaitForSeconds(Cooldown);
        }
        public void Rotate(RotationType rotationType)
        {
            if(!_player.IsMoving)
            {
                if(_player.CurrentCameraIndex != 0) _player.Cameras[_player.CurrentCameraIndex].Priority = _player.Cameras[0].Priority;
                int newIndex = _player.CurrentCameraIndex;
                if (rotationType == RotationType.Right) newIndex = _player.CurrentCameraIndex == 3 ? 0 : newIndex + 1;
                else newIndex = _player.CurrentCameraIndex == 0 ? 3 : newIndex - 1;
                if(newIndex != 0) _player.Cameras[0].Priority = _player.Cameras[newIndex].Priority;
                _player.Cameras[newIndex].Priority = _player.Cameras.Count;
                _player.PlayerCanvasHandler.HideActions();
                StartCoroutine(RotatingCoroutine(newIndex));
            } 
        }
        private IEnumerator RotatingCoroutine(int newIndex)
        {
            _player.IsMoving = true;
            yield return _cooldownWFS;
            _audioHandler.PlaySFX(_rotateSound, 0);
            while (_player.Brain.IsBlending) yield return null;
            _player.PlayerCanvasHandler.UpdateActions(newIndex);
            _player.CurrentCameraIndex = newIndex;
            yield return _cooldownWFS;
            _player.IsMoving = false;
        }
    }
    public enum RotationType { Left, Right }
}
