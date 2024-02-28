using AcessHome.Models.Firebase;
using AcessHome.Services.Firebase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AcessHome.ViewModels
{
    public class VisitasViewModel : BaseViewModel
    {

        #region VARIABLES

        private FireBaseSettings _settings;

        string _Texto;       

        string userName;
        string userPass;
        string userPass2;

        bool _isTaskRunning;

        List<VisitaUser> _visitas;

        DateTime _fechaSeleccionada;


        #endregion
        #region CONSTRUCTOR
        public VisitasViewModel()
        {
            _settings = new FireBaseSettings();

            FechaSeleccionada = DateTime.Today;

            IsTaskRunning = false;

            Texto = "Obteniendo visitas";

        }
        #endregion
        #region OBJETOS

        public bool IsTaskRunning
        {
            get { return _isTaskRunning; }

            set { SetValue(ref _isTaskRunning, value); }

        }


        public DateTime FechaSeleccionada
        {
            get { return _fechaSeleccionada; }
            set
            {
                SetValue(ref _fechaSeleccionada, value);
            }
        }


        public List<VisitaUser> Visitas
        {
            get { return _visitas; }

            set { SetValue(ref  _visitas, value); }

        }

        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
        }
     
        public string UserName
        {
            get { return userName; }

            set { SetValue(ref userName, value); }
        }


        public string UserPass
        {
            get { return userPass; }

            set { SetValue(ref userPass, value); }
        }

        public string UserPass2
        {
            get { return userPass2; }

            set { SetValue(ref userPass2, value); }
        }




        #endregion
        #region PROCESOS      
        public async Task ObtenerVisitas()
        {
            if(IsBusy) 
                return;

            try
            {
                IsBusy = true;
                string fecha = FechaSeleccionada.ToString("yyyy-MM-dd");

                Visitas = await _settings.ObtenerVisitas(fecha);


            }catch(Exception ex) 
            {
                await DisplayAlert("Aviso", $"Ocurrio lo siguiente al obtener las visitas: {ex.Message}", "Cerrar");
            }
            finally
            {
                IsBusy = false;
            }
        }
       
        #endregion
        #region COMANDOS
        public ICommand ObtenerVisitasCommand => new Command(async () => await ObtenerVisitas());        
        #endregion


    }
}
