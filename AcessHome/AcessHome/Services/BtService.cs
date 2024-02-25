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


        public BtService() 
        {

        }

        public async Task<bool> Conectar()
        {
            var adapter = DependencyService.Resolve<IBluetoothAdapter>();
            adapter.StartDiscovery();
            await Task.Delay(5000);
            _device = adapter.BondedDevices.FirstOrDefault(d => d.Name == "HC-05");            
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

        public async Task Abrir() 
        {                       
           var data = Encoding.ASCII.GetBytes("1");
           await _bluetoothConnection.TransmitAsync(data);
                                                                            
        }

        public async Task Cerrar()
        {
            var data = Encoding.ASCII.GetBytes("0");
            await _bluetoothConnection.TransmitAsync(data);
        }

    }
}
