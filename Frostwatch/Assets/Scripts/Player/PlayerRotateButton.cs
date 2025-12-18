using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Q17pD.Frostwatch.Player
{
    public class PlayerRotateButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
    {
        private PlayerRotation _playerRotation;
        [SerializeField] private RotationType _rotationType;
        [SerializeField] private bool _autoRotate;
        [SerializeField] private bool _showArea;  
        private void Start() 
        {
            _playerRotation = transform.parent.parent.GetComponentInChildren<PlayerRotation>();
            TryGetComponent<Image>(out Image image);
            if(_showArea) image.color = new Color(0,255,0,170); else image.color = new Color(0,255,0,0);
        }
        public void OnPointerEnter(PointerEventData eventData)  { if(_autoRotate) _playerRotation.Rotate(_rotationType);}
        public void OnPointerDown(PointerEventData eventData) { if(!_autoRotate) _playerRotation.Rotate(_rotationType);}
    }
}
