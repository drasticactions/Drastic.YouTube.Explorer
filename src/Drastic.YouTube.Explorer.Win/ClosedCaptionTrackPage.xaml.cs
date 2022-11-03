// <copyright file="ClosedCaptionTrackPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.DependencyInjection;
using Drastic.YouTube.Explorer.Models;
using Drastic.YouTube.Explorer.Tools;
using Drastic.YouTube.Explorer.Tools.Tools;
using Drastic.YouTube.Explorer.ViewModels;
using Drastic.YouTube.Videos;
using Drastic.YouTube.Videos.ClosedCaptions;
using Windows.Media.Core;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
namespace Drastic.YouTube.Explorer.Win
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClosedCaptionTrackPage : BasePage
    {
        public ClosedCaptionsViewModel Vm;

        public ClosedCaptionTrackPage(ClosedCaptionTrack track, ClosedCaptionTrackInfo info, Video video, IReadOnlyList<ClosedCaptionDetail> details)
        {
            this.InitializeComponent();
            this.DataContext = this.Vm = Ioc.Default.ResolveWith<ClosedCaptionsViewModel>(track, info, video, details);
            this.Vm.MediaPlayback += this.Vm_MediaPlayback;
        }

        public ClosedCaptionTrackPage(VideoId videoId, string language = "en")
        {
            this.InitializeComponent();
            this.DataContext = this.Vm = Ioc.Default.ResolveWith<ClosedCaptionsViewModel>(videoId, language);
            this.Vm.MediaPlayback += this.Vm_MediaPlayback;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (!this.DisposedValue)
            {
                if (disposing)
                {
                    this.Vm.MediaPlayback -= this.Vm_MediaPlayback;
                }
            }

            base.Dispose(disposing);
        }

        private void ListView_ItemClick(object sender, Microsoft.UI.Xaml.Controls.ItemClickEventArgs e)
        {
            if (e.ClickedItem is ClosedCaptionDetail detail)
            {
                if (this.Vm.DownloadClipCommand?.CanExecute(detail) ?? false)
                {
                    this.Vm.DownloadClipCommand.ExecuteAsync(detail).FireAndForgetSafeAsync();
            }
            }
        }

        private void Vm_MediaPlayback(object? sender, Events.MediaPlaybackEventArgs e)
        {
            switch (e.Type)
            {
                case Events.MediaPlaybackType.Play:
                    if (e.Arguments is string location)
                    {
                        this.TestPlayer.Source = MediaSource.CreateFromUri(new Uri(location));
                        this.TestPlayer.MediaPlayer.Play();
                    }

                    break;
            }
        }
    }
}
