using System;
using Xamarin.Forms.Internals;
using UnityEngine;

namespace Xamarin.Forms.Platform.Unity
{
    public interface IVisualElementRenderer : IRegisterable, IDisposable
    {
        /// <summary>
        /// Xamarin.Forms の VisualElement
        /// </summary>
        VisualElement Element { get; }
        
        NativeVisualElement NativeElement { get; }
        
        /// <summary>
        /// Unity Component (GameObject) の親となるコンテナ。
        /// 子はこの Transform を SetParent する。
        /// 通常は UnityComponent.transform 。
        /// </summary>
        Transform UnityContainerTransform { get; }

        event EventHandler<VisualElementChangedEventArgs> ElementChanged;

        SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint);

        void SetElement(VisualElement element);
    }
}