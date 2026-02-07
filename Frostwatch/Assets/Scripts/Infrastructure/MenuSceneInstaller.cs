using Q17pD.Frostwatch.Menu;
using TOZ.ImageFX;
using UnityEngine;
using Zenject;

namespace Q17pD.Frostwatch.Infrastructure
{
    public class MenuSceneInstaller : MonoInstaller
    {
        [SerializeField] private AudioHandler _audioHandler;
        [SerializeField] private PP_Pixelated _pixelated;
        public override void InstallBindings()
        {
            BindCursorHandler();
            BindAudioHandler();
            BindPixelation();
            BindForcedCameraRatio();
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
        private void BindPixelation()
        {
            Container
                .Bind<PP_Pixelated>()
                .FromInstance(_pixelated)
                .AsSingle()
                .NonLazy();
        }
        private void BindForcedCameraRatio()
        {
            Container
                .Bind<ForcedCameraRatio>()
                .FromInstance(GetComponent<ForcedCameraRatio>())
                .AsSingle()
                .NonLazy();
        }
    }
}
