﻿using PocketLearn.Core.Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketLearn.Win.MVVM.ViewModel
{
    public class QuestionVM : ObservableObject
    {
        public LearnProject Project { get; set; }
        public RelayCommand ShowAnswer { get; set; } = new RelayCommand(_ =>
        {
            MainWindowVM.Instance.CurrentView = MainWindowVM.Instance.AnswerVM;
            MainWindowVM.Instance.AnswerVM.Update();
        });

        public CardContent QuestionContent { get => QuestionContent;
            set
            {
                QuestionContent = value;
                RaisePropertyChanged();
            }
        }
        public void NextCard()
        {
            QuestionContent = Project.ShowNextCard().Item1;
        }
        public QuestionVM()
        {

        }
        public QuestionVM(LearnProject project)
        {
            Project = project;
        }
    }
}
