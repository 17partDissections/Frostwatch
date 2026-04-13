using UnityEngine;
using Zenject;

namespace Q17pD.Frostwatch.Interactive
{
    public abstract class InteractiveObject : MonoBehaviour
    {
        [SerializeField] protected Outline _outline;
        [SerializeField] private string _localeNameKey, _localeDescriptionKey;
        [SerializeField] private AudioClip _enterSound;
        protected CursorHandler _cursorHandler;
        protected AudioHandler _audioHandler;
        protected Player.Player _player;
        [SerializeField] protected bool _ignorePlayerHoldingItem;

        [Inject] private void Construct(CursorHandler cursorHandler, AudioHandler aH, Player.Player player)
        {
            _cursorHandler = cursorHandler;
            _audioHandler = aH;
            _player = player;
        }
        protected virtual void Start()
        {
            _outline.OutlineMode = Outline.Mode.OutlineVisible;
            _outline.OutlineWidth = 5;
            _outline.enabled = false;
        }
        public virtual void OnMouseEnter()
        {
            if (!_player.IsMoving && (_ignorePlayerHoldingItem || !_player.IsHoldingItem))
            {
                _player.PlayerCanvasHandler.SetObjectInfo(_localeNameKey, _localeDescriptionKey);
                _outline.enabled = true;
                _cursorHandler.SetCursor("Pointer");
                _audioHandler.PlaySound(SoundType.SFX, _enterSound);
            }
        }
        public virtual void OnMouseExit()
        {
            _player.PlayerCanvasHandler.ClearInfo();
            _outline.enabled = false;
            _cursorHandler.SetCursor("Default");
        }
        public abstract void OnMouseDown();
    }
}
