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
        [HideInInspector] public EventBus EventBus;
        [SerializeField] private FrostEffect _frostEffect;
        private int _warm = 3;


        [Inject] private void Construct(EventBus eventBus)
        {
            Brain = GetComponentInChildren<CinemachineBrain>();
            PlayerRotation = GetComponentInChildren<PlayerRotation>();
            PlayerAnimation = GetComponent<PlayerAnimation>();
            PlayerItemHandler = GetComponent<PlayerItemHandler>();
            PlayerCanvasHandler = GetComponentInChildren<PlayerCanvasHandler>();
            EventBus = eventBus;
            EventBus.CampfireStateChanged += ChangeWarmState;
        }

        private void ChangeWarmState(bool increased, float frostAmount)
        {
            Debug.Log("changeWarmState");
            _frostEffect.FrostAmount = frostAmount;
            if (increased && _warm < 7) _warm++;
            else if (!increased && _warm > 0) _warm--;
            else if (!increased && _warm == 0)
            {
                Debug.Log("DEWA");
            }
        }
    }
}
