// <copyright file="DebugWindow.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.YouTube.Videos;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
namespace Drastic.YouTube.Explorer.Win
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DebugWindow : Window
    {
        public DebugWindow()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var videoId = VideoId.TryParse(this.DebugText.Text);
            if (videoId is null)
            {
                return;
            }

            var videoPage = new VideoDetailPage(videoId.Value);
            var popup = new BaseWindow(videoPage);
            popup.Activate();
        }
    }
}
