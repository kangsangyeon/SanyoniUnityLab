using UnityEngine;
using Zenject;

public class TestInstaller : MonoInstaller
{
    [SerializeField] private IAudioService m_AudioService;

    public override void InstallBindings()
    {
        Container.Bind<IAudioService>().FromInstance(m_AudioService);
    }
}