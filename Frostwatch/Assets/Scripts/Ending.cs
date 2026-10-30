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
        [SerializeField] private GameObject _level;
        [SerializeField] private UIHighlight _darkeningPanel, _endlessModeUnlocked;
        [SerializeField] private AudioHandler _audioHandler;
        [SerializeField] private AudioClip _endingSound, _ambientSound, _endlessModeUnlockedSound;
        [SerializeField] private LocalizeTMPro _end, _endMessage;
        [SerializeField] private Button _statisticsButton, _continueButton;
        [SerializeField] private List<Statistic> _statistics;
        private CursorHandler _cursorHandler;
        private AudioSource _src;

        public void Statistics() => StartCoroutine(StatisticsCoroutine());
        public void Continue() => StartCoroutine(ContinueCoroutine());
        private IEnumerator Start()
        {
            _cursorHandler = GetComponent<CursorHandler>();
            _cursorHandler.LockCursorToggle(true);
            int ending = PlayerPrefs.GetInt("Ending");
            string endingsUnlocked = PlayerPrefs.GetString("EndingsUnlocked");
            if (ending != 4 && endingsUnlocked[ending - 1] == '0')
            {
                char[] chars = endingsUnlocked.ToCharArray();
                chars[ending - 1] = '1';
                string newString = new string(chars);
                if (newString[1] == '1' || newString[2] == '1')
                    PlayerPrefs.SetInt("IsEndlessModeAvailable", 1);
                PlayerPrefs.SetString("EndingsUnlocked", newString);
            }
            yield return new WaitForSeconds(3f);
            _end.localizationKey = $"End{PlayerPrefs.GetInt("Ending")}";
            _end.UpdateLocale();
            _endMessage.localizationKey = $"EndMessage{PlayerPrefs.GetString("EndingsUnlocked").Count(x => x == '1')}";
            _endMessage.UpdateLocale();

            _audioHandler.PlaySound(SoundType.SFX, _endingSound, 0.5f);
            _darkeningPanel.UnHighlightImage(2);
            _end.GetComponent<UIHighlight>().HighlightTMP(1, 2);
            yield return new WaitForSeconds(2f);

            _darkeningPanel.gameObject.SetActive(false);
            yield return new WaitForSeconds(2f);

            _endMessage.GetComponent<UIHighlight>().HighlightTMP(1, 1);
            yield return new WaitForSeconds(3f);

            _statisticsButton.GetComponentInChildren<UIHighlight>().HighlightTMP(1, 1);
            _continueButton.GetComponentInChildren<UIHighlight>().HighlightTMP(1, 1);
            yield return new WaitForSeconds(1f);

            _cursorHandler.LockCursorToggle(false);
            _statisticsButton.enabled = true;
            _continueButton.enabled = true;
        }

        private IEnumerator StatisticsCoroutine()
        {
            _cursorHandler.LockCursorToggle(true);
            _statisticsButton.enabled = false;
            _continueButton.enabled = false;

            _darkeningPanel.gameObject.SetActive(true);
            _darkeningPanel.HighlightImage(1, 2);
            yield return new WaitForSeconds(2.2f);
            _src = _audioHandler.PlaySound(SoundType.Music, _ambientSound, 0.5f, true, true, 2);
            _end.GetComponent<UIHighlight>().UnHighlightTMP(0.1f);
            _endMessage.GetComponent<UIHighlight>().UnHighlightTMP(0.1f);
            _statisticsButton.GetComponentInChildren<UIHighlight>().UnHighlightTMP(0.1f);
            _continueButton.GetComponentInChildren<UIHighlight>().UnHighlightTMP(0.1f);
            _statisticsButton.gameObject.SetActive(false);
            _level.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _darkeningPanel.UnHighlightImage(2);
            yield return new WaitForSeconds(2f);
            _darkeningPanel.gameObject.SetActive(false);

            foreach (var statistic in _statistics)
            {
                if (statistic.Name != "None")
                {
                    int value = PlayerPrefs.GetInt(statistic.Name);
                    var text = statistic.OptionalText;
                    int dots = Mathf.Max(0, Mathf.FloorToInt((text.rectTransform.rect.width - text.GetPreferredValues($"{text.text} {value}").x) / text.GetPreferredValues(".").x));
                    text.text = $"{text.text}{new string('.', dots)}{value}";
                }
                statistic.OptionalText.GetComponent<UIHighlight>().HighlightTMP(1, 1f);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(0.5f);
            _continueButton.GetComponentInChildren<UIHighlight>().HighlightTMP(1, 1);
            yield return new WaitForSeconds(1f);

            _cursorHandler.LockCursorToggle(false);
            _continueButton.enabled = true;
        }

        private IEnumerator ContinueCoroutine()
        {
            _cursorHandler.LockCursorToggle(true);
            _statisticsButton.enabled = false;
            _continueButton.enabled = false;

            if (_src != null) _audioHandler.StopSound(_src, true, 3);
            _end.GetComponent<UIHighlight>().UnHighlightTMP(0.1f);
            _endMessage.GetComponent<UIHighlight>().UnHighlightTMP(0.1f);
            _statisticsButton.GetComponentInChildren<UIHighlight>().UnHighlightTMP(0.1f);

            foreach (var statistic in _statistics)
            {
                var highlight = statistic.OptionalText.GetComponent<UIHighlight>();
                if (highlight != null) highlight.UnHighlightTMP(0.1f);
            }

            _continueButton.GetComponentInChildren<UIHighlight>().UnHighlightTMP(0.1f);
            _darkeningPanel.gameObject.SetActive(true);
            _darkeningPanel.HighlightImage(1, 2);
            if (PlayerPrefs.GetInt("IsEndlessModeAvailable") == 1)
            {
                _audioHandler.PlaySound(SoundType.SFX, _endlessModeUnlockedSound);
                _endlessModeUnlocked.HighlightTMP(1, 1);
                _darkeningPanel.UnHighlightImage(2);
                yield return new WaitForSeconds(6f);
                _endlessModeUnlocked.UnHighlightTMP(1);
            }
            yield return new WaitForSeconds(2.2f);
            UnityEngine.SceneManagement.SceneManager.LoadScene((PlayerPrefs.GetInt("Ending") == 1 || PlayerPrefs.GetInt("Ending") == 4) ? "Menu" : "Credits");
        }
    }

    [Serializable] public class Statistic { public string Name; public int OptionalValue; public TextMeshProUGUI OptionalText; }
}