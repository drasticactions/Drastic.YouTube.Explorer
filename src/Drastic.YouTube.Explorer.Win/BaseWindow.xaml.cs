// <copyright file="BaseWindow.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Drastic.YouTube.Explorer.Win
{
    /// <summary>
    /// Base Window.
    /// Pass in a page.
    /// </summary>
    public sealed partial class BaseWindow : Window
    {
        private Page page;

        public BaseWindow(Page page)
        {
            this.InitializeComponent();

            this.ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(this.AppTitleBar);

            this.MainFrame.Content = this.page = page;
            page.DataContextChanged += this.Page_DataContextChanged;
        }

        private void Page_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            this.MainGrid.DataContext = args.NewValue;
        }
    }
}
