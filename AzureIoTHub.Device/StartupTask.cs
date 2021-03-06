﻿using Windows.ApplicationModel.Background;
using System;
using Microsoft.Azure.Devices.Client;
using System.IO;
using Windows.Storage;
using System.Threading.Tasks;

namespace AzureIoTHub.Device
{
    public sealed class StartupTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
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