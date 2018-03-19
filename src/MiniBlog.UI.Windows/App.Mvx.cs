using System;
using System.Windows;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Wpf.Views.Presenters;

namespace MiniBlog.UI.Windows
{
    /// <summary>
    /// App initialization logic
    /// </summary>
	public partial class App : Application
	{
		private bool setupComplete;

		private void DoSetup()
		{
			var presenter = new MvxWpfViewPresenter(MainWindow);

			var setup = new Setup(Dispatcher, presenter);
			setup.Initialize();

			var start = Mvx.Resolve<IMvxAppStart>();
			start.Start();

			setupComplete = true;
		}

	    /// <inheritdoc />
	    protected override void OnActivated(EventArgs e)
		{
			if (!setupComplete)
			{
				DoSetup();
			}

			base.OnActivated(e);
		}
	}
}
