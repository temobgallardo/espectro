using MvvmCross;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.IoC;
using Spectrum.Core;
using Spectrum.Login.DeviceServices;
using Spectrum.Repository.Abstractions;
using System.Collections.Generic;
using System.Reflection;

namespace Spectrum.Droid
{
    public class Setup : MvxAppCompatSetup<App>
    {
        protected override IEnumerable<Assembly> AndroidViewAssemblies =>
            new List<Assembly>(base.AndroidViewAssemblies)
            {
                typeof(MvxRecyclerView).Assembly
            };

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IDatabaseDeviceLocation, DatabaseDeviceLocation>();
        }
    }
}