/*
 * DFT Games Studios
 * All rights reserved 2009-Present
 */
using UnityEngine;
using TMPro;

namespace DFTGames.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizeTMPro : LocalizeBase
    {

        #region Private Fields

        private TextMeshProUGUI text;

        #endregion

        #region Public Methods

        /// <summary>
        /// Update the value of the Text we are attached to.
        /// </summary>
        public override void UpdateLocale()
        {
            if (!text) return; // catching race condition
            if(text.text != string.Empty) { text.text = string.Empty; }
            if (!System.String.IsNullOrEmpty(localizationKey) && Locale.CurrentLanguageStrings.ContainsKey(localizationKey))
                text.SetText(Locale.CurrentLanguageStrings[localizationKey].Replace(@"\n", "" + '\n'));
        }

        #endregion Public Methods


        protected override void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
            base.Start();
        }
    }
}