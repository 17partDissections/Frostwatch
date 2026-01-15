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
        [HideInInspector] public bool IsMoving;
        [HideInInspector] public bool IsHoldingItem;
        [HideInInspector] public PlayerRotation PlayerRotation;
        [HideInInspector] public PlayerAnimation PlayerAnimation;
        [HideInInspector] public PlayerItemHandler PlayerItemHandler;
        [HideInInspector] public PlayerCanvasHandler PlayerCanvasHandler;


        [Inject] private void Construct()
        {
            Brain = GetComponentInChildren<CinemachineBrain>();
            PlayerRotation = GetComponentInChildren<PlayerRotation>();
            PlayerAnimation = GetComponent<PlayerAnimation>();
            PlayerItemHandler = GetComponent<PlayerItemHandler>();
            PlayerCanvasHandler = GetComponentInChildren<PlayerCanvasHandler>();
        }
    }
}
