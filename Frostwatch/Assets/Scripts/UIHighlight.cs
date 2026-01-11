using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Q17pD.Frostwatch
{
    public class UIHighlight : MonoBehaviour
    {
        private Image _image; private TextMeshProUGUI _text;
        private void Start()
        {
            TryGetComponent(out _image); TryGetComponent(out _text);
        }

        public void HighlightImage(float value = 1f, float time = 0.2f) { _image.DOFade(value, time); }
        public void UnHighlightImage(float time = 0.2f) { _image.DOFade(0f, time); }
        public void HighlightImageFixedValues() { _image.DOFade(0.7f, 0.2f); }
        public void UnHighlightImageFixedValues() { _image.DOFade(0f, 0.2f); }
        public void HighlightTMP(float value = 1f, float time = 0.2f) { _text.DOFade(value, time); }
        public void UnHighlightTMP(float time = 0.2f) { _text.DOFade(0f, time); }
        public void HighlightTMPFixedValues() { _text.DOFade(1f, 0.2f); }
        public void UnHighlightTMPFixedValues() { _text.DOFade(0f, 0.2f); }
    }
}
