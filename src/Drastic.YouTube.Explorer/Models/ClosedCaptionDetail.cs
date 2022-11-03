// <copyright file="ClosedCaptionDetail.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.YouTube.Common;
using Drastic.YouTube.Videos;
using Drastic.YouTube.Videos.ClosedCaptions;

namespace Drastic.YouTube.Explorer.Models
{
    public class ClosedCaptionDetail
    {
        public ClosedCaptionDetail(ClosedCaption caption, byte[] thumbnail)
        {
            this.Caption = caption;
            this.Thumbnail = thumbnail;
        }

        public ClosedCaption Caption { get; }

        public byte[] Thumbnail { get; }
    }
}
