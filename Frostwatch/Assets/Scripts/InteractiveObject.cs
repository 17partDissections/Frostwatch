using UnityEngine;
using Zenject;

namespace Q17pD.Frostwatch
{
    public abstract class InteractiveObject : MonoBehaviour
    {
        [SerializeField] protected Outline _outline;
        [SerializeField] private AudioClip _enterSound;
        protected AudioHandler _audioHandler;

        [Inject] private void Construct(AudioHandler aH) { _audioHandler = aH;  }

        protected virtual void Start()
        {
            _outline.OutlineMode = Outline.Mode.OutlineVisible;
            _outline.OutlineColor = Color.white;
            _outline.OutlineWidth = 5;
            _outline.enabled = false;
        }

        public virtual void OnMouseEnter() { _outline.enabled = true; _audioHandler.PlaySFX(_enterSound, 0); }

        public virtual void OnMouseExit() { _outline.enabled = false; }

        public abstract void OnMouseDown();
    }
}
