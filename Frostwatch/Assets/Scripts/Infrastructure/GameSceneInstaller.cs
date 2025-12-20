using Zenject;
using UnityEngine;
using Q17pD.Frostwatch.Player;
using Q17pD;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private AudioHandler _audioHandler;
    [SerializeField] private Player _player;
    public override void InstallBindings()
    {
        BindAudioHandler();
        BindPlayer();
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
            .Bind<Player>()
            .FromInstance(_player)
            .AsSingle()
            .NonLazy();
    }
}
