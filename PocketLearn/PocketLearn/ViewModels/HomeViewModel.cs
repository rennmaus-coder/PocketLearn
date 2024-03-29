﻿#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using PocketLearn.Core;
using PocketLearn.Shared.Core.Learning;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;

namespace PocketLearn.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public static HomeViewModel Instance { get; set; }

        public ProjectListViewModel ProjectListViewModel { get; set; }
        public QuestionViewModel QuestionViewModel { get; set; }
        public AnswerViewModel AnswerViewModel { get; set; }

        private string _test = "hadsfo";
        public string Test
        {
            get => _test;
            set => SetProperty(ref _test, value);
        }


        private object _current;
        public object Current 
        { 
            get => _current;
            set => SetProperty(ref _current, value);
        }

        

        public HomeViewModel()
        {
            Instance = this;
            ProjectListViewModel = new ProjectListViewModel();
            Current = ProjectListViewModel;
        }
    }
}
