using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Q17pD
{
    public class AudioHandler : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _audioMixerGroup;
        [SerializeField] private AudioSource _music;
        [SerializeField] private AudioSource _SFX;
        [SerializeField] private AudioSource _SFXAudioSourcePrefab;
        private List<AudioSource> _SFXSources = new List<AudioSource>();
        private int _tempMusicValue;
        private int _tempSFXValue;
        private void Start()
        {
            _audioMixerGroup.audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
            _audioMixerGroup.audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
        }
        private void OnDestroy() { Save(); }

        /// <summary>
        /// keep volume <= 0, if you want to play sound with current audiosource volume.
        /// </summary>
        public void PlaySFX(AudioClip clip, float volume)
        {
            AudioSource SFXSource = GetAvailableSFXSource();
            SFXSource.clip = clip;
            if (volume > 0)
                SFXSource.volume = volume;
            SFXSource.PlayOneShot(clip);
        }
        /// <summary>
        /// keep volume <= 0, if you want to play sound with current audiosource volume.
        /// </summary>
        public void PlayMusic(AudioClip clip, float volume)
        {
            _music.clip = clip;
            if(volume > 0)
                _music.volume = volume;
            _music.Play();
        }
        public void StopMusic()
        {
            _music.Stop();
        }
        public void StopSFX()
        {
            foreach (var source in _SFXSources)
            {
                if(source.isPlaying)
                    source.Stop();
            }
        }
        private AudioSource GetAvailableSFXSource()
        {
            foreach (var source in _SFXSources) if (!source.isPlaying) return source;
            AudioSource newSource = Instantiate(_SFXAudioSourcePrefab, transform);

            newSource.transform.SetParent(transform);
            newSource.outputAudioMixerGroup = _audioMixerGroup;
            _SFXSources.Add(newSource);
            return newSource;
        }
        public void OnMasterVolumeValueChanged(float percent)
        {
            _audioMixerGroup.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, percent));
        }
        public void OnMusicVolumeValueChanged(float percent)
        {
            _audioMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, percent));
        }
        public void OnSFXVolumeValueChanged(float percent)
        {
            _audioMixerGroup.audioMixer.SetFloat("SFXVolume", Mathf.Lerp(-80, 0, percent));

        }
        public void OnMusicVolumeValueChangedBySlider(UnityEngine.UI.Slider slider)
        {
            var percent = slider.value;
            _audioMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, percent));
            PlayerPrefs.SetFloat("MusicSlider", percent);
        }
        public void OnSFXVolumeValueChangedBySlider(UnityEngine.UI.Slider slider)
        {
            var percent = slider.value;
            _audioMixerGroup.audioMixer.SetFloat("SFXVolume", Mathf.Lerp(-80, 0, percent));
            PlayerPrefs.SetFloat("SFXSlider", percent);

        }
        public void TempChangeMusicValue(int value) { _tempMusicValue = value; }
        public void TempChangeSFXValue(int value) { _tempSFXValue = value; }
        public void CompletelyChangeValues()
        {
            _audioMixerGroup.audioMixer.SetFloat("MusicVolume", _tempMusicValue);
            _audioMixerGroup.audioMixer.SetFloat("SFXVolume", _tempSFXValue);
            Save();
        }
        private void Save()
        {
            _audioMixerGroup.audioMixer.GetFloat("MusicVolume", out float mValue);
            PlayerPrefs.SetFloat("MusicVolume", mValue);
            _audioMixerGroup.audioMixer.GetFloat("SFXVolume", out float sfxValue);
            PlayerPrefs.SetFloat("SFXVolume", sfxValue);
        }
    }
}

