// <copyright file="VideoDetailViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.YouTube.Explorer.Tools;
using Drastic.YouTube.Videos;

namespace Drastic.YouTube.Explorer.ViewModels
{
    /// <summary>
    /// Video Detail View Model.
    /// </summary>
    public class VideoDetailViewModel : BaseViewModel
    {
        private VideoId videoId;
        private Video? video;

        public VideoDetailViewModel(VideoId id, IServiceProvider services)
            : base(services)
        {
            this.videoId = id;
            this.SetupCommands();
        }

        public VideoDetailViewModel(Video video, IServiceProvider services)
           : base(services)
        {
            this.videoId = video.Id;
            this.SetupCommands();
        }

        /// <summary>
        /// Gets the ShareLinkCommand.
        /// </summary>
        public AsyncCommand<Video>? ShareLinkCommand { get; private set; }

        /// <summary>
        /// Gets the OpenBrowserCommand.
        /// </summary>
        public AsyncCommand<Video>? OpenBrowserCommand { get; private set; }

        /// <summary>
        /// Gets or sets the video.
        /// </summary>
        public Video? Video
        {
            get => this.video;
            set => this.SetProperty(ref this.video, value);
        }

        /// <inheritdoc/>
        public override async Task OnLoad()
        {
            await base.OnLoad();

            if (this.Video is null)
            {
                this.PerformBusyAsyncTask(() => this.UpdateVideo(), Translations.Common.LoadingMetadata).FireAndForgetSafeAsync();
            }
        }

        private async Task UpdateVideo(CancellationToken token = default)
        {
            this.Video = await this.Client.Videos.GetAsync(this.videoId, token);
            this.UpdateTitle(this.Video.Title);
        }

        private Task ShareLinkAsync(Video item)
        {
            if (item is null)
            {
                return Task.CompletedTask;
            }

            return this.Platform.ShareUrlAsync(item.Url);
        }

        private Task OpenBrowserAsync(Video item)
        {
            if (item is null)
            {
                return Task.CompletedTask;
            }

            return this.Platform.OpenBrowserAsync(item.Url);
        }

        private void SetupCommands()
        {
            this.ShareLinkCommand = new AsyncCommand<Video>(
            async (item) => await this.ShareLinkAsync(item),
            (Video item) => item is not null && !this.IsBusy,
            this.ErrorHandler);

            this.OpenBrowserCommand = new AsyncCommand<Video>(
           async (item) => await this.OpenBrowserAsync(item),
           (Video item) => item is not null && !this.IsBusy,
           this.ErrorHandler);
        }
    }
}
