using UnityEngine;

namespace Q17pD.Frostwatch.Menu
{
    public class ForcedRatioToggle : MonoBehaviour
    {
        [SerializeField] private GameObject _enabled, _disabled, _attention;
        private int _onEnableValue;

        private void OnEnable()
        {
            _onEnableValue = PlayerPrefs.GetInt("ForcedRatio");
            bool value = _onEnableValue == 1;
            _enabled.SetActive(value); _disabled.SetActive(!value);
            _attention.SetActive(false);
        }
        public void ChangeValue()
        {
            int value = _enabled.activeSelf ? 1 : 0;
            PlayerPrefs.SetInt("ForcedRatio", value);
            _attention.SetActive(value != _onEnableValue);
        }
        public void Save()
        {
            if (_onEnableValue == PlayerPrefs.GetInt("ForcedRatio")) return;
            if (PlayerPrefs.GetInt("ForcedRatio") == 1) Screen.SetResolution(800, 600, FullScreenMode.FullScreenWindow);
            else Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
            UnityEngine.SceneManagement.SceneManager.LoadScene(0); //yea ik i better do this somewhere else but i dont give a fuck
        }
    }
}