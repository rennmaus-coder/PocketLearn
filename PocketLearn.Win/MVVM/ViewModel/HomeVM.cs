﻿using PocketLearn.Core.Learning;
using PocketLearn.Win.Core;
using PocketLearn.Win.MVVM.Model;
using PocketLearn.Win.MVVM.PopUp;
using PocketLearn.Win.MVVM.View;
using System;
using System.Collections.Generic;
using System.Windows;

namespace PocketLearn.Win.MVVM.ViewModel
{
    public class HomeVM : ObservableObject
    {
        private List<object> _learningProjectsView;
        public List<object> LearningProjectsView
        {
            get => _learningProjectsView;
            set
            {
                _learningProjectsView = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand AddProject { get; set; }

        public HomeVM(ProjectManager projectManager)
        {
            UpdateView(projectManager);
            projectManager.ProjectsChanged += ProjectsChanged;

            AddProject = new RelayCommand(_ =>
            {
                new NewProjectPopUp(projectManager).Show();
            });
        }

        private void ProjectsChanged(object sender)
        {
            UpdateView((ProjectManager)sender);
        }

        public void UpdateView(ProjectManager projectManager)
        {
            List<object> view = new List<object>();
            foreach (LearnProject project in projectManager.LearnProjects)
            {
                view.Add(new LearningProjectControl(project));
            }
            LearningProjectsView = view;
        }
    }
}
