using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Zenject;
using System.Collections;

namespace Q17pD.Frostwatch.Player
{
    public class PlayerRotateButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        public bool AutoRotate;
        [SerializeField] private RotationType _rotationType;
        [SerializeField] private bool _showArea;
        private PlayerRotation _playerRotation;
        private Coroutine _repeatRoutine;
        private WaitForSeconds _cooldownWFS;
        private RectTransform _rectTransform;
        private bool _wasPointerInside;

        [Inject] private void Construct(Player player)
        {
            _playerRotation = player.PlayerRotation;
            _cooldownWFS = new WaitForSeconds(_playerRotation.Cooldown * 3.5f);
            TryGetComponent<Image>(out Image image);
            if (_showArea) image.color = new Color(0, 255, 0, 170); else image.color = new Color(0, 255, 0, 0);

            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            bool isPointerInside = IsPointerInExtendedZone();

            if (isPointerInside && !_wasPointerInside)
            {
                OnPointerEnterExtended();
            }
            else if (!isPointerInside && _wasPointerInside)
            {
                OnPointerExitExtended();
            }

            if (isPointerInside && !AutoRotate && Input.GetMouseButtonDown(0))
            {
                Rotate();
            }

            _wasPointerInside = isPointerInside;
        }

        private bool IsPointerInExtendedZone()
        {
            Vector2 mousePos = Input.mousePosition;
            Vector3[] corners = new Vector3[4];
            _rectTransform.GetWorldCorners(corners);

            float top = corners[1].y;
            float bottom = corners[0].y;

            if (mousePos.y < bottom || mousePos.y > top) return false;

            bool isLeftButton = _rotationType == RotationType.Left;

            if (isLeftButton)
            {
                return mousePos.x <= corners[2].x;
            }
            else
            {
                return mousePos.x >= corners[0].x;
            }
        }

        private void OnPointerEnterExtended()
        {
            if (AutoRotate)
            {
                _playerRotation.Rotate(_rotationType);
                if (_repeatRoutine != null) StopCoroutine(_repeatRoutine);
                _repeatRoutine = StartCoroutine(RepeatRotationOnHover());
            }
        }

        private void OnPointerExitExtended()
        {
            if (_repeatRoutine != null)
            {
                StopCoroutine(_repeatRoutine);
                _repeatRoutine = null;
            }
        }

        private IEnumerator RepeatRotationOnHover()
        {
            while (true)
            {
                yield return _cooldownWFS;
                if (AutoRotate) _playerRotation.Rotate(_rotationType);
            }
        }

        public void OnPointerEnter(PointerEventData eventData) { }
        public void OnPointerExit(PointerEventData eventData) { }
        public void OnPointerDown(PointerEventData eventData) { }

        public void Rotate() { _playerRotation.Rotate(_rotationType); }
    }
}