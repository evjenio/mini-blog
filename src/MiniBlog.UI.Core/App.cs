using MiniBlog.ServiceClient.Integration;
using MiniBlog.UI.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Serilog;

namespace MiniBlog.UI.Core
{
    /// <summary>
    /// Application abstraction.
    /// </summary>
	public class App : MvxApplication
	{

        /// <summary>
        /// Initializes DI container. Register dependencies here
        /// </summary>
		public override void Initialize()
		{
		    Mvx.LazyConstructAndRegisterSingleton<IBlogService>(() => new BlogServiceClient());

		    Mvx.LazyConstructAndRegisterSingleton<ILogger>(() => new LoggerConfiguration()
		        .MinimumLevel.Verbose()
		        .WriteTo.File("log.txt")
		        .CreateLogger());

            RegisterNavigationServiceAppStart<BlogViewModel>();
		}
	}
}
