// <copyright file="BaseViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Drastic.YouTube.Explorer.Events;
using Drastic.YouTube.Explorer.Services;

namespace Drastic.YouTube.Explorer.ViewModels
{
    /// <summary>
    /// Base View Model.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        private bool isBusy;
        private string title = "Drastic.YouTube.Explorer";
        private string isLoadingText = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/>.</param>
        public BaseViewModel(IServiceProvider services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            this.Services = services;
            this.Client = services.GetService(typeof(YoutubeClient)) as YoutubeClient ?? throw new NullReferenceException(nameof(YoutubeClient));
            this.Dispatcher = services.GetService(typeof(IAppDispatcher)) as IAppDispatcher ?? throw new NullReferenceException(nameof(IAppDispatcher));
            this.ErrorHandler = services.GetService(typeof(IErrorHandlerService)) as IErrorHandlerService ?? throw new NullReferenceException(nameof(IErrorHandlerService));
            this.Platform = services.GetService(typeof(IPlatformService)) as IPlatformService ?? throw new NullReferenceException(nameof(IPlatformService));
        }

        /// <summary>
        /// Gets a baseline navigation handler.
        /// Handle this to handle navigation events within the view model.
        /// </summary>
        public event EventHandler<NavigationEventArgs>? Navigation;

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets or sets a value indicating whether the VM is busy.
        /// </summary>
        public bool IsBusy
        {
            get { return this.isBusy; }
            set { this.SetProperty(ref this.isBusy, value); }
        }

        /// <summary>
        /// Gets or sets the is loading text.
        /// </summary>
        public string IsLoadingText
        {
            get { return this.isLoadingText; }
            set { this.SetProperty(ref this.isLoadingText, value); }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        /// <summary>
        /// Gets the YouTube Client.
        /// </summary>
        internal YoutubeClient Client { get; }

        /// <summary>
        /// Gets the Error Handler.
        /// </summary>
        internal IErrorHandlerService ErrorHandler { get; }

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/>.
        /// </summary>
        internal IServiceProvider Services { get; }

        /// <summary>
        /// Gets the Dispatcher.
        /// </summary>
        internal IAppDispatcher Dispatcher { get; }

        /// <summary>
        /// Gets the Platform services.
        /// </summary>
        internal IPlatformService Platform { get; }

        /// <summary>
        /// Called when wanting to raise a Command Can Execute.
        /// </summary>
        public virtual void RaiseCanExecuteChanged()
        {
        }

        /// <summary>
        /// Called on VM Load.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        public virtual Task OnLoad()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Performs an Async task while setting the <see cref="IsBusy"/> variable.
        /// If the task throws, it is handled by <see cref="ErrorHandler"/>.
        /// </summary>
        /// <param name="action">Task to run.</param>
        /// <param name="isLoadingText">Optional Is Loading text.</param>
        /// <returns>Task.</returns>
        public async Task PerformBusyAsyncTask(Func<Task> action, string isLoadingText = "")
        {
            this.IsLoadingText = isLoadingText;
            this.IsBusy = true;

            try
            {
                await action.Invoke();
            }
            catch (Exception ex)
            {
                this.ErrorHandler.HandleError(ex);
            }

            this.IsBusy = false;
            this.IsLoadingText = string.Empty;
        }

        /// <summary>
        /// Sends a navigation request to whatever handlers attach to it.
        /// </summary>
        /// <param name="viewModel">The view model type.</param>
        /// <param name="arguments">Arguments to send to the view model.</param>
        public void SendNavigationRequest(Type viewModel, object? arguments = default)
        {
            if (viewModel.IsSubclassOf(typeof(BaseViewModel)))
            {
                this.Navigation?.Invoke(this, new NavigationEventArgs(viewModel, arguments));
            }
        }

#pragma warning disable SA1600 // Elements should be documented
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action? onChanged = null)
#pragma warning restore SA1600 // Elements should be documented
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            this.OnPropertyChanged(propertyName);
            this.RaiseCanExecuteChanged();
            return true;
        }

        /// <summary>
        /// On Property Changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.Dispatcher?.Dispatch(() =>
            {
                var changed = this.PropertyChanged;
                if (changed == null)
                {
                    return;
                }

                changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
            });
        }
    }
}