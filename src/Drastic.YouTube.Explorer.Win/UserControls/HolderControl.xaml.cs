// <copyright file="HolderControl.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.YouTube.Common;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Drastic.YouTube.Explorer.Win.UserControls
{
    public sealed partial class HolderControl : UserControl
    {
        public HolderControl()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(FrameworkElement),
            typeof(HolderControl), new PropertyMetadata(null));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty MainViewPaneContentProperty = DependencyProperty.Register("MainViewContent", typeof(FrameworkElement),
            typeof(HolderControl), new PropertyMetadata(null));

        public FrameworkElement MainViewContent
        {
            get { return (FrameworkElement)GetValue(MainViewPaneContentProperty); }
            set { SetValue(MainViewPaneContentProperty, value); }
        }
    }
}
