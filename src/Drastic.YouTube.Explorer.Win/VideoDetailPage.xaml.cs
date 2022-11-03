// <copyright file="VideoDetailPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.DependencyInjection;
using Drastic.YouTube.Explorer.Tools.Tools;
using Drastic.YouTube.Explorer.ViewModels;
using Drastic.YouTube.Videos;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
namespace Drastic.YouTube.Explorer.Win
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VideoDetailPage : BasePage
    {
        public VideoDetailViewModel Vm;

        public VideoDetailPage(VideoId videoId)
            : base()
        {
            this.InitializeComponent();
            this.DataContext = this.Vm = Ioc.Default.ResolveWith<VideoDetailViewModel>(videoId);
        }

        public VideoDetailPage(Video video)
            : base()
        {
            this.InitializeComponent();
            this.DataContext = Ioc.Default.ResolveWith<VideoDetailViewModel>(video);
        }

        private string GetThumbnail(int i)
        {
            return this.Vm.Video?.Thumbnails[i].Url ?? string.Empty;
        }
    }
}
