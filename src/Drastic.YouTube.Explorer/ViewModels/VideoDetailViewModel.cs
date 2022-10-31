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
        }

        public VideoDetailViewModel(Video video, IServiceProvider services)
           : base(services)
        {
            this.videoId = video.Id;
        }

        /// <summary>
        /// Gets or sets the video.
        /// </summary>
        public Video? Video
        {
            get => this.video;
            set => this.SetProperty(ref this.video, value);
        }

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
    }
}
