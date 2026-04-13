using System.Collections;
using UnityEngine;
using Zenject;

namespace Q17pD.Frostwatch
{
    public class ChangeScene : MonoBehaviour
    {
        [SerializeField] private int _sceneID;
        [SerializeField] private float _delay;
        [SerializeField] private UIHighlight _darkeningPanel;
        [SerializeField] private AudioClip _clip;
        private AudioHandler _audioHandler;
        private CursorHandler _cursorHandler;

        [Inject] private void Construct(AudioHandler ah, CursorHandler cursorHandler) { _audioHandler = ah; _cursorHandler = cursorHandler;  }
        public void LoadScene() { StartCoroutine(LoadSceneCoroutine()); }
        private IEnumerator LoadSceneCoroutine()
        {
            _cursorHandler.LockCursorToggle(true);
            if (_darkeningPanel != null) {  _darkeningPanel.gameObject.SetActive(true); _darkeningPanel.UnHighlightImage(0); _darkeningPanel.HighlightImage(1, _delay); }
            yield return new WaitForSeconds(_delay /2);
            if (_clip != null) _audioHandler.PlaySound(SoundType.SFX, _clip);
            yield return new WaitForSeconds(_delay /2);
            UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneID);
        }
    }
}
