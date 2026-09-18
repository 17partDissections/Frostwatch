using DFTGames.Localization;
using Q17pD.Frostwatch.Inventory;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Q17pD.Frostwatch.Player
{
    public class PlayerCanvasHandler : MonoBehaviour
    {
        public Image DarkeningPanel;
        [SerializeField] private CursorHandler _cursorHandler;
        [SerializeField] private UIHighlight _itemInfoBg;
        [SerializeField] private LocalizeTMPro _name, _description;
        [SerializeField] private float _hightlightTime = 0.25f;
        private UIHighlight _nameHighlight, _descriptionHighlight;
        [SerializeField] private List<Button> _buttons;
        private List<InventoryAction> _currentActions = new List<InventoryAction>();
        private List<InventoryAction> _buttonActions = new List<InventoryAction> { null, null };
        private List<ActionVectors> _currentActionsVectors = new List<ActionVectors>();

        private void Start()
        {
            _nameHighlight = _name.GetComponent<UIHighlight>();
            _descriptionHighlight = _description.GetComponent<UIHighlight>();
        }
        private void UpdateInfoLocales() { _name.UpdateLocale(); _description.UpdateLocale(); }
        public void SetObjectInfo(string name, string description)
        {
            _name.localizationKey = name;
            _description.localizationKey = description;
            _itemInfoBg.HighlightImage(0.7f, _hightlightTime);
            _nameHighlight.HighlightTMP(time: _hightlightTime);
            _descriptionHighlight.HighlightTMP(time: _hightlightTime);
            UpdateInfoLocales();
        }
        public void ClearInfo()
        {
            _itemInfoBg.UnHighlightImage(_hightlightTime);
            _nameHighlight.UnHighlightTMP(_hightlightTime);
            _descriptionHighlight.UnHighlightTMP(_hightlightTime);
        }
        public void UpdateActions(int CurrentCameraIndex, List<InventoryAction> actions = null, List<ActionVectors> actionsVectors = null)
        {
            if (actions != null && actionsVectors != null)
            {
                _currentActions = new List<InventoryAction>(actions);
                _currentActionsVectors = new List<ActionVectors>(actionsVectors);
            }
            HideActions();
            if (_currentActions.Count == 0 || _currentActionsVectors.Count == 0) return;
            _buttonActions = new List<InventoryAction> { null, null };
            for (int i = 0; i < _currentActions.Count; i++)
            {
                if (_currentActionsVectors[i].Vectors[CurrentCameraIndex] && _currentActions[i].IsCustomConditionSatisfied)
                {
                    int buttonIndex = _buttons.FindIndex(x => !x.gameObject.activeSelf);
                    if (buttonIndex == -1) continue;
                    Button button = _buttons[buttonIndex];
                    _buttonActions[buttonIndex] = _currentActions[i];
                    LocalizeTMPro l = button.GetComponentInChildren<LocalizeTMPro>();
                    if (l != null) { l.localizationKey = _currentActions[i].LocalizationKey; l.UpdateLocale(); }
                    UIHighlight buttonHighlight = button.GetComponent<UIHighlight>();
                    UIHighlight textHighlight = l.GetComponent<UIHighlight>();
                    button.gameObject.SetActive(true);
                    buttonHighlight.UnHighlightImageFixedValues();
                    textHighlight.UnHighlightTMPFixedValues();
                }
            }
        }
        public void HideActions()
        {
            _cursorHandler.SetCursor("Default");
            for (int i = 0; i < _buttons.Count; i++)
            {
                Button buttツ = _buttons[i]; //i saved it ツ
                UIHighlight buttonHighlight = buttツ.GetComponent<UIHighlight>();
                if (buttonHighlight != null) buttonHighlight.UnHighlightImage(0.2f);
                UIHighlight textHighlight = buttツ.GetComponentInChildren<UIHighlight>();
                if (textHighlight != null) textHighlight.UnHighlightTMP(0.2f);
                LocalizeTMPro l = buttツ.GetComponentInChildren<LocalizeTMPro>();
                if (l != null) { l.localizationKey = ""; l.UpdateLocale(); }
                buttツ.gameObject.SetActive(false);
                if (i < _buttonActions.Count) _buttonActions[i] = null;
            }
        }
        public void ClearActions()
        {
            HideActions();
            _currentActions.Clear();
            _currentActionsVectors.Clear();
            _buttonActions = new List<InventoryAction> { null, null };
        }
        public void ActionButtonDown(int index) { if (_buttonActions[index] != null) { _buttonActions[index].Act(); _buttonActions[index] = null; } }
    }
}