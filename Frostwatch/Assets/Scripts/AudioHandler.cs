using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Q17pD
{
    public class AudioHandler : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _audioMixerGroup;
        [SerializeField] private AudioSource _audioSourcePrefab;
        private List<AudioSource> _activeAudioSources = new List<AudioSource>();
        private List<AudioSource> _availableAudioSources = new List<AudioSource>();
        private float _tempMusicDB, _tempSFXDB;

        private void Start() { Revert(); }
        private void Update() { for (int i = _activeAudioSources.Count - 1; i >= 0; i--) if (!_activeAudioSources[i].isPlaying) ReturnSourceToPool(_activeAudioSources[i]); }
        public AudioSource PlaySound(SoundType soundType, AudioClip clip, bool loop = false)
        {
            AudioSource source = GetAvailableAudioSource();
            source.clip = clip; source.loop = loop;
            if (soundType == SoundType.Music) source.outputAudioMixerGroup = _audioMixerGroup.audioMixer.FindMatchingGroups("Music")[0];
            else source.outputAudioMixerGroup = _audioMixerGroup.audioMixer.FindMatchingGroups("SFX")[0];
            source.Play();
            _activeAudioSources.Add(source);
            return source;
        }
        public void StopSound(AudioSource source) { source.Stop(); ReturnSourceToPool(source); }
        public void StopAllSounds()
        {
            for (int i = _activeAudioSources.Count - 1; i >= 0; i--)
            {
                _activeAudioSources[i].Stop();
                ReturnSourceToPool(_activeAudioSources[i]);
            }
        }
        private AudioSource GetAvailableAudioSource()
        {
            if (_availableAudioSources.Count > 0)
            {
                AudioSource source = _availableAudioSources[_availableAudioSources.Count - 1];
                _availableAudioSources.RemoveAt(_availableAudioSources.Count - 1);
                return source;
            }
            AudioSource newSource = Instantiate(_audioSourcePrefab, transform);
            return newSource;
        }
        private void ReturnSourceToPool(AudioSource source)
        {
            source.Stop(); source.clip = null;
            _activeAudioSources.Remove(source); _availableAudioSources.Add(source);
        }
        public void SetMusicVolume(float percent) => _audioMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, percent));
        public void SetSFXVolume(float percent) => _audioMixerGroup.audioMixer.SetFloat("SFXVolume", Mathf.Lerp(-80, 0, percent));
        public void Save()
        {
            PlayerPrefs.SetFloat("MusicVolume", _tempMusicDB);
            PlayerPrefs.SetFloat("SFXVolume", _tempSFXDB);
        }
        public void Revert()
        {
            _audioMixerGroup.audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume", 0f));
            _audioMixerGroup.audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume", 0f));
        }
        public void SetVolumeFromSlider(SoundType soundType, float value)
        {
            _tempMusicDB = Mathf.Lerp(-80, 0, value);
            if (soundType == SoundType.Music) _audioMixerGroup.audioMixer.SetFloat("MusicVolume", _tempMusicDB);
            else _audioMixerGroup.audioMixer.SetFloat("MusicVolume", _tempMusicDB);
        }
        public void SetVolumeFromToggle(SoundType soundType, bool value)
        {
            _tempMusicDB = value == true ? 0 : -80;
            if (soundType == SoundType.Music) _audioMixerGroup.audioMixer.SetFloat("MusicVolume", _tempMusicDB);
            else _audioMixerGroup.audioMixer.SetFloat("MusicVolume", _tempMusicDB);
        }
    }
    public enum SoundType { Music, SFX }
}