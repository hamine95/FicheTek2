using Microsoft.Extensions.Logging;
using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.Plugin;
using Serilog;
using Serilog.Extensions.Logging;

namespace FrontEnd
{
    public class Setup : MvxWpfSetup<BackEnd2.App>
    {
        protected override ILoggerFactory CreateLogFactory()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel
                .Debug()
                .CreateLogger();
            return new SerilogLoggerFactory();
        }
        public override void LoadPlugins(IMvxPluginManager pluginManager)
        {
            base.LoadPlugins(pluginManager);
            pluginManager.EnsurePluginLoaded<MvvmCross.Plugin.MethodBinding.Plugin>(true);
        }

        protected override ILoggerProvider CreateLogProvider()
        {
            return new SerilogLoggerProvider();
        }
    }
}