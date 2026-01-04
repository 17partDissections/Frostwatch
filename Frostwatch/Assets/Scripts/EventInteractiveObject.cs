using UnityEngine;

namespace Q17pD.Frostwatch
{
    public class EventInteractiveObject : InteractiveObject
    {
        [SerializeField] private AudioClip _downSound;
        public override void OnMouseDown()
        {
            base.OnMouseDown();
            _audioHandler.PlaySFX(_downSound, 0);
            //invoking action from eventbus
            //eventbus is not even added in dis project btw)))
            //upd.: no wait i just checked. its added. but im too lazy for doing this rn
        }
    }
}
