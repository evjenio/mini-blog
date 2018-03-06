using System.Windows.Threading;
using MvvmCross.Core.ViewModels;
using MvvmCross.Wpf.Platform;
using MvvmCross.Wpf.Views.Presenters;

namespace MiniBlog.UI.WPF
{
    /// <summary>
    /// App Setup logic.
    /// </summary>
	public class Setup : MvxWpfSetup
	{
	    /// <inheritdoc />
	    public Setup(Dispatcher uiThreadDispatcher, IMvxWpfViewPresenter presenter) 
			: base(uiThreadDispatcher, presenter)
		{
		}

	    /// <inheritdoc />
	    protected override IMvxApplication CreateApp()
		{
			return new Core.App();
		}
	}
}
