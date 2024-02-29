using AcessHome.Models.Firebase;
using AcessHome.Services.Firebase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private string _Texto;

        private bool _isTaskRunning;       

        private DateTime _fechaSeleccionada;

        private ObservableCollection<VisitaUser> _visitas;

        #endregion VARIABLES

        #region CONSTRUCTOR

        public VisitasViewModel()
        {
            _settings = new FireBaseSettings();

            FechaSeleccionada = DateTime.Today;

            IsTaskRunning = false;

            Texto = "Obteniendo visitas";
        }

        #endregion CONSTRUCTOR

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

        public ObservableCollection<VisitaUser> Visitas
        {
            get { return _visitas; }

            set { SetValue(ref _visitas, value); }
        }

        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
        }

        #endregion OBJETOS

        #region PROCESOS

        public async Task ObtenerVisitas()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                IsTaskRunning = true;
                string fecha = FechaSeleccionada.ToString("yyyy-MM-dd");
                Visitas = await _settings.ObtenerVisitas(fecha);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Aviso", $"Ocurrio lo siguiente al obtener las visitas: {ex.Message}", "Cerrar");
            }
            finally
            {
                IsTaskRunning = false;
                IsBusy = false;
            }
        }

        #endregion PROCESOS

        #region COMANDOS

        public ICommand ObtenerVisitasCommand => new Command(async () => await ObtenerVisitas());

        #endregion COMANDOS
    }
}