using System.Windows.Input;
using Xamarin.Forms;

namespace MaterialMvvmSample.ViewModels
{
    public class MaterialSwitchViewModel : BaseViewModel
    {
        private bool isActive = true;

        public bool IsActive
        {
            get => this.isActive;
            set => this.Set(ref this.isActive, value, nameof(this.IsActive));
        }

        public ICommand SwitchIsActiveCommand { get; }

        public MaterialSwitchViewModel()
        {
            this.SwitchIsActiveCommand = new Command(() => this.IsActive = !this.IsActive);
        }
    }
}
