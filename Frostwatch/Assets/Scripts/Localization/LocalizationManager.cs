using UnityEngine;
using System.Collections.Generic;
using DFTGames.Localization;

namespace MBW.WaST
{
    public class LocalizationManager : MonoBehaviour
    {
        private List<SystemLanguage> _languages = new List<SystemLanguage> { SystemLanguage.English, SystemLanguage.Russian, SystemLanguage.French, SystemLanguage.German, SystemLanguage.Unknown };
        private int _languageIndex;

        public void ChangeIndex(int newIndex) { _languageIndex = newIndex; }
        public void ChangeLanguage()
        {
            Localize.SetCurrentLanguage(_languages[_languageIndex]);
            //LocalizeImage.SetCurrentLanguage(_languages[_languageIndex]);
        }
    }
}