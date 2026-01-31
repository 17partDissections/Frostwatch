using UnityEngine;

namespace Q17pD.Frostwatch.Player
{
    public class AnimationInvoker : MonoBehaviour
    {
        [SerializeField] private PlayerItemHandler _pih;

        public void Inv() { _pih.ContinueAddingItem(); }
    }
}
