using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

namespace Q17pD.Frostwatch.Interactive
{
    public class Campfire : InteractiveObject
    {
        [SerializeField] Transform _fireObj;
        [Range(10,180)][SerializeField] private int _defaultSleep;
        [SerializeField] private List<CampfireStates> _states;
        [SerializeField] int _currentState = 3;
        private EventBus _eventBus;
        private int _delay = 0;

        [Inject] private void Construct(EventBus eventBus) { _eventBus = eventBus; }
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
                else { _currentState--; _eventBus.CampfireStateChanged?.Invoke(false, _states[_currentState].FrostAmount); }
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
            _eventBus.CampfireStateChanged?.Invoke(true, _states[_currentState].FrostAmount);
            StartCoroutine(WorkCoroutine());
        }

        public override void OnMouseDown()
        {
            throw new NotImplementedException();
        }
    }
    [Serializable] public class CampfireStates { public float ExtinguishDivider; public float VisualFireScale; public float FrostAmount; public bool InvokeMonster; }
}
