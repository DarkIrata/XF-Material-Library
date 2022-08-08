using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaterialMvvmSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialSwitch : ContentPage
    {
        public MaterialSwitch()
        {
            this.InitializeComponent();
            BindingContext = new MaterialSwitchViewModel();
        }
    }
}
