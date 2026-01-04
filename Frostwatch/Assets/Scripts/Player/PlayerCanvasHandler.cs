using DFTGames.Localization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Q17pD.Frostwatch.Player
{
    public class PlayerCanvasHandler : MonoBehaviour
    {
        [SerializeField] private Image _itemInfoBg;
        [SerializeField] private LocalizeTMPro _name, _description;
        [SerializeField] private List<Button> _buttons;
        private bool _busy;

        private void UpdateLocales() { _name.UpdateLocale(); _description.UpdateLocale(); }
        public void SetObjectInfo(string name, string description) 
        {
            _busy = true;
            _itemInfoBg.enabled = true;
            _name.localizationKey = name;
            _description.localizationKey = description;
            UpdateLocales();
            }
        public void ClearInfo() 
        {
            _busy = false;
            _itemInfoBg.enabled = false;
            _name.localizationKey = string.Empty;
            _description.localizationKey = string.Empty;
            UpdateLocales();
        }
        public void SetActions(List<InventoryAction> actions)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                _buttons[i].gameObject.SetActive(true);
                _buttons[i].GetComponentInChildren<LocalizeTMPro>().localizationKey = actions[0].LocalizationKey;
            }
            UpdateLocales();
        }
    }
}
