using DFTGames.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Q17pD.Frostwatch
{
    public class Ending : MonoBehaviour
    {
        [SerializeField] private UIHighlight _darkeningPanel;
        [SerializeField] private AudioHandler _audioHandler;
        [SerializeField] private AudioClip _endingSound;
        [SerializeField] private LocalizeTMPro _end, _endMessage;
        [SerializeField] private Button _statisticsButton, _continueButton;
        [SerializeField] private List<Statistic> _statistics;
        

        private IEnumerator Start()
        {
            if (PlayerPrefs.GetString("EndingsUnlocked")[PlayerPrefs.GetInt("Ending") - 1] == '0')
            {
                char[] chars = PlayerPrefs.GetString("EndingsUnlocked").ToCharArray();
                chars[PlayerPrefs.GetInt("Ending") - 1] = '1';
                string newString = new string(chars);
                if (newString[1] == '1' || newString[2] == '1') PlayerPrefs.SetInt("IsEndlessModeAvailable", 1);
                PlayerPrefs.SetString("EndingsUnlocked", newString); 
            }
            yield return new WaitForSeconds(0.5f);
            _end.localizationKey = $"End{PlayerPrefs.GetInt("Ending")}"; _end.UpdateLocale();
            _endMessage.localizationKey = $"EndMessage{PlayerPrefs.GetString("EndingsUnlocked").Count(x => x == '1')}"; _endMessage.UpdateLocale();
            _audioHandler.PlaySound(SoundType.SFX, _endingSound);
            _darkeningPanel.UnHighlightImage(2);
            _end.GetComponent<UIHighlight>().HighlightTMP(1, 2);
            yield return new WaitForSeconds(3f);
            _endMessage.GetComponent<UIHighlight>().HighlightTMP(1, 1);
            yield return new WaitForSeconds(2f);
            _statisticsButton.GetComponentInChildren<UIHighlight>().HighlightTMP(1, 1);
            _continueButton.GetComponentInChildren<UIHighlight>().HighlightTMP(1, 1);
            yield return new WaitForSeconds(1f);
            _statisticsButton.enabled = true;
            _continueButton.enabled = true;
        }

    }
    [Serializable] public class Statistic { public string Name; public int Value; }
}
