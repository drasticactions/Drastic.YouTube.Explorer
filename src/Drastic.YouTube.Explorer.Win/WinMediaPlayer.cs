// <copyright file="WinMediaPlayer.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.YouTube.Explorer.Services;
using Microsoft.UI.Xaml.Controls;
using WinRT;

namespace Drastic.YouTube.Explorer.Win
{
    public class WinMediaPlayer : MediaPlayerElement
    {
        public WinMediaPlayer()
        {
        }

        protected WinMediaPlayer(DerivedComposed _) : base(_)
        {
        }

        protected internal WinMediaPlayer(IObjectReference objRef) : base(objRef)
        {
        }

        public void PlayClip()
        {
        }
    }
}
