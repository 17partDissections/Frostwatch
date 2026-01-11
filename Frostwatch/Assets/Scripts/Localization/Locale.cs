/*
 * DFT Games Studios
 * All rights reserved 2009-Present
 */
using System.Collections.Generic;
using UnityEngine;

namespace DFTGames.Localization
{
    public static class Locale
    {
        const string STR_LOCALIZATION_KEY = "locale";
        const string STR_LOCALIZATION_PREFIX = "Localization/";
        static string currentLanguage;
        //static bool currentLanguageFileHasBeenFound = false;
        public static bool currentLanguageHasBeenSet = false;
        public static Dictionary<string, string> CurrentLanguageStrings = new Dictionary<string, string>();
        static TextAsset currentLocalizationText;

        public delegate void OnLanguageChangedDelegate();
        public static event OnLanguageChangedDelegate OnLanguageChanged;



        /// <summary>
        /// This sets the current language. It expects a standard .Net CultureInfo.Name format
        /// </summary>
        public static string CurrentLanguage
        {
            get { return currentLanguage; }
            set
            {
                if (value != null && value.Trim() != string.Empty)
                {
                    currentLanguage = value;
                    currentLocalizationText = Resources.Load(STR_LOCALIZATION_PREFIX + currentLanguage, typeof(TextAsset)) as TextAsset;
                    if (currentLocalizationText == null)
                    {
                        Debug.LogWarningFormat("Missing locale '{0}', loading English.", currentLanguage);
                        currentLanguage = SystemLanguage.English.ToString();
                        currentLocalizationText = Resources.Load(STR_LOCALIZATION_PREFIX + currentLanguage, typeof(TextAsset)) as TextAsset;
                    }
                    if (currentLocalizationText != null)
                    {
                        //currentLanguageFileHasBeenFound = true;
                        // We wplit on newlines to retrieve the key pairs
                        string[] lines = currentLocalizationText.text.Split(
                            new string[] { "\r\n", "\n\r", "\n" },
                            System.StringSplitOptions.RemoveEmptyEntries
                        );

                        CurrentLanguageStrings.Clear();

                        for (int i = 0; i < lines.Length; i++)
                        {
                            string line = lines[i].Trim();
                            if (string.IsNullOrEmpty(line) || line.StartsWith("#") || line.StartsWith("//")) continue;
                            string[] pairs = line.Split(new char[] { '\t', '=' }, 2, System.StringSplitOptions.RemoveEmptyEntries);
                            if (pairs.Length == 2)
                            {
                                string key = pairs[0].Trim();
                                string val = pairs[1].Trim();
                                key = key.Replace("\t", "");
                                CurrentLanguageStrings.Add(key, val);
                            }
                        }
                    }
                    else
                    {
                        Debug.LogErrorFormat("Locale Language '{0}' not found!", currentLanguage);
                    }
                    OnLanguageChanged?.Invoke();
                }
            }
        }


        public static bool CurrentLanguageHasBeenSet
        {
            get
            {
                return currentLanguageHasBeenSet;
            }
        }

        /// <summary>
        /// The player language. If not set in PlayerPrefs then returns Application.systemLanguage
        /// </summary>
        public static SystemLanguage PlayerLanguage
        {
            get
            {
                return (SystemLanguage)PlayerPrefs.GetInt(STR_LOCALIZATION_KEY, (int)Application.systemLanguage);
            }
            set
            {
                PlayerPrefs.SetInt(STR_LOCALIZATION_KEY, (int)value);
                PlayerPrefs.Save();
            }
        }
    }
}