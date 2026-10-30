using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Q17pD.Frostwatch
{
    public class Credits : MonoBehaviour
    {
        [SerializeField] private CursorHandler _cursorHandler;
        [SerializeField] private AudioHandler _audioHandler;
        [SerializeField] private AudioClip _clip;
        [SerializeField] private UIHighlight _darkeningPanel, _skipButton;
        [SerializeField] private Transform _finalPos, _thxFinalPos; [SerializeField] private float _time;
        [SerializeField] private Transform _thx;
        private AudioSource _src;

        public void End() { StopAllCoroutines(); StartCoroutine(EndCoroutine()); }
        private IEnumerator Start()
        {
            _cursorHandler.LockCursorToggle(true);
            _src = _audioHandler.PlaySound(SoundType.Music, _clip, 1f, false, true, 5);
            transform.DOMove(_finalPos.position, _time).SetEase(Ease.Linear);
            _thx.DOMove(_thxFinalPos.position, _time).SetEase(Ease.Linear);
            yield return new WaitForSeconds(_time / 10 * 2);
            _skipButton.GetComponentInParent<Button>().enabled = true; _skipButton.HighlightTMP(0.3f, 2);
            _cursorHandler.LockCursorToggle(false);
            yield return new WaitForSeconds(_time / 10 * 8);
            yield return new WaitForSeconds(2);
            End();
        }
        private IEnumerator EndCoroutine()
        {
            _skipButton.gameObject.SetActive(false);
            _cursorHandler.LockCursorToggle(true);
            _darkeningPanel.HighlightImage(1, 2);
            _audioHandler.StopSound(_src, true, 2);
            yield return new WaitForSeconds(2);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }
    }
}
