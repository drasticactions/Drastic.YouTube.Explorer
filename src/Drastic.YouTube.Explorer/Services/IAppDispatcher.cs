// <copyright file="IAppDispatcher.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;

namespace Drastic.YouTube.Explorer.Services
{
    /// <summary>
    /// App Dispatcher.
    /// </summary>
    public interface IAppDispatcher
    {
        /// <summary>
        /// Dispatch Command.
        /// </summary>
        /// <param name="action">Action to dispatch.</param>
        /// <returns>Bool if action was dispatched.</returns>
        bool Dispatch(Action action);
    }
}