using System;

namespace Xamarin.Forms.Platform.Unity
{
    public class VisualElementChangedEventArgs : ElementChangedEventArgs<VisualElement>
    {
        public VisualElementChangedEventArgs(VisualElement oldElement, VisualElement newElement) 
            : base(oldElement, newElement) { }
    }

    public class ElementChangedEventArgs<TElement> : EventArgs where TElement : Element
    {
        public TElement NewElement { get; }

        public TElement OldElement { get; }
        
        public ElementChangedEventArgs(TElement oldElement, TElement newElement)
        {
            OldElement = oldElement;
            NewElement = newElement;
        }
    }
}