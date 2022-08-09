using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Material.Forms.UI
{
    public class ActivatedEventArgs : ToggledEventArgs
    {
        public ActivatedEventArgs(bool value) : base(value)
        {
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialSwitch : ContentView
    {
        public static readonly BindableProperty ActiveTrackColorProperty = BindableProperty.Create(nameof(ActiveTrackColor), typeof(Color), typeof(MaterialSwitch), Material.Color.Secondary.MultiplyAlpha(0.54));

        public static readonly BindableProperty ActiveThumbColorProperty = BindableProperty.Create(nameof(ActiveThumbColor), typeof(Color), typeof(MaterialSwitch), Material.Color.Secondary);

        public static readonly BindableProperty InactiveTrackColorProperty = BindableProperty.Create(nameof(InactiveTrackColor), typeof(Color), typeof(MaterialSwitch), Color.LightGray);

        public static readonly BindableProperty InactiveThumbColorProperty = BindableProperty.Create(nameof(InactiveThumbColor), typeof(Color), typeof(MaterialSwitch), Color.FromHex("#FFFFFF"));

        public static readonly BindableProperty IsActivatedProperty = BindableProperty.Create(nameof(IsActivated), typeof(bool), typeof(MaterialSwitch), false, BindingMode.TwoWay);

        public static readonly BindableProperty ActiveTrackColorBrightnessProperty = BindableProperty.Create(nameof(ActiveTrackColorBrightness), typeof(double), typeof(MaterialSwitch), 0.0);
        public static readonly BindableProperty ActiveThumbColorBrightnessProperty = BindableProperty.Create(nameof(ActiveThumbColorBrightness), typeof(double), typeof(MaterialSwitch), 0.0);
        public static readonly BindableProperty InactiveTrackColorBrightnessProperty = BindableProperty.Create(nameof(InactiveTrackColorBrightness), typeof(double), typeof(MaterialSwitch), 0.0);
        public static readonly BindableProperty InactiveThumbColorBrightnessProperty = BindableProperty.Create(nameof(InactiveThumbColorBrightness), typeof(double), typeof(MaterialSwitch), 0.0);

        public MaterialSwitch()
        {
            InitializeComponent();
            this.UpdateTrackColor();
            this.OnActivatedChanged(this.IsActivated);
        }

        public event EventHandler<ActivatedEventArgs> Activated;

        public Color ActiveTrackColor
        {
            get => (Color)GetValue(ActiveTrackColorProperty);
            set => SetValue(ActiveTrackColorProperty, value);
        }

        public Color ActiveThumbColor
        {
            get => (Color)GetValue(ActiveThumbColorProperty);
            set => SetValue(ActiveThumbColorProperty, value);
        }

        public Color InactiveTrackColor
        {
            get => (Color)GetValue(InactiveTrackColorProperty);
            set => SetValue(InactiveTrackColorProperty, value);
        }

        public Color InactiveThumbColor
        {
            get => (Color)GetValue(InactiveThumbColorProperty);
            set => SetValue(InactiveThumbColorProperty, value);
        }

        public bool IsActivated
        {
            get => (bool)GetValue(IsActivatedProperty);
            set => SetValue(IsActivatedProperty, value);
        }

        public double ActiveTrackColorBrightness
        {
            get => (double)this.GetValue(ActiveTrackColorBrightnessProperty);
            set => this.SetValue(ActiveTrackColorBrightnessProperty, value);
        }

        public double ActiveThumbColorBrightness
        {
            get => (double)this.GetValue(ActiveThumbColorBrightnessProperty);
            set => this.SetValue(ActiveThumbColorBrightnessProperty, value);
        }

        public double InactiveTrackColorBrightness
        {
            get => (double)this.GetValue(InactiveTrackColorBrightnessProperty);
            set => this.SetValue(InactiveTrackColorBrightnessProperty, value);
        }

        public double InactiveThumbColorBrightness
        {
            get => (double)this.GetValue(InactiveThumbColorBrightnessProperty);
            set => this.SetValue(InactiveThumbColorBrightnessProperty, value);
        }

        protected virtual void OnActivatedChanged(bool isActivated)
        {
            Activated?.Invoke(this, new ActivatedEventArgs(IsActivated));

            Device.BeginInvokeOnMainThread(async () => await AnimateSwitchAsync(IsActivated));
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(IsActivated):
                case nameof(ActiveThumbColor):
                case nameof(InactiveThumbColor):
                    OnActivatedChanged(IsActivated);
                    break;
                case nameof(ActiveTrackColor):
                case nameof(InactiveTrackColor):
                    this.UpdateTrackColor();
                    break;
            }
        }

        private void UpdateTrackColor()
        {
            if (_background != null)
            {
                var color = IsActivated ? ActiveTrackColor : InactiveTrackColor;
                var adjustBy = IsActivated ? ActiveTrackColorBrightness : InactiveTrackColorBrightness;

                color = GetBrightnessAdjustedColor(color, adjustBy);

                _background.Color = color;
                _background.BackgroundColor = color;
            }
        }

        private void SetThumbColor(Color targetColor, double adjustBrightnessBy)
        {
            var color = this.GetBrightnessAdjustedColor(targetColor, adjustBrightnessBy);
            _thumb.BackgroundColor = color;
            _thumb.Background = color;

            _thumb.BorderColor = this.GetBrightnessAdjustedColor(targetColor, -0.15);
        }

#pragma warning disable CA1822 // Mark members as static
        private Color GetBrightnessAdjustedColor(Color color, double adjustBy)
#pragma warning restore CA1822 // Mark members as static
        {
            if (adjustBy == 0.0)
            {
                return color;
            }

            return new Color(color.R + adjustBy, color.G + adjustBy, color.B + adjustBy, color.A);
        }

        private async Task AnimateSwitchAsync(bool isActivated)
        {
            this.UpdateTrackColor();

            if (isActivated)
            {
                await AnimateToActivatedState();
            }
            else
            {
                await AnimateToUnactivatedState();
            }
        }

        private async Task AnimateToActivatedState()
        {
            this.SetThumbColor(this.ActiveThumbColor, this.ActiveThumbColorBrightness);
            await _thumb.TranslateTo(16, 0, 150, Easing.SinOut);
        }

        private async Task AnimateToUnactivatedState()
        {
            this.SetThumbColor(this.InactiveThumbColor, this.InactiveThumbColorBrightness);
            await _thumb.TranslateTo(0, 0, 100, Easing.SinOut);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            IsActivated = !IsActivated;
        }
    }
}
