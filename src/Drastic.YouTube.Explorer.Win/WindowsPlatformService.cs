// <copyright file="WindowsPlatformService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.YouTube.Explorer.Services;
using Windows.ApplicationModel.DataTransfer;

namespace Drastic.YouTube.Explorer.Win
{
    /// <summary>
    /// Windows Platform Services.
    /// </summary>
    public class WindowsPlatformService : IPlatformService
    {
        private static readonly Guid DtmIid = new Guid(0xa5caee9b, 0x8708, 0x49d1, 0x8d, 0x36, 0x67, 0xd2, 0x5a, 0x8d, 0xa0, 0x0c);

        public string TemporaryStoragePath => Windows.Storage.ApplicationData.Current.LocalCacheFolder.Path;

        public string TemporaryClipStoragePath
        {
            get
            {
                var path = Path.Combine(this.TemporaryStoragePath, "Clips");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }

        [System.Runtime.InteropServices.ComImport]
        [System.Runtime.InteropServices.Guid("3A3DCD6C-3EAB-43DC-BCDE-45671CE800C8")]
        [System.Runtime.InteropServices.InterfaceType(
        System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIUnknown)]
        private interface IDataTransferManagerInterop
        {
            IntPtr GetForWindow(
                [System.Runtime.InteropServices.In] IntPtr appWindow,
                [System.Runtime.InteropServices.In] ref Guid riid);

            void ShowShareUIForWindow(IntPtr appWindow);
        }

        /// <inheritdoc/>
        public async Task OpenBrowserAsync(string url)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(url));
        }

        /// <inheritdoc/>
        public unsafe Task ShareUrlAsync(string url)
        {
            var windowHandle = Windows.Win32.PInvoke.GetActiveWindow();
            IDataTransferManagerInterop interop = Windows.ApplicationModel.DataTransfer.DataTransferManager.As<IDataTransferManagerInterop>();
            Guid guid = Guid.Parse("a5caee9b-8708-49d1-8d36-67d25a8da00c");
            var iop = DataTransferManager.As<IDataTransferManagerInterop>();
            var transferManager = DataTransferManager.FromAbi(iop.GetForWindow(windowHandle, DtmIid));
            transferManager.DataRequested += (DataTransferManager dtm, DataRequestedEventArgs args) =>
            {
                DataPackage dataPackage = new DataPackage();
                dataPackage.SetWebLink(new Uri(url));
                dataPackage.Properties.Title = url;
                args.Request.Data = dataPackage;
            };

            interop.ShowShareUIForWindow(windowHandle);
            return Task.CompletedTask;
        }
    }
}
