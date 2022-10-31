// <copyright file="ThumbnailListControl.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.YouTube.Common;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Drastic.YouTube.Explorer.Win.UserControls
{
    public sealed partial class ThumbnailListControl : UserControl
    {
        public ThumbnailListControl()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public static readonly DependencyProperty ThumbnailListContentProperty = DependencyProperty.Register("ThumbnailList", typeof(FrameworkElement),
            typeof(ThumbnailListControl), new PropertyMetadata(null));

        public IReadOnlyList<Thumbnail> ThumbnailList
        {
            get { return (IReadOnlyList<Thumbnail>)GetValue(ThumbnailListContentProperty); }
            set { SetValue(ThumbnailListContentProperty, value); }
        }
    }
}
