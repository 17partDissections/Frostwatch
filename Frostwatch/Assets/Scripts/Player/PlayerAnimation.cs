using System.Collections.Generic;
using UnityEngine;

namespace Q17pD.Frostwatch.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private List<string> _animations = new List<string>();
        private int _index = -1;
        private void Start()
        {
            int itemAmount = GetComponent<PlayerItemHandler>().GetItemAmount();
            for (int i = 1; i <= itemAmount; i++) { _animations.Add((i - 1).ToString()); }
        }
        public void ChangeAnimation(int index)
        {
            if (index == -1) { _animator.SetBool("Idle", false); }
            else
            {
                string newAnim = _animations[index];
                _animator.SetBool(newAnim, true);
            }
            _index = index;
        }
    }
}
