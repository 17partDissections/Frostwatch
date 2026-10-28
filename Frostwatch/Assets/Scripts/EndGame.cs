using System.Collections.Generic;
using UnityEngine;

namespace Q17pD.Frostwatch
{
    public class EndGame : MonoBehaviour
    {
        [SerializeField] private List<Statistic> _statistics;
        public void End(int end)
        {
            PlayerPrefs.SetInt("Ending", end);
            if (PlayerPrefs.GetString("EndingsUnlocked")[end - 1] == '0')
            {
                char[] chars = PlayerPrefs.GetString("EndingsUnlocked").ToCharArray();
                chars[end - 1] = '1';
                string newString = new string(chars);
                PlayerPrefs.SetString("EndingsUnlocked", newString);
                Debug.Log(newString);
            }
            foreach (var statistic in _statistics) PlayerPrefs.SetInt(statistic.Name, statistic.Value);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Ending");
        }
    }
}
