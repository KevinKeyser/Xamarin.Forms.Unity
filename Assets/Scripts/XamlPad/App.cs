using Xamarin.Forms;
using Xamarin.Forms.Platform.Unity;

namespace XamlPad
{
	public class App : Application
	{
		public App()
		{
			MainPage = new XamlPadPage();
			Application app = new Application();
			MainPage.BindingContext = new XamlPadBindingContext();
		}

		protected override void OnStart()
		{
			base.OnStart();
			(MainPage.BindingContext as XamlPadBindingContext)?.InitializePropertyValues();
		}

		protected override void OnSleep()
		{
			base.OnSleep();
		}

		protected override void OnResume()
		{
			base.OnResume();
		}
	}
}