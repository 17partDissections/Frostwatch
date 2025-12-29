using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Q17pD.Frostwatch.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private List<int> _animations = new List<int>();
        private int _index = -1;
        private void Start()
        {
            int itemAmount = GetComponent<PlayerItemHandler>().GetItemAmount();
            for (int i = 1; i <= itemAmount; i++) { _animations.Add(i - 1); }
        }
        public void ChangeAnimation(int index)
        {
            if (_index != -1) _animator.SetBool(_animations[_index].ToString(), false);
            _index = index; _animator.SetBool(_animations[_index].ToString(), true);
        }
    }
}
