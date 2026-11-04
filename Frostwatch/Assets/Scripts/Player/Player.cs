using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TOZ.ImageFX;
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
        [HideInInspector] public int CurrentHeldItemIndex = -1;
        [HideInInspector] public bool IsMoving;
        [HideInInspector] public bool IsHoldingItem;
        [HideInInspector] public PlayerRotation PlayerRotation;
        [HideInInspector] public PlayerAnimation PlayerAnimation;
        [HideInInspector] public PlayerItemHandler PlayerItemHandler;
        [HideInInspector] public PlayerCanvasHandler PlayerCanvasHandler;
        [HideInInspector] public EventBus EventBus;
        [SerializeField] private EndGame _endGame;
        [SerializeField] private AudioClip _deathSound;
        private FrostEffect _frostEffect;
        private PP_Amnesia _amnezia;
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
            _frostEffect = GetComponentInChildren<FrostEffect>();
            _amnezia = GetComponentInChildren<PP_Amnesia>();
        }

        private void ChangeWarmState(bool increased, float frostAmount)
        {
            _frostEffect.FrostAmount = frostAmount;
            if (increased && _warm < 7) _warm++;
            else if (!increased && _warm > 0) _warm--;
            if (_warm == 0) StartCoroutine(DeathCoroutine());
        }
        private IEnumerator DeathCoroutine()
        {
            EventBus.PlayerDeath?.Invoke();
            _endGame.GetComponent<CursorHandler>().LockCursorToggle(true);
            _amnezia.enabled = true; _amnezia.Visibility = 0.05f;
            //PlayerAnimation.ChangeAnimation(9);
            yield return new WaitForSeconds(6);
            _endGame.End(4);
        }
    }
}