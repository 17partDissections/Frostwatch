using Zenject;
using UnityEngine;
using Q17pD.Frostwatch.Interactive;
using System.Collections.Generic;

namespace Q17pD.Frostwatch.Infrastructure
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private bool _isCampaign;
        [SerializeField] private AudioHandler _audioHandler;
        [SerializeField] private Player.Player _player;
        [SerializeField] private PickupableObject _branches;
        public List<BoxCollider> Interactive;
        public override void InstallBindings()
        {
            foreach (var obj in Interactive) { obj.enabled = false; }
            BindCampaignBool();
            BindCursorHandler();
            BindAudioHandler();
            BindEventBus();
            BindPlayer();
            BindBranches();
        }
        public void BindCampaignBool() { Container.Bind<bool>().FromInstance(_isCampaign).AsSingle(); }
        private void BindCursorHandler()
        {
            Container
                .Bind<CursorHandler>()
                .FromInstance(GetComponent<CursorHandler>())
                .AsSingle()
                .NonLazy();
        }
        private void BindAudioHandler()
        {
            Container
                .Bind<AudioHandler>()
                .FromInstance(_audioHandler)
                .AsSingle()
                .NonLazy();
        }
        private void BindEventBus()
        {
            Container
                .Bind<EventBus>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
        private void BindPlayer()
        {
            Container
                .Bind<Player.Player>()
                .FromInstance(_player)
                .AsSingle()
                .NonLazy();
        }
        private void BindBranches()
        {
            Container
                .Bind<PickupableObject>()
                .FromInstance(_branches)
                .AsSingle()
                .NonLazy();
        }
    }
}
