using UnityEngine;

namespace Q17pD.Frostwatch
{
    public class PlayerPrefsInit : MonoBehaviour
    {
        //just an init for playerprefs vars if player ran a game in the first time
        //and also list of all playerprefs vars in Frostwatch namespace
        private void Start()
        {
            if(PlayerPrefs.GetInt("Inited") == 0)
            {
                PlayerPrefs.SetInt("LangIndex", 0);
                PlayerPrefs.SetInt("MusicVolume", 1);
                PlayerPrefs.SetInt("SFXVolume", 1);
                PlayerPrefs.SetInt("SFXToggleValue", 1);
                PlayerPrefs.SetInt("MusicToggleValue", 1);
                PlayerPrefs.SetInt("IsEndlessModeAvailable", 0);
            }
        }
    }
}
