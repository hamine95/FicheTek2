using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DSheetEnfilage
{
    public partial class EnfilageSchV2 : UserControl
    {
        private Point baselineOrigin;
        private GlyphTypeface glyphTypeface;

        public EnfilageSchV2()
        {
            InitializeComponent();
        }

        private void EnfilageSchV2_OnContentRendered(object sender, EventArgs e)
        {
        }

        private static GlyphRun ConvertTextLinesToGlyphRun(GlyphTypeface glyphTypeface, double renderingEmSize,
            double advanceWidth, double advanceHeight, Point baselineOrigin, string[] lines)
        {
            var glyphIndices = new List<ushort>();
            var advanceWidths = new List<double>();
            var glyphOffsets = new List<Point>();

            var y = baselineOrigin.Y;
            for (var i = 0; i < lines.Length; ++i)
            {
                var line = lines[i];

                var x = baselineOrigin.X;
                for (var j = 0; j < line.Length; ++j)
                {
                    var glyphIndex = glyphTypeface.CharacterToGlyphMap[line[j]];
                    glyphIndices.Add(glyphIndex);
                    advanceWidths.Add(0);
                    glyphOffsets.Add(new Point(x, y));

                    x += advanceWidth;
                }

                y += advanceHeight;
            }

            return new GlyphRun(
                glyphTypeface,
                0,
                false,
                renderingEmSize,
                glyphIndices,
                baselineOrigin,
                advanceWidths,
                glyphOffsets,
                null,
                null,
                null,
                null,
                null);
        }

        private Drawing Render()
        {
            new Typeface("Consolas").TryGetGlyphTypeface(out glyphTypeface);
            baselineOrigin = new Point(0, glyphTypeface.Baseline * 8);

            var lines = new string[1];
            for (var i = 0; i < lines.Length; ++i)
                lines[i] = "1";

            var drawing = new DrawingGroup();
            using (var drawingContext = drawing.Open())
            {
                // TODO: draw rectangles which represent background.

                // TODO: group of glyphs which has the same color should be drawn together.
                // Following code draws all glyphs in Red color.
                var glyphRun = ConvertTextLinesToGlyphRun(glyphTypeface, 8, 1, 1, baselineOrigin, lines);
                drawingContext.DrawGlyphRun(Brushes.Red, glyphRun);
            }

            return drawing;
        }


        private void DrawEnfilageArea()
        {
            var SquareWidth = Convert.ToInt32(GameArea.ActualWidth / 83);
            var SquareHeight = Convert.ToInt32(GameArea.ActualHeight / 59);
            var doneDrawingBackground = false;
            int nextX = 0, nextY = 0;
            var rowCounter = 0;
            var nextIsOdd = false;
            var NumCellWid = 1;
            var NumCellHei = 1;

            while (doneDrawingBackground == false)
            {
                var img = new Image
                {
                    Width = SquareWidth,
                    Height = SquareHeight
                };
                img.Source = new BitmapImage(new Uri(@"/Asset/square.png", UriKind.Relative));

                Canvas.SetTop(img, nextY);
                Canvas.SetLeft(img, nextX);
                GameArea.Children.Add(img);


                nextIsOdd = !nextIsOdd;
                nextX += SquareWidth;
                NumCellWid++;
                if (NumCellWid >= 83)
                {
                    nextX = 0;
                    NumCellWid = 1;
                    nextY += SquareHeight;
                    rowCounter++;
                    NumCellHei++;
                    nextIsOdd = rowCounter % 2 != 0;
                }

                if (NumCellHei >= 59)
                    doneDrawingBackground = true;
            }
        }

        private void EnfilageSchV2_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
        }

        private void EnfilageSchV2_OnLayoutUpdated(object sender, EventArgs e)
        {
        }

        private void EnfilageSchV2_OnLoaded(object sender, RoutedEventArgs e)
        {
            DrawEnfilageArea();
        }
    }
}