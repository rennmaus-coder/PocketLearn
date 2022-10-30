﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PocketLearn.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Environment = System.Environment;

namespace PocketLearn.Droid
{
    public class AndroidConstants : IApplicationConstants
    {
        public string GetDataPath() => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    }

    public class IOSConstants : IApplicationConstants
    {
        public string GetDataPath() => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    }
}