﻿using AcessHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AcessHome.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersView : ContentPage
    {
        public UsersView()
        {
            InitializeComponent();
            BindingContext = new UsersViewModel();
        }
    }
}