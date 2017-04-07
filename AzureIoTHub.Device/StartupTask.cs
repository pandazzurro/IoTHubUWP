using Windows.ApplicationModel.Background;
using System;
using Microsoft.Azure.Devices.Client;
using System.IO;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace AzureIoTHub.Device
{
    public sealed class StartupTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
            try
            {
                string myConnectionDevice = "HostName=gabp2017.azure-devices.net;DeviceId=RaspBerryDevice;SharedAccessKey=dXEQpCUTrZGWdMHNcC9hKHGv29p3S/LtFoDrb1Tr5cA=";
                DeviceClient client = DeviceClient.CreateFromConnectionString(myConnectionDevice);
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.Write("CiaoSonoUnMessaggio");
                writer.Flush();
                stream.Position = 0;
                await client.UploadToBlobAsync("demo", stream);
            }
            catch (Exception ex)
            {
                var a = ex;
            }
        }
    }
}