// <copyright file="IPlatformService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;

namespace Drastic.YouTube.Explorer.Services
{
    /// <summary>
    /// Cross Platform Services.
    /// </summary>
    public interface IPlatformService
    {
        /// <summary>
        /// Share a URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>Task.</returns>
        Task ShareUrlAsync(string url);

        /// <summary>
        /// Open a URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>Task.</returns>
        Task OpenBrowserAsync(string url);

        /// <summary>
        /// Gets the temporary storage path for files.
        /// </summary>
        string TemporaryStoragePath { get; }

        /// <summary>
        /// Gets tne temporary storage clip path.
        /// </summary>
        string TemporaryClipStoragePath { get; }
    }
}