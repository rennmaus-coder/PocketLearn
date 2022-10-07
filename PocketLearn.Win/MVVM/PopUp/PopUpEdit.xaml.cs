﻿using PocketLearn.Core.Learning;
using PocketLearn.Win.Core;
using PocketLearn.Win.MVVM.ViewModel;
using Swan;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Image = System.Windows.Controls.Image;

namespace PocketLearn.Win.MVVM.PopUp
{
    /// <summary>
    /// Interaktionslogik für EditPopUp.xaml
    /// </summary>
    public partial class PopUpEdit : Window
    {
        public LearnCard ActiveCard { get; set; }
        public LearnProject LearnProject { get; set; }

        public PopUpEdit(LearnProject learnProject, LearnCard learnCard)
        {
            ActiveCard = learnCard;
            LearnProject = learnProject;

            InitializeComponent();
            foreach (object obj in learnCard.CardContent1.Items)
            {
                CardContentItem<object> item = (CardContentItem<object>)obj;
                if (item.Type == CardContentItemType.Text)
                {
                    if (learnCard.CardContent1.Items.Where(x => ((CardContentItem<object>)x).Type == CardContentItemType.Text).Last() == obj)
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
                    QuestionImages.Items.Add(new Bitmap((string)item.Content).ToBitmapImage());
                }
            }
            foreach (object obj in learnCard.CardContent2.Items)
            {
                CardContentItem<object> item = (CardContentItem<object>)obj;
                if (item.Type == CardContentItemType.Text)
                {
                    if (learnCard.CardContent2.Items.Where(x => ((CardContentItem<object>)x).Type == CardContentItemType.Text).Last() == obj)
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
                    AnswerImages.Items.Add(new Bitmap((string)item.Content).ToBitmapImage());
                }
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddImage(object sender, RoutedEventArgs e)
        {
            List<string> files = Utility.FileDialog("Images(*.jpg;*.bmp;*.png;*.tiff)|*.jpg;*.bmp;*.png;*.tiff", "Select images");
            foreach (string file in files)
            {
                AnswerImages.Items.Add(new Image() { Source = new Bitmap(file).ToBitmapImage() });
            }
        }

        private void AddImageAnswer(object sender, RoutedEventArgs e)
        {
            List<string> files = Utility.FileDialog("Images(*.jpg;*.bmp;*.png;*.tiff)|*.jpg;*.bmp;*.png;*.tiff", "Select images");
            foreach (string file in files)
            {
                AnswerImages.Items.Add(new Image() { Source = new Bitmap(file).ToBitmapImage() });
            }
        }

        private void Accept(object sender, RoutedEventArgs e)
        {
            ActiveCard.CardContent1.Items.Clear();
            ActiveCard.CardContent2.Items.Clear();
            foreach (string part in QuestionText.Text.Split('\n'))
            {
                ActiveCard.CardContent1.Items.Add(new CardContentItem<string>(part, CardContentItemType.Text));
            }
            foreach (Image bmp in QuestionImages.Items)
            {
                Bitmap image = ((BitmapImage)bmp.Source).ToBitmap();
                Guid imageGuid = Guid.NewGuid();
                image.Save(System.IO.Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images", imageGuid.ToString() + ".jpg"));

                ActiveCard.CardContent1.Items.Add(new CardContentItem<string>(imageGuid.ToString() + ".jpg", CardContentItemType.Image));
            }

            foreach (string part in AnswerText.Text.Split('\n'))
            {
                ActiveCard.CardContent2.Items.Add(new CardContentItem<string>(part, CardContentItemType.Text));
            }
            foreach (Image bmp in AnswerImages.Items)
            {
                Bitmap image = ((BitmapImage)bmp.Source).ToBitmap();
                Guid imageGuid = Guid.NewGuid();
                image.Save(System.IO.Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images", imageGuid.ToString() + ".jpg"));

                ActiveCard.CardContent2.Items.Add(new CardContentItem<string>(imageGuid.ToString() + ".jpg", CardContentItemType.Image));
            }
            MainWindowVM.Instance.EditVM.UpdateView(LearnProject);
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}