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
                if (obj is string question)
                {
                    if (learnCard.CardContent1.Items.Where(x => ((object)x).GetType() == typeof(string)).Last() == obj)
                    {
                        QuestionText.Text += $"{question}";
                    }
                    else
                    {
                        QuestionText.Text += $"{question}\n";
                    }
                }
                else if (obj is Bitmap bmp)
                {
                    QuestionImages.Items.Add(bmp.ToBitmapImage());
                }
            }
            foreach (object obj in learnCard.CardContent2.Items)
            {
                if (obj is string answer)
                {
                    if (learnCard.CardContent2.Items.Where(x => ((object)x).GetType() == typeof(string)).Last() == obj)
                    {
                        AnswerText.Text += $"{answer}";
                    }
                    else
                    {
                        AnswerText.Text += $"{answer}\n";
                    }
                }
                else if (obj is Bitmap bmp)
                {
                    AnswerImages.Items.Add(bmp.ToBitmapImage());
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
                ActiveCard.CardContent1.Items.Add(part);
            }
            foreach (BitmapImage bmp in QuestionImages.Items)
            {
                ActiveCard.CardContent1.Items.Add(bmp.ToBitmap());
            }

            foreach (string part in AnswerText.Text.Split('\n'))
            {
                ActiveCard.CardContent2.Items.Add(part);
            }
            foreach (BitmapImage bmp in AnswerImages.Items)
            {
                ActiveCard.CardContent2.Items.Add(bmp.ToBitmap());
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