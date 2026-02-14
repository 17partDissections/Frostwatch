using DFTGames.Localization;
using Q17pD.Frostwatch.Inventory;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Q17pD.Frostwatch.Player
{
    public class PlayerCanvasHandler : MonoBehaviour
    {
        public Image DarkeningPanel;
        [SerializeField] private UIHighlight _itemInfoBg;
        [SerializeField] private LocalizeTMPro _name, _description;
        [SerializeField] private float _hightlightTime = 0.25f;
        private UIHighlight _nameHighlight, _descriptionHighlight;
        [SerializeField] private List<Button> _buttons;
        private List<InventoryAction> _currentActions = new List<InventoryAction>();
        private List<InventoryAction> _buttonActions = new List<InventoryAction> { null, null };
        private List<ActionVectors> _currentActionsVectors = new List<ActionVectors>();
        private bool _busy;

        private void Start()
        {
            _nameHighlight = _name.GetComponent<UIHighlight>();
            _descriptionHighlight = _description.GetComponent<UIHighlight>();
        }
        private void UpdateInfoLocales() { _name.UpdateLocale(); _description.UpdateLocale(); }
        public void SetObjectInfo(string name, string description)
        {
            _busy = true;
            _name.localizationKey = name;
            _description.localizationKey = description;
            _itemInfoBg.HighlightImage(0.7f, _hightlightTime);
            _nameHighlight.HighlightTMP(time: _hightlightTime);
            _descriptionHighlight.HighlightTMP(time: _hightlightTime);
            UpdateInfoLocales();
        }
        public void ClearInfo()
        {
            _busy = false;
            _itemInfoBg.UnHighlightImage(_hightlightTime);
            _nameHighlight.UnHighlightTMP(_hightlightTime);
            _descriptionHighlight.UnHighlightTMP(_hightlightTime);
        }
        public void UpdateActions(int CurrentCameraIndex, List<InventoryAction> actions = null, List<ActionVectors> actionsVectors = null)
        {
            HideActions();
            if (actions != null && actionsVectors != null) { _currentActions = actions; _currentActionsVectors = actionsVectors; }
            for (int i = 0; i < _currentActions.Count; i++)
            {
                if (_currentActionsVectors[i].Vectors[CurrentCameraIndex] && _currentActions[i].IsCustomConditionSatisfied)
                {
                    Button button = _buttons.FirstOrDefault(x => !x.gameObject.activeSelf);
                    _buttonActions[_buttons.FindIndex(x => !x.gameObject.activeSelf)] = _currentActions[i];
                    button.gameObject.SetActive(true);
                    LocalizeTMPro l = button.GetComponentInChildren<LocalizeTMPro>();
                    l.localizationKey = _currentActions[i].LocalizationKey;
                    l.UpdateLocale();
                }
            }
        }
        public void HideActions() { foreach (Button buttツ in _buttons) buttツ.gameObject.SetActive(false); }
        public void ActionButtonDown(int index) { _buttonActions[index].Act(); }
    }
}
