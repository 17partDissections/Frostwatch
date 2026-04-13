using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Q17pD.Frostwatch.Interactive
{
    public class Radio : InteractiveObject
    {
        [SerializeField] private List<AudioClip> _sounds;
        private AudioSource _soundSource;
        private bool _on;

        public override void OnMouseDown()
        {
            if(!_player.IsMoving && !_player.IsHoldingItem)
            {
                if(!_on) { _on = true; StartCoroutine(SoundCoroutine()); }
                else { _on = false; StopAllCoroutines(); _audioHandler.StopSound(_soundSource); }
            }
        }
        private IEnumerator SoundCoroutine()
        {
            AudioClip sound = _sounds[Random.Range(0, _sounds.Count)];
            WaitForSeconds sleep = new WaitForSeconds(sound.length);
            while (true)
            {
                _soundSource = _audioHandler.PlaySound(SoundType.SFX, sound);
                yield return sleep;
            }
        }
    }
}
