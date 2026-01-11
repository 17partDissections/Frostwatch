using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Q17pD.Frostwatch.Player
{
    public class PlayerRotation : MonoBehaviour
    {
        public float Cooldown = 0.25f;
        private Player _player;
        private WaitForSeconds _cooldownWFS;
        private void Start() { _player = GetComponentInParent<Player>(); _cooldownWFS = new WaitForSeconds(Cooldown); }
        public void Rotate(RotationType rotationType)
        {
            if(!_player.IsBlending)
            {
                if(_player.CurrentCameraIndex != 0) _player.Cameras[_player.CurrentCameraIndex].Priority = _player.Cameras[0].Priority;
                int newIndex = _player.CurrentCameraIndex;
                if (rotationType == RotationType.Right) newIndex = _player.CurrentCameraIndex == 3 ? 0 : newIndex + 1;
                else newIndex = _player.CurrentCameraIndex == 0 ? 3 : newIndex - 1;
                if(newIndex != 0) _player.Cameras[0].Priority = _player.Cameras[newIndex].Priority;
                _player.Cameras[newIndex].Priority = _player.Cameras.Count;
                StartCoroutine(BlendingCoroutine(newIndex));
            } 
        }
        private IEnumerator BlendingCoroutine(int newIndex)
        {
            _player.IsBlending = true;
            yield return _cooldownWFS;
                while (_player.Brain.IsBlending) yield return null;
            _player.PlayerCanvasHandler.UpdateActions(newIndex);
            _player.CurrentCameraIndex = newIndex;
            yield return _cooldownWFS;
            _player.IsBlending = false;
        }
    }
    public enum RotationType { Left, Right }
}
