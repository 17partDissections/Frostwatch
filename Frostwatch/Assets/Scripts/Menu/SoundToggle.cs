using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Q17pD.Frostwatch.Menu
{
    public class SoundToggle : MonoBehaviour
    {
        private AudioHandler _audioHandler;
        private Toggle _toggle;
        [SerializeField] private SoundType _toggleType;

        [Inject] private void Construct(AudioHandler ah)
        {
            _audioHandler = ah;
            _toggle = GetComponent<Toggle>();
        }
        private void Awake()
        {
            string key = _toggleType == SoundType.Music ? "MusicToggleValue" : "SFXToggleValue";
            _toggle.isOn = PlayerPrefs.GetInt(key) == 1;
        }
        public void ChangeValue() { _audioHandler.SetVolumeFromToggle(_toggleType, _toggle.isOn); }
        public void Save()
        {
            string key = _toggleType == SoundType.Music ? "MusicToggleValue" : "SFXToggleValue";
            PlayerPrefs.SetInt(key, _toggle.isOn ? 1 : 0);
        }
    }
}
