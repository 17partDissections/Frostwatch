using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Q17pD.Frostwatch.Menu
{
public class SoundToggle : MonoBehaviour
{
    private AudioHandler _audioHandler;
    private Toggle _toggle;

    [SerializeField]
    private ToggleType _toggleType;

    [Inject]
    private void Construct(AudioHandler ah)
    {
        _audioHandler = ah;
        _toggle = GetComponent<Toggle>();
    }

    private void Awake()
    {
        string key = _toggleType == ToggleType.Music ? "MusicToggleValue" : "SFXToggleValue";
        _toggle.isOn = PlayerPrefs.GetInt(key) == 1;
    }

    public void ChangeValue()
    {
        int value = _toggle.isOn ? 0 : -80;

        if (_toggleType == ToggleType.Music)
            _audioHandler.TempChangeMusicValue(value);
        else
            _audioHandler.TempChangeSFXValue(value);
    }

    public void Save()
    {
        string key = _toggleType == ToggleType.Music ? "MusicToggleValue" : "SFXToggleValue";
        PlayerPrefs.SetInt(key, _toggle.isOn ? 1 : 0);
    }
}
}

public enum ToggleType { Music, SFX }