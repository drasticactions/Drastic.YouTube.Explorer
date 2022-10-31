// <copyright file="ThumbnailListControl.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.DependencyInjection;
using Drastic.YouTube.Common;
using Drastic.YouTube.Explorer.Services;
using Drastic.YouTube.Explorer.Tools;
using Drastic.YouTube.Explorer.Tools.Tools;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Drastic.YouTube.Explorer.Win.UserControls
{
    public sealed partial class ThumbnailListControl : UserControl
    {
        IPlatformService platform;

        public ThumbnailListControl()
        {
            this.InitializeComponent();
            this.DataContext = this;
            this.platform = Ioc.Default.GetService(typeof(IPlatformService)) as IPlatformService ?? throw new NullReferenceException(nameof(IPlatformService));
        }

        public static readonly DependencyProperty ThumbnailListContentProperty = DependencyProperty.Register("ThumbnailList", typeof(FrameworkElement),
            typeof(ThumbnailListControl), new PropertyMetadata(null));

        public IReadOnlyList<Thumbnail> ThumbnailList
        {
            get { return (IReadOnlyList<Thumbnail>)GetValue(ThumbnailListContentProperty); }
            set { SetValue(ThumbnailListContentProperty, value); }
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Thumbnail nail)
            {
                this.platform.OpenBrowserAsync(nail.Url).FireAndForgetSafeAsync();
            }
        }
    }
}
