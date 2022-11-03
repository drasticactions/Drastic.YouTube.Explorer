// <copyright file="CommonHelpers.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Reflection;
using Drastic.YouTube.Common;
using Drastic.YouTube.Explorer.Models;
using Drastic.YouTube.Videos;
using Drastic.YouTube.Videos.ClosedCaptions;

namespace Drastic.YouTube.Explorer.Tools
{
    public static class CommonHelpers
    {
        public static async Task<IReadOnlyList<StoryboardImage>> GetAllStoryboardImagesAsync(this YoutubeClient client, Video video, CancellationToken token = default)
        {
            var images = new List<StoryboardImage>();

            // TODO: Allow setting quality settings.
            var storyboardSet = video.Storyboards.LastOrDefault();

            if (storyboardSet is null)
            {
                return images;
            }

            foreach (var storyboard in storyboardSet.Storyboards)
            {
                var imageSet = await client.Videos.Storyboard.GetStoryboardImagesAsync(storyboard);
                images.AddRange(imageSet);
            }

            return images;
        }

        public static async Task<IReadOnlyList<ClosedCaptionDetail>> GetClosedCaptionDetailsAsync(this YoutubeClient client, Video video, ClosedCaptionTrack track, IReadOnlyList<StoryboardImage>? images = default, byte[]? defaultThumbnail = default, CancellationToken token = default)
        {
            var details = new List<ClosedCaptionDetail>();

            if (defaultThumbnail is null)
            {
                defaultThumbnail = GetResourceFileContent("Images.thumbnail.jpg").GetByteArray();
            }

            if (images is null)
            {
                images = await client.GetAllStoryboardImagesAsync(video, token);
            }

            foreach (var caption in track.Captions)
            {
                var image = images.FirstOrDefault(n => caption.Offset.TotalSeconds < n.Start);
                if (image is null)
                {
                    details.Add(new ClosedCaptionDetail(caption, defaultThumbnail));
                }
                else
                {
                    details.Add(new ClosedCaptionDetail(caption, image.Image));
                }
            }

            return details;
        }

        /// <summary>
        /// Get Resource File Content via FileName.
        /// </summary>
        /// <param name="fileName">Filename.</param>
        /// <returns>Stream.</returns>
        public static Stream GetResourceFileContent(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Drastic.YouTube.Explorer." + fileName;
            if (assembly is null)
            {
                throw new NullReferenceException(nameof(assembly));
            }

            return assembly.GetManifestResourceStream(resourceName) ?? throw new NullReferenceException(nameof(assembly));
        }

        private static byte[] GetByteArray(this Stream sm)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                sm.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
