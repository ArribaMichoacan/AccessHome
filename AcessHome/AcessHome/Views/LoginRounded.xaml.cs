using AcessHome.Models;
using AcessHome.ViewModels;
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
    public partial class LoginRounded : ContentPage
    {
        public LoginRounded()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(Navigation);
        }
        
    }
}