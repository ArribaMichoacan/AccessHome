using AcessHome.Models;

using System.Collections.Generic;

using Xamarin.Forms;

namespace AcessHome
{
    public partial class MainPage : ContentPage
    {
      

        public MainPage()
        {
            BindingContext = new ViewModels.MainViewModel(Navigation);
            InitializeComponent();
            
        }                      
       
    }
}