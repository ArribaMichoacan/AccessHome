using AcessHome.Data;
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
        
      

        private async void GetVisitas()
        {
            DataBaseHelper bd = await DataBaseHelper.instance;
            List<Visita> consulta = new List<Visita>();
            await bd.GetAllVisitasByDate();
     
        }

       
    }
}