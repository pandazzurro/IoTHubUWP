using Microsoft.Azure.Devices.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410

namespace AzureIoTHub.Device.UWP
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();


            try
            {
                Task.Run(async () =>
                {
                    string myConnectionDevice = "HostName=gabp2017.azure-devices.net;DeviceId=TestDevice;SharedAccessKey=3bG0qc+rxpaIyNT8ckCzM1BWJFRav+29mfGimdM7kig=";
                    DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(myConnectionDevice);

                    StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Images/GAB.png"));
                    var fileName = file.Name;
                    using (Windows.Storage.Streams.IRandomAccessStreamWithContentType stream = await file.OpenReadAsync())
                        await deviceClient.UploadToBlobAsync(fileName, stream.AsStream());
                }).Wait();
            }
            catch (Exception ex)
            {
                var a = ex;
            }
        }
    }
}
