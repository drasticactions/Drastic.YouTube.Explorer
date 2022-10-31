﻿// <copyright file="BasePage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.YouTube.Explorer.Tools;
using Drastic.YouTube.Explorer.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Drastic.YouTube.Explorer.Win
{
    public class BasePage : Page, IDisposable
    {
        private bool disposedValue;

        public BasePage()
        {
            this.Loaded += this.BasePage_Loaded;
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.Loaded -= this.BasePage_Loaded;
                }

                this.disposedValue = true;
            }
        }

        private void BasePage_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (this.DataContext is BaseViewModel vm)
            {
                vm.OnLoad().FireAndForgetSafeAsync();
            }
        }
    }
}