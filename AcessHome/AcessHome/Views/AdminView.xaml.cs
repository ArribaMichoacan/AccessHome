﻿using AcessHome.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AcessHome.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminView : ContentPage
    {
        public AdminView()
        {
            BindingContext = new AdminViewModel();
            InitializeComponent();
           
        }
    }
}