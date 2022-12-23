using BackEnd.Database;
using MvvmCross;
using MvvmCross.ViewModels;

namespace BackEnd
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();
            Mvx.IoCProvider.RegisterSingleton(new DBContext());
            //RegisterAppStart<HomepageViewModel>();
            RegisterCustomAppStart<AppStart>();
        }
    }
}