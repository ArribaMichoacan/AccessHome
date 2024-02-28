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

        /// <summary>
        /// Method to connect to bluetooth device
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Conectar()
        {
            var adapter = DependencyService.Resolve<IBluetoothAdapter>();
            adapter.StartDiscovery(); //start searching for nearby devices
            await Task.Delay(5000);   //delay in order to give time to search devices

            // the device we're testing with it's an arduino hc-05 device.
            _device = adapter.BondedDevices.FirstOrDefault(d => d.Name == "HC-05"); //place here the name of your bluetooth device
            if (_device != null) //if it gets a device with the given name
            {
                _bluetoothConnection = adapter.CreateConnection(_device); //create a connection
                await _bluetoothConnection.ConnectAsync(); //try to connect with it
                return true; //if succesfully returns true
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Open the lock
        /// </summary>
        /// <returns></returns>
        public async Task Abrir()
        {
            //Our bluetooth device is connected to an arduino that receives a byte from us.The arduino opens a lock if it receives a 1 from us.

            var data = Encoding.ASCII.GetBytes("1");
            await _bluetoothConnection.TransmitAsync(data); //Sends a byte
        }

        /// <summary>
        /// Close the lock
        /// </summary>
        /// <returns></returns>
        public async Task Cerrar()
        {
            //The lock is closed when the bt devices receives a byte 0. 
            var data = Encoding.ASCII.GetBytes("0");
            await _bluetoothConnection.TransmitAsync(data);
        }
    }
}