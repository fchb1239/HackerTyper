using ComputerInterface.Interfaces;
using Zenject;

namespace HackerTyper.ComputerInterface
{
    class MainInstaller : Installer
    {
        public override void InstallBindings()
        {
            base.Container.Bind<IComputerModEntry>().To<HackerEntry>().AsSingle();
        }
    }
}
