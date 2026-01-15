using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Q17pD.Frostwatch
{
    public class Radio : InteractiveObject
    {
        [SerializeField] private List<AudioClip> _sounds;
        private bool _on;

        public override void OnMouseDown()
        {
            if(!_on) { _on = true; StartCoroutine(SoundCoroutine()); }
            else { _on = false; StopAllCoroutines(); _audioHandler.StopSFX(); }
        }
        private IEnumerator SoundCoroutine()
        {
            Debug.Log("asd");
            AudioClip sound = _sounds[Random.Range(0, _sounds.Count)];
            WaitForSeconds sleep = new WaitForSeconds(sound.length);
            while (true)
            {
                _audioHandler.PlaySFX(sound, 0);
                yield return sleep;
            }
        }
    }
}
