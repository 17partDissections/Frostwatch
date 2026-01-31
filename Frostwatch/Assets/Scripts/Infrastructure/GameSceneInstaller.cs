using Zenject;
using UnityEngine;
using Q17pD.Frostwatch.Interactive;

namespace Q17pD.Frostwatch.Infrastructure
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private AudioHandler _audioHandler;
        [SerializeField] private Player.Player _player;
        [SerializeField] private PickupableObject _branches;
        public override void InstallBindings()
        {
            BindCursorHandler();
            BindAudioHandler();
            BindPlayer();
            BindBranches();
        }

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
