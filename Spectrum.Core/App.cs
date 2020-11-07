using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Spectrum.Core.ViewModels;
using Spectrum.Repository.Abstractions;
using Spectrum.Repository.Services;

namespace Spectrum.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            //Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            Mvx
                .IoCProvider
                .LazyConstructAndRegisterSingleton<IDataAccessService, FakeUserAccessDataService>();
            RegisterAppStart<LoginViewModel>();
        }
    }
}
