using Plugin.BluetoothClassic.Abstractions;
using System.Linq;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using AcessHome.Data;
using AcessHome.Models;
using System.Text;
namespace AcessHome.Services
{
    public class BtService
    {
        private IBluetoothConnection _bluetoothConnection;
        private BluetoothDeviceModel _device;

        public async Task<bool> Conectar()
        {
            var adapter = DependencyService.Resolve<IBluetoothAdapter>();

            adapter.StartDiscovery();
            await Task.Delay(5000);

            _device = adapter.BondedDevices.FirstOrDefault(d => d.Name == "HC-05");
            try
            {
                if (_device != null)
                {
                    _bluetoothConnection = adapter.CreateConnection(_device);
                    await _bluetoothConnection.ConnectAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async void Abrir() 
        {
            try
            {
               var data = Encoding.ASCII.GetBytes("1");
               await _bluetoothConnection.TransmitAsync(data);
               
            }
            catch
            {
               
            }
        }

        public async void Cerrar()
        {
            var data = Encoding.ASCII.GetBytes("0");
            await _bluetoothConnection.TransmitAsync(data);
        }

    }
}
