using Xamarin.Forms;

namespace XF.Material.Forms.UI.Internals
{
    /// <inheritdoc />
    /// <summary>
    /// Used for rendering the <see cref="T:Xamarin.Forms.Entry" /> control in <see cref="T:XF.Material.Forms.UI.MaterialTextField" />.
    /// </summary>
    public class MaterialEntry : Entry
    {
        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(MaterialEntry), Material.Color.Secondary);

        public static readonly BindableProperty CursorColorProperty = BindableProperty.Create(nameof(CursorColor), typeof(Color), typeof(MaterialEntry), Material.Color.Secondary);

        public static readonly BindableProperty IsNumericKeyboardProperty = BindableProperty.Create(nameof(IsNumericKeyboard), typeof(bool), typeof(MaterialEntry), false);

        /// <summary>
        /// Hot reload requires a public constructor
        /// </summary>
        public MaterialEntry()
        {
        }

        public bool IsNumericKeyboard
        {
            get => (bool)GetValue(IsNumericKeyboardProperty);
            set => SetValue(IsNumericKeyboardProperty, value);
        }

        public Color TintColor
        {
            get => (Color)GetValue(TintColorProperty);
            set => SetValue(TintColorProperty, value);
        }

        public Color CursorColor
        {
            get => (Color)GetValue(CursorColorProperty);
            set => SetValue(CursorColorProperty, value);
        }
    }
}
