﻿#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using CSharpMath.Forms;
using PocketLearn.Core;
using PocketLearn.Shared.Core.Learning;
using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace PocketLearn.Views.Converter
{
    public class CardContentConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int font = 14;
            if (parameter != null)
            {
                string[] split = parameter.ToString().Split('-'); // 20&400&200 (font, width, height)
                font = int.Parse(split[0]);
            }
            string directory = App.PlatformMediator.ApplicationConstants.GetDataPath();
            CardContent content = (CardContent)value;
            StackLayout container = new()
            {
                Orientation = StackOrientation.Vertical,
            };
            foreach (CardContentItem item in content.Items)
            {
                if (item.Type == CardContentItemType.Image)
                {
                    if (!File.Exists(Path.Combine(directory, item.Content)))
                    {
                        continue;
                    }
                    Image image = new()
                    {
                        Source = Path.Combine(directory, item.Content),
                        Margin = new Thickness(2),
                        HorizontalOptions = LayoutOptions.CenterAndExpand
                    };
                    container.Children.Add(image);
                }
                else if (item.Type == CardContentItemType.Text)
                {
                    if (content.ContainsLaTeX)
                    {
                        MathView latex = new MathView()
                        {
                            LaTeX = item.Content,
                            FontSize = font,
                            Margin = new Thickness(2),
                            TextColor = Utility.GetColorFromHex("#FFF").Color,
                            HorizontalOptions = LayoutOptions.CenterAndExpand
                        };
                        container.Children.Add(latex);
                    }
                    else 
                    {
                        Label textBlock = new()
                        {
                            Text = item.Content,
                            FontSize = font,
                            Margin = new Thickness(2),
                            TextColor = Utility.GetColorFromHex("#FFF").Color,
                            HorizontalOptions = LayoutOptions.CenterAndExpand
                        };
                        container.Children.Add(textBlock);
                    }
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
