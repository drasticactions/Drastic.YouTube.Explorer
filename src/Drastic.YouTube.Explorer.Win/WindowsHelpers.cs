// <copyright file="WindowsHelpers.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.UI.Composition;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;

namespace Drastic.YouTube.Explorer.Win
{
    public static class WindowsHelpers
    {
        // From https://xamlbrewer.wordpress.com/2017/02/27/creating-a-fluid-adaptive-ui-with-variablesizedwrapgrid-and-implicit-animations/
        // Which is from https://github.com/Microsoft/WindowsUIDevLabs/blob/master/SampleGallery/Samples/SDK%2014393/LayoutAnimations/Photos.xaml.cs
        public static void RegisterImplicitAnimations(this Panel panel)
        {
            var compositor = ElementCompositionPreview.GetElementVisual(panel).Compositor;

            // Create ImplicitAnimations Collection. 
            var elementImplicitAnimation = compositor.CreateImplicitAnimationCollection();

            // Define trigger and animation that should play when the trigger is triggered.
            elementImplicitAnimation["Offset"] = CreateOffsetAnimation(compositor);

            foreach (var item in panel.Children)
            {
                var elementVisual = ElementCompositionPreview.GetElementVisual(item);
                elementVisual.ImplicitAnimations = elementImplicitAnimation;
            }
        }

        private static CompositionAnimationGroup CreateOffsetAnimation(Compositor compositor)
        {
            // Define Offset Animation for the Animation group
            var offsetAnimation = compositor.CreateVector3KeyFrameAnimation();
            offsetAnimation.InsertExpressionKeyFrame(1.0f, "this.FinalValue");
            offsetAnimation.Duration = TimeSpan.FromSeconds(.4);

            // Define Animation Target for this animation to animate using definition. 
            offsetAnimation.Target = "Offset";

            // Define Rotation Animation for Animation Group. 
            var rotationAnimation = compositor.CreateScalarKeyFrameAnimation();
            rotationAnimation.InsertKeyFrame(.5f, 0.160f);
            rotationAnimation.InsertKeyFrame(1f, 0f);
            rotationAnimation.Duration = TimeSpan.FromSeconds(.4);

            // Define Animation Target for this animation to animate using definition. 
            rotationAnimation.Target = "RotationAngle";

            // Add Animations to Animation group. 
            var animationGroup = compositor.CreateAnimationGroup();
            animationGroup.Add(offsetAnimation);
            animationGroup.Add(rotationAnimation);

            return animationGroup;
        }
    }
}
