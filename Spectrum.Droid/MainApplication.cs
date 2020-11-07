using Android.App;
using Android.Runtime;
using MvvmCross.Droid.Support.V7.AppCompat;
using Spectrum.Core;
using System;

namespace Spectrum.Droid
{
    [Application]
    public class MainApplication : MvxAppCompatApplication<Setup, App>
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            //UserDialogs.Init(this);
        }
    }
}