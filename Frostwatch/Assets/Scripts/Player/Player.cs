using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace Q17pD.Frostwatch.Player
{
    public class Player : MonoBehaviour
    {
        [HideInInspector] public CinemachineBrain Brain;
        public List<CinemachineVirtualCamera> Cameras;
        [HideInInspector] public int CurrentCameraIndex;
        [HideInInspector] public PlayerItemHandler PlayerItemHandler;
        
        [Inject] private void Construct() { Brain = GetComponentInChildren<CinemachineBrain>(); PlayerItemHandler = GetComponent<PlayerItemHandler>(); }
    }
}
