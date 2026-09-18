using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Q17pD.Frostwatch
{
    public class SoundSlider : MonoBehaviour
    {
        [SerializeField] private SoundType _sliderType;
        [SerializeField] private TextMeshProUGUI _handleValue;
        [SerializeField] private AudioHandler _audioHandler;
        [SerializeField] private GameObject _attention;
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            string key = _sliderType == SoundType.Music ? "MusicSliderValue" : "SFXSliderValue";
            _slider.value = PlayerPrefs.GetFloat(key);
        }
        public void ChangeValue()
        {
            _audioHandler.SetVolumeFromSlider(_sliderType, _slider.value);
            _handleValue.text = (Math.Round((_slider.value * 100), 0)).ToString();
            if(_attention) _attention.SetActive(_sliderType == SoundType.SFX && _slider.value < 0.5 ? true : false);
        }
        public void Save()
        {
            string key = _sliderType == SoundType.Music ? "MusicSliderValue" : "SFXSliderValue";
            PlayerPrefs.SetFloat(key, _slider.value);
            _audioHandler.Save();
        }
        public void Revert()
        {
            _slider.value = PlayerPrefs.GetFloat(_sliderType == SoundType.Music ? "MusicSliderValue" : "SFXSliderValue");
            if (_attention) _attention.SetActive(_sliderType == SoundType.SFX && _slider.value < 0.5 ? true : false);
            _audioHandler.Revert();
        }
    }
}
