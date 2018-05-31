using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Core.App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : MasterDetailPage
	{
		public MasterPage ()
		{
			InitializeComponent ();
            App.Navigator = Navigator;
		}
	}
}