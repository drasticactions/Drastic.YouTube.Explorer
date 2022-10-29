// <copyright file="NavigationEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;

namespace Drastic.YouTube.Explorer.Events
{
    /// <summary>
    /// Navigation Event.
    /// </summary>
    public class NavigationEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationEventArgs"/> class.
        /// </summary>
        /// <param name="type">Type to navigate to.</param>
        /// <param name="arguments">Arguments to include.</param>
        public NavigationEventArgs(Type type, object? arguments)
        {
            ArgumentNullException.ThrowIfNull(type, nameof(type));
            this.Type = type;
            this.Arguments = arguments;
        }

        /// <summary>
        /// Gets the type to navigate to.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public object? Arguments { get; }
    }
}