using UnityEngine;
using Zenject;

namespace Q17pD.Frostwatch
{
    public abstract class InteractiveObject : MonoBehaviour
    {
        [SerializeField] private Outline _outline;
        [SerializeField] private string _localeNameKey, _localeDescriptionKey;
        [SerializeField] private AudioClip _enterSound;
        protected AudioHandler _audioHandler;
        protected Player.Player _player;

        [Inject] private void Construct(AudioHandler aH, Player.Player player) { _audioHandler = aH; _player = player; }
        protected virtual void Start()
        {
            _outline.OutlineMode = Outline.Mode.OutlineVisible;
            _outline.OutlineColor = Color.white;
            _outline.OutlineWidth = 5;
            _outline.enabled = false;
        }

        public virtual void OnMouseEnter()
        {
            _outline.enabled = true;
            _audioHandler.PlaySFX(_enterSound, 0);
            _player.PlayerCanvasHandler.SetObjectInfo(_localeNameKey, _localeDescriptionKey);
        }

        public virtual void OnMouseExit() { _outline.enabled = false; _player.PlayerCanvasHandler.ClearInfo(); }

        public virtual void OnMouseDown() { _player.PlayerCanvasHandler.ClearInfo(); }
    }
}
