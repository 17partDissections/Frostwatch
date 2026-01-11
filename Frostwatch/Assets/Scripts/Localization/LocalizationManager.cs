using UnityEngine;
using System.Collections.Generic;
using DFTGames.Localization;

namespace MBW.WaST
{
    public class LocalizationManager : MonoBehaviour
    {
        private List<SystemLanguage> _languages = new List<SystemLanguage> { SystemLanguage.English, SystemLanguage.Russian };
        private int _languageIndex;
        private int _lastLanguageIndex = -1;

        private void Start() { _languageIndex = PlayerPrefs.GetInt("LangIndex", 0); }
        public void ChangeIndex(int newIndex)
        {
            if (_lastLanguageIndex == -1)
                _lastLanguageIndex = PlayerPrefs.GetInt("LangIndex", 0);
            _languageIndex = newIndex;
            ChangeLanguage();
        }
        public void ChangeLanguage() { Localize.SetCurrentLanguage(_languages[_languageIndex]); }
        public void CompletelyChangeLanguage()
        {
            PlayerPrefs.SetInt("LangIndex", _languageIndex);
            _lastLanguageIndex = -1;
            ChangeLanguage();
        }
        public void Revert()
        {
            if (_lastLanguageIndex != -1)
            {
                _languageIndex = _lastLanguageIndex;
                _lastLanguageIndex = -1;
            }
            ChangeLanguage();
        }
    }
}