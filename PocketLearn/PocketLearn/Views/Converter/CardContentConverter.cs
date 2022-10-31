﻿using PocketLearn.Core;
using PocketLearn.Shared.Core.Learning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace PocketLearn.Views.Converter
{
    public class CardContentConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int font = 14;
            int width = 400;
            int height = 200;
            if (parameter != null)
            {
                string[] split = parameter.ToString().Split('-'); // 20&400&200 (font, width, height)
                font = int.Parse(split[0]);
                width = int.Parse(split[1]);
                height = int.Parse(split[2]);
            }
            string directory = Path.Combine(App.PlatformMediator.ApplicationConstants.GetDataPath(), "Images");
            CardContent content = (CardContent)value;
            StackLayout container = new()
            {
                Orientation = StackOrientation.Vertical,
            };
            foreach (CardContentItem item in content.Items)
            {
                if (((item).Type == CardContentItemType.Image))
                {
                    if (!File.Exists(Path.Combine(directory, item.Content)))
                    {
                        continue;
                    }
                    Bitmap bmp = new(Path.Combine(directory, item.Content));
                    int factor = Utility.GetSizeFactor(bmp.Height, height);
                    int targetwidth = bmp.Width / factor;
                    if (targetwidth > width)
                    {
                        factor = Utility.GetSizeFactor(bmp.Width, width);
                    }
                    Xamarin.Forms.Image image = new()
                    {
                        Source = bmp.ToBitmapImage(),
                        Margin = new System.Windows.Thickness(2),
                        MaxHeight = bmp.Height / factor,
                        MaxWidth = bmp.Width / factor
                    };
                    container.Children.Add(image);
                }
                else if ((item).Type == CardContentItemType.Text)
                {
                    TextBlock textBlock = new()
                    {
                        Text = item.Content,
                        FontSize = font,
                        Margin = new System.Windows.Thickness(2),
                        Foreground = (Brush)new BrushConverter().ConvertFromString("#FFF")
                    };
                    container.Children.Add(textBlock);
                }
            }
            return container;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
