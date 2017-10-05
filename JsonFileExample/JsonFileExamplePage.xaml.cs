using Xamarin.Forms;

namespace JsonFileExample
{
    public partial class JsonFileExamplePage : ContentPage
    {
        public JsonFileExamplePage()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Settings","Settings Saved","Ok");
        }
    }
}
