using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;

namespace MaterialMvvmSample.Views
{
    public partial class MaterialTextFieldView : ContentPage
    {
        public MaterialTextFieldView()
        {
            InitializeComponent();
            BindingContext = new MaterialTextFieldViewModel();
        }

        private void TestBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == "aa")
            {
                this.TestBox.ErrorText = "OH NOO";
                this.TestBox.HelperText = "OH NOO";
            }
            else
            {
                this.TestBox.ErrorText = null;
                this.TestBox.HelperText = null;
            }

            this.TestBox.HasError = !string.IsNullOrEmpty(this.TestBox.ErrorText);
        }

        private void TestButton_Clicked(object sender, System.EventArgs e)
        {
            this.TestBox.IsEnabled = !this.TestBox.IsEnabled;
        }

        private void Test2Button_Clicked(object sender, System.EventArgs e)
        {
            this.TestBox.Text += "aa";
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            this.TestBox.Text += "aab";
        }
    }
}
