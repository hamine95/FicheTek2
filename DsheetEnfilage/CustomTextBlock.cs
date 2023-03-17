using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DSheetEnfilage
{
    public class CustomTextBlock : Control
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(CustomTextBlock),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsMeasure,
                    (o, e) => ((CustomTextBlock)o).TextPropertyChanged((string)e.NewValue)));

        private FormattedText _formattedText;
        private GlyphTypeface glyphTypeface;

        static CustomTextBlock()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(MyTextBlockTest), new FrameworkPropertyMetadata(typeof(MyTextBlockTest)));
        }


        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private void TextPropertyChanged(string text)
        {
            var typeface = new Typeface(
                FontFamily,
                FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);

            var typef2 = new Typeface("Consolas").TryGetGlyphTypeface(out glyphTypeface);


            _formattedText = new FormattedText(
                text,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                typeface,
                8,
                Brushes.Black,
                VisualTreeHelper.GetDpi(this).PixelsPerDip);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (_formattedText != null) drawingContext.DrawText(_formattedText, new Point());
        }

        protected override Size MeasureOverride(Size constraint)
        {
            //return base.MeasureOverride(constraint);

            return _formattedText != null
                ? new Size(_formattedText.Width, _formattedText.Height)
                : new Size();
        }
    }
}