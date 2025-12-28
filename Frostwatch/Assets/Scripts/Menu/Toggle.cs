using Zenject;
    using UnityEngine;

namespace Q17pD.Frostwatch.Menu
{
    public abstract class Toggle<T> : MonoBehaviour where T : Behaviour
    {
        [SerializeField] private GameObject _buttonEnabled, _buttonDisabled;
        private T _t;
        protected abstract string PrefsKey { get; }

        [Inject] private void Construct(T t) { _t = t; }
        private void Awake()
        {
            int value = PlayerPrefs.GetInt(PrefsKey);
            _buttonEnabled.SetActive(value == 1);
            _buttonDisabled.SetActive(value == 0);
            _t.enabled = value == 1;
        }
        public void ChangeValue() { _t.enabled = _buttonEnabled.activeSelf; }

        public void Save() { PlayerPrefs.SetInt(PrefsKey, _t.enabled ? 1 : 0); }

        public void Revert()
        {
            int value = PlayerPrefs.GetInt(PrefsKey);
            bool shouldBeEnabled = value == 1;
            bool isEnabled = _buttonEnabled.activeSelf;
            if (shouldBeEnabled != isEnabled)
            {
                _buttonEnabled.SetActive(shouldBeEnabled);
                _buttonDisabled.SetActive(!shouldBeEnabled);
                _t.enabled = shouldBeEnabled;
            }
        }
    }
}
