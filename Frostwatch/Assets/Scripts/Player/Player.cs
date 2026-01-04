using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace Q17pD.Frostwatch.Player
{
    public class Player : MonoBehaviour
    {
        public Transform PickupableObjectsFinalTransform;
        [HideInInspector] public CinemachineBrain Brain;
        public List<CinemachineVirtualCamera> Cameras;
        [HideInInspector] public int CurrentCameraIndex;
        [HideInInspector] public PlayerItemHandler PlayerItemHandler;
        [HideInInspector] public PlayerCanvasHandler PlayerCanvasHandler;
        [HideInInspector] public PlayerAnimation PlayerAnimation;

        [Inject] private void Construct()
        {
            Brain = GetComponentInChildren<CinemachineBrain>();
            PlayerItemHandler = GetComponent<PlayerItemHandler>();
            PlayerCanvasHandler = GetComponentInChildren<PlayerCanvasHandler>();
            PlayerAnimation = GetComponent<PlayerAnimation>();
        }
    }
}
