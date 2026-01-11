using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Zenject;
using System.Collections;

namespace Q17pD.Frostwatch.Player
{
    public class PlayerRotateButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [SerializeField] private RotationType _rotationType;
        [SerializeField] private bool _autoRotate;
        [SerializeField] private bool _showArea;
        private PlayerRotation _playerRotation;
        private Coroutine _repeatRoutine;
        private WaitForSeconds _cooldownWFS;

        [Inject] private void Construct(Player player)
        {
            _playerRotation = player.PlayerRotation;
            _cooldownWFS = new WaitForSeconds(_playerRotation.Cooldown * 3.5f);
            TryGetComponent<Image>(out Image image);
            if (_showArea) image.color = new Color(0, 255, 0, 170); else image.color = new Color(0, 255, 0, 0);
        }
        private IEnumerator RepeatRotationOnHover()
        {
            while (true)
            {
                yield return _cooldownWFS;
                if (_autoRotate) _playerRotation.Rotate(_rotationType);
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_autoRotate)
            {
                _playerRotation.Rotate(_rotationType);
                if (_repeatRoutine != null) StopCoroutine(_repeatRoutine);
                _repeatRoutine = StartCoroutine(RepeatRotationOnHover());
            }
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if (_repeatRoutine != null)
            {
                StopCoroutine(_repeatRoutine);
                _repeatRoutine = null;
            }
        }
        public void OnPointerDown(PointerEventData eventData) { if (!_autoRotate) _playerRotation.Rotate(_rotationType); }
    }
}
