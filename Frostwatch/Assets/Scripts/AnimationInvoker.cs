using System.Collections;
using System.Collections.Generic;
using Q17pD.Frostwatch.Player;
using UnityEngine;

namespace Q17pD.Frostwatch
{
    public class AnimationInvoker : MonoBehaviour
    {
        [SerializeField] private PlayerItemHandler _pih;

        public void Inv() { _pih.ContinueAddingItem(); }
    }
}
