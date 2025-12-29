using UnityEngine;
using Zenject;

namespace Q17pD.Frostwatch
{
    public class InventoryItem : MonoBehaviour
    {
        public int _index;
        [SerializeField] private string _localizationKey;
        [Range(1,2)] [SerializeField] private int _actionsAmount;
    }
}
