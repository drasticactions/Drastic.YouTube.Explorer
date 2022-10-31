// <copyright file="VideoDetailPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Drastic.YouTube.Explorer.Tools.Tools;
using Drastic.YouTube.Explorer.ViewModels;
using Drastic.YouTube.Videos;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
namespace Drastic.YouTube.Explorer.Win
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VideoDetailPage : BasePage
    {
        private VideoDetailViewModel vm;

        public VideoDetailPage(VideoId videoId)
            : base()
        {
            this.InitializeComponent();
            this.DataContext = this.vm = Ioc.Default.ResolveWith<VideoDetailViewModel>(videoId);
        }

        public VideoDetailPage(Video video)
            : base()
        {
            this.InitializeComponent();
            this.DataContext = Ioc.Default.ResolveWith<VideoDetailViewModel>(video);
        }

        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = "Clicked";
            this.vm.IsBusy = true;
        }
    }
}
