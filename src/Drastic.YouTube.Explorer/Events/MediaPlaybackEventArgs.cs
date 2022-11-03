// <copyright file="MediaPlaybackEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Drastic.YouTube.Explorer.Events
{
    public class MediaPlaybackEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaPlaybackEventArgs"/> class.
        /// </summary>
        /// <param name="type">Type to play to.</param>
        /// <param name="arguments">Arguments to include.</param>
        public MediaPlaybackEventArgs(MediaPlaybackType type, object? arguments)
        {
            ArgumentNullException.ThrowIfNull(type, nameof(type));
            this.Type = type;
            this.Arguments = arguments;
        }

        /// <summary>
        /// Gets the type to navigate to.
        /// </summary>
        public MediaPlaybackType Type { get; }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public object? Arguments { get; }
    }

    public enum MediaPlaybackType
    {
        Unknown,
        Play,
        Pause,
        Stop,
    }
}
