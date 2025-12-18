using System;
using UnityEngine;

namespace Q17pD.Frostwatch
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private string _itemName;
        private Outline _outline;
        [SerializeField] private int _itemIndex;
        [Range(1, 5)][SerializeField] private int _amount;
        private void Start() 
        {
            _outline = GetComponent<Outline>();
            _outline.enabled = false;
        }
        private void OnMouseEnter()
        {
            _outline.enabled = true;
        }
        private void OnMouseExit()
        {
            _outline.enabled = false;
        }
        private void OnMouseDown()
        {
        }
    }
}
