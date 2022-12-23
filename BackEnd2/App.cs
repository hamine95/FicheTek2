using BackEnd2.Data;
using BackEnd2.Database;
using MvvmCross;
using MvvmCross.ViewModels;

namespace BackEnd2
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();
            Mvx.IoCProvider.RegisterSingleton(new MyDBContext());
            Mvx.IoCProvider.RegisterSingleton(new SqliteData());
            //RegisterAppStart<HomepageViewModel>();
            RegisterCustomAppStart<AppStart>();
        }
    }
}