using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Q17pD.Frostwatch.Menu
{
    public class EndlessModeCheck : MonoBehaviour
    {
        void Start()
        {
            bool isAvailable = PlayerPrefs.GetInt("IsEndlessModeAvailable") == 1;
            GetComponentInChildren<Button>().gameObject.SetActive(isAvailable);
            GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(!isAvailable);
        }
    }
}
