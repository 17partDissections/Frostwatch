using System.Collections;
using UnityEngine;
using Zenject;

namespace Q17pD.Frostwatch
{
    public class StartGame : MonoBehaviour
    {
        [SerializeField] private Mode _mode;
        [SerializeField] private UIHighlight _darkeningPanel;
        [SerializeField] private AudioClip _menuMusic, _monster;
        private AudioSource _menuMusicSrc;
        private AudioHandler _audioHandler;
        private CursorHandler _cursorHandler;

        [Inject] private void Construct(AudioHandler ah, CursorHandler ch) { _audioHandler = ah; _cursorHandler = ch; }
        private void Start() {if(_mode == Mode.Campaign) _audioHandler.PlaySound(SoundType.Music, _menuMusic, true, true, 7); }
        public void LoadScene() { StartCoroutine(LoadSceneCoroutine()); }
        private IEnumerator LoadSceneCoroutine()
        {
            _cursorHandler.LockCursorToggle(true);
            if (_darkeningPanel != null) { _darkeningPanel.gameObject.SetActive(true); _darkeningPanel.UnHighlightImage(0); _darkeningPanel.HighlightImage(1, 3); }
            _audioHandler.StopSound(_menuMusicSrc, true, 3);
            yield return new WaitForSeconds(1.5f);
            if (_monster != null && _mode == Mode.Campaign) _audioHandler.PlaySound(SoundType.SFX, _monster, false, true, 0.1f);
            yield return new WaitForSeconds(1.5f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(_mode == Mode.Campaign ? 1 : 2);
        }
    }
    public enum Mode { Campaign, EndlessMode }
}
