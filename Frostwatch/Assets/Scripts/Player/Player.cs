using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Q17pD.Frostwatch.Player
{
    public class Player : MonoBehaviour
    {
        [HideInInspector] public CinemachineBrain Brain;
        public List<CinemachineVirtualCamera> Cameras;
        [HideInInspector]public int CurrentCameraIndex;
        
        private void Start() { Brain = GetComponentInChildren<CinemachineBrain>(); }
    }
}
