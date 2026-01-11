using Zenject;
using UnityEngine;

namespace Q17pD.Frostwatch.Infrastructure
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private AudioHandler _audioHandler;
        [SerializeField] private Player.Player _player;
        public override void InstallBindings()
        {
            BindCursorHandler();
            BindAudioHandler();
            BindPlayer();
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
    }
}
