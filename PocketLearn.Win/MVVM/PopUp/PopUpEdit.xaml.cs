﻿#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using PocketLearn.Shared.Core.Learning;
using PocketLearn.Win.Core;
using PocketLearn.Win.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using Wpf.Ui.Controls;
using Image = System.Windows.Controls.Image;
using Path = System.IO.Path;

namespace PocketLearn.Win.MVVM.PopUp
{
    /// <summary>
    /// Interaktionslogik für EditPopUp.xaml
    /// </summary>
    public partial class PopUpEdit : UiWindow
    {
        public LearnCard ActiveCard { get; set; }
        public LearnProject LearnProject { get; set; }

        public PopUpEdit(LearnProject learnProject, LearnCard learnCard)
        {
            ActiveCard = learnCard;
            LearnProject = learnProject;
            
            InitializeComponent();
            ObservableCollection<string> data = new ObservableCollection<string>();
            data.Add("twoway");
            data.Add("oneway");
            data.Add("reverse oneway");
            CardTypeCombo.ItemsSource = data;
            if(ActiveCard.CardType != null)
            {
                switch(ActiveCard.CardType)
                {
                    case CardType.TwoWay:
                        CardTypeCombo.SelectedIndex = 0;
                        break;
                    case CardType.OneWay:
                        CardTypeCombo.SelectedIndex=1;
                        break;
                    case CardType.ReverseOneWay:
                        CardTypeCombo.SelectedIndex=2;
                        break;

                }
            }
            ContainsLaTeX.IsChecked = learnCard.CardContent1.ContainsLaTeX;

            foreach (object obj in learnCard.CardContent1.Items)
            {
                CardContentItem item = (CardContentItem)obj;
                if (item.Type == CardContentItemType.Text)
                {
                    if (learnCard.CardContent1.Items.Last(x => x.Type == CardContentItemType.Text) == item)
                    {
                        QuestionText.Text += $"{item.Content}";
                    }
                    else
                    {
                        QuestionText.Text += $"{item.Content}\n";
                    }
                }
                else if (item.Type == CardContentItemType.Image)
                {
                    if (!File.Exists(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images", item.Content)))
                    {
                        learnCard.CardContent1.Items.Remove(item);
                    }
                    using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images", item.Content))))
                    {
                        var decoder = BitmapDecoder.Create(ms,
                                                           BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                        BitmapSource source = decoder.Frames[0];
                        Image image = new()
                        {
                            Source = source,
                            Margin = new Thickness(2)
                        };
                        QuestionImages.Items.Add(image);
                    }
                }
            }
            foreach (object obj in learnCard.CardContent2.Items)
            {
                CardContentItem item = (CardContentItem)obj;
                if (item.Type == CardContentItemType.Text)
                {
                    if (learnCard.CardContent2.Items.Last(x => x.Type == CardContentItemType.Text) == item)
                    {
                        AnswerText.Text += $"{item.Content}";
                    }
                    else
                    {
                        AnswerText.Text += $"{item.Content}\n";
                    }
                }
                else if (item.Type == CardContentItemType.Image)
                {
                    if (!File.Exists(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images", item.Content)))
                    {
                        learnCard.CardContent2.Items.Remove(item);
                    }
                    using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images", item.Content))))
                    {
                        var decoder = BitmapDecoder.Create(ms,
                                                           BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                        BitmapSource source = decoder.Frames[0];
                        Image image = new()
                        {
                            Source = source,
                            Margin = new Thickness(2)
                        };
                        AnswerImages.Items.Add(image);
                    }
                }
            }
        }

        private void AddImage(object sender, RoutedEventArgs e)
        {
            int height = 90;
            int width = 200;
            List<string> files = Utility.FileDialog("Images(*.jpg;*.bmp;*.png;*.tiff)|*.jpg;*.bmp;*.png;*.tiff", "Select images");
            if (files == null) return;
            foreach (string file in files)
            {
                Stream s = File.OpenRead(file);
                System.Drawing.Image img = Bitmap.FromStream(s, false, false); // Read only the metadata
                double factor = img.Height / height;
                double targetwidth = img.Width / factor;
                if (targetwidth > width)
                {
                    factor = img.Width / width;
                }
                int resHeight = (int)(img.Height / factor);
                int resWidth = (int)(img.Width / factor);
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.UriSource = new Uri(file);
                bmp.DecodePixelHeight = resHeight;
                bmp.DecodePixelWidth = resWidth;
                bmp.EndInit();
                if (!bmp.IsFrozen) bmp.Freeze();
                s.Dispose();
                img.Dispose();

                Image image = new()
                {
                    Source = bmp,
                    Margin = new Thickness(2)
                };
                QuestionImages.Items.Add(image);
            }
        }

        private void AddImageAnswer(object sender, RoutedEventArgs e)
        {
            int height = 90;
            int width = 200;
            List<string> files = Utility.FileDialog("Images(*.jpg;*.bmp;*.png;*.tiff)|*.jpg;*.bmp;*.png;*.tiff", "Select images");
            if (files == null) return;
            foreach (string file in files)
            {
                Stream s = File.OpenRead(file);
                System.Drawing.Image img = Bitmap.FromStream(s, false, false); // Read only the metadata
                double factor = img.Height / height;
                double targetwidth = img.Width / factor;
                if (targetwidth > width)
                {
                    factor = img.Width / width;
                }
                int resHeight = (int)(img.Height / factor);
                int resWidth = (int)(img.Width / factor);
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.UriSource = new Uri(file);
                bmp.DecodePixelHeight = resHeight;
                bmp.DecodePixelWidth = resWidth;
                bmp.EndInit();
                if (!bmp.IsFrozen) bmp.Freeze();
                s.Dispose();
                img.Dispose();

                Image image = new()
                {
                    Source = bmp,
                    Margin = new Thickness(2)
                };
                AnswerImages.Items.Add(image);
            }
        }

        private void Accept(object sender, RoutedEventArgs e)
        {
            string directory = Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images");
            ActiveCard.CardContent1.ClearItems(directory);
            ActiveCard.CardContent2.ClearItems(directory);
            switch(CardTypeCombo.Text) {
                case "twoway":
                    ActiveCard.CardType = CardType.TwoWay;
                    break;
                case "oneway":
                    ActiveCard.CardType = CardType.OneWay;
                    break;
                case "reverse oneway":
                    ActiveCard.CardType = CardType.ReverseOneWay;
                    break;

            }
            ActiveCard.CardContent1.Items.Add(new CardContentItem(QuestionText.Text, CardContentItemType.Text));

            foreach (Image bmp in QuestionImages.Items)
            {
                Bitmap image = ((BitmapImage)bmp.Source).ToBitmap();
                Guid imageGuid = Guid.NewGuid();
                image.Save(Path.Combine(directory, imageGuid.ToString() + ".jpg"), Utility.GetEncoder(ImageFormat.Jpeg), Utility.GetCompression());

                ActiveCard.CardContent1.Items.Add(new CardContentItem(imageGuid.ToString() + ".jpg", CardContentItemType.Image));
            }

            ActiveCard.CardContent2.Items.Add(new CardContentItem(AnswerText.Text, CardContentItemType.Text));

            foreach (Image bmp in AnswerImages.Items)
            {
                Bitmap image = ((BitmapImage)bmp.Source).ToBitmap();
                Guid imageGuid = Guid.NewGuid();
                image.Save(Path.Combine(directory, imageGuid.ToString() + ".jpg"), Utility.GetEncoder(ImageFormat.Jpeg), Utility.GetCompression());

                ActiveCard.CardContent2.Items.Add(new CardContentItem(imageGuid.ToString() + ".jpg", CardContentItemType.Image));
            }
            ActiveCard.CardContent1.ContainsLaTeX = (bool)ContainsLaTeX.IsChecked;
            ActiveCard.CardContent2.ContainsLaTeX = (bool)ContainsLaTeX.IsChecked;

            ActiveCard.LastEdit = DateTime.Now;
            MainWindowVM.Instance.EditVM.UpdateView(LearnProject);

            LearnProject.LastEdit = DateTime.Now;
            Close();
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}