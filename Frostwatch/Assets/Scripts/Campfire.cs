using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Q17pD.Frostwatch
{
    public class Campfire : InteractiveObject
    {
        [SerializeField] Transform _fireObj;
        [Range(10,180)][SerializeField] private int _defaultSleep;
        [SerializeField] private List<CampfireStates> _states;
        [SerializeField] int _currentState = 3;
        private int _delay = 0;

        protected override void Start()
        {
            base.Start();
            StartCoroutine(WorkCoroutine());
        }

        private IEnumerator WorkCoroutine()
        {
            while(_currentState != 0)
            {
                float s = _states[_currentState].VisualFireScale;
                _fireObj.DOScale(new Vector3(s, s, s), 1f);
                yield return new WaitForSeconds(_defaultSleep / _states[_currentState].ExtinguishDivider);
                if(_delay != 0) _delay--;
                else _currentState--;
            }
        }
        public void IncreaseState(int _branchAmount)
        {
            StopAllCoroutines();
            if((_branchAmount + _currentState) > (_states.Count - 1))
            {
                _currentState = (_states.Count - 1);
                _delay += (_branchAmount + _currentState) - (_states.Count - 1);
            }
            else _currentState += _branchAmount;
            StartCoroutine(WorkCoroutine());
        }

        public override void OnMouseDown()
        {
            throw new NotImplementedException();
        }
    }
    [Serializable] public class CampfireStates { public float ExtinguishDivider; public float VisualFireScale; public bool InvokeMonster; }
}
