// <copyright file="ClosedCaptionsViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.YouTube.Exceptions;
using Drastic.YouTube.Explorer.Models;
using Drastic.YouTube.Explorer.Tools;
using Drastic.YouTube.Videos;
using Drastic.YouTube.Videos.ClosedCaptions;
using System.Collections.ObjectModel;

namespace Drastic.YouTube.Explorer.ViewModels
{
    public class ClosedCaptionsViewModel : BaseViewModel
    {
        private VideoId videoId;
        private Video? video;
        private ClosedCaptionTrack? track;
        private ClosedCaptionTrackInfo? info;
        private ClosedCaptionManifest? manifest;

        private string language = "en";

        public ClosedCaptionsViewModel(
    ClosedCaptionTrack track,
    ClosedCaptionTrackInfo info,
    Video video,
    IReadOnlyList<ClosedCaptionDetail> details,
    IServiceProvider services)
    : base(services)
        {
            this.Track = track;
            this.Video = video;
            this.videoId = video.Id;
            this.TrackInfo = info;

            foreach (var cap in details)
            {
                this.AllClosedCaptions.Add(cap);
            }

            this.ClosedCaptions = new ObservableCollection<ClosedCaptionDetail>(this.AllClosedCaptions);

            // TODO:
            // Thumbnail Cache
            // Match thumbnail to track
            // Filter?
            // await this.Youtube.Videos.Storyboard.GetStoryboardImagesAsync(storyboard);
            // Create database cache, link storyboard images to it.
        }

        public ClosedCaptionsViewModel(
            VideoId id,
            string language,
            IServiceProvider services)
            : base(services)
        {
            this.videoId = id;
            this.language = language;
            this.ClosedCaptions = new ObservableCollection<ClosedCaptionDetail>(this.AllClosedCaptions);
        }

        public ObservableCollection<ClosedCaptionDetail> AllClosedCaptions { get; }
    = new ObservableCollection<ClosedCaptionDetail>();

        public ObservableCollection<ClosedCaptionDetail> ClosedCaptions { get; }

        /// <summary>
        /// Gets or sets the track.
        /// </summary>
        public ClosedCaptionTrackInfo? TrackInfo
        {
            get => this.info;
            set => this.SetProperty(ref this.info, value);
        }

        /// <summary>
        /// Gets or sets the track.
        /// </summary>
        public ClosedCaptionTrack? Track
        {
            get => this.track;
            set => this.SetProperty(ref this.track, value);
        }

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

            if (!this.ClosedCaptions.Any())
            {
                this.PerformBusyAsyncTask(() => this.RefreshViewModel(), Translations.Common.LoadingMetadata).FireAndForgetSafeAsync();
            }

            this.UpdateTitle($"{this.Video?.Title} - {this.TrackInfo?.ToString()}");
        }

        private async Task RefreshViewModel(CancellationToken token = default)
        {
            // Get the video if we have not.
            if (this.Video is null)
            {
                this.Video = await this.Client.Videos.GetAsync(this.videoId, token);
            }

            // If we don't have track info, get it from the manifest and the inputed language string.
            if (this.TrackInfo is null)
            {
                this.manifest = await this.Client.Videos.ClosedCaptions.GetManifestAsync(this.videoId) ?? throw new DrasticYouTubeException("Could not get the closed caption manifest.");

                if (!this.manifest.Tracks.Any())
                {
                    throw new DrasticYouTubeException("No Closed Caption Manifests found for video.");
                }

                this.TrackInfo = this.manifest.TryGetByLanguage(this.language);
                if (this.TrackInfo is null)
                {
                    // Failed to get the language with the provided string, default to the first one.
                    this.TrackInfo = this.manifest.Tracks.First();
                }
            }

            this.Track = await this.Client.Videos.ClosedCaptions.GetAsync(this.TrackInfo) ?? throw new DrasticYouTubeException("Could not get the closed caption track.");

            var details = await this.Client.GetClosedCaptionDetailsAsync(this.Video, this.Track);

            this.AllClosedCaptions.Clear();

            foreach (var cap in details)
            {
                this.AllClosedCaptions.Add(cap);
            }
        }

        private void SetupCommands()
        {
        }
    }
}
