using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Platform.Unity
{
    public class VisualElementRenderer<TElement, TNativeElement> :
        IVisualElementRenderer, IEffectControlProvider
        where TElement : VisualElement
        where TNativeElement : NativeVisualElement

    {
        private VisualElementTracker<TElement, TNativeElement> tracker;
        private VisualElementPackager packager;

        public virtual Transform UnityContainerTransform => NativeElement ? NativeElement.RectTransform : null;

        public event EventHandler<VisualElementChangedEventArgs> ElementChanged;

        NativeVisualElement IVisualElementRenderer.NativeElement => NativeElement;
        public TNativeElement NativeElement { get; private set; }
        
        VisualElement IVisualElementRenderer.Element => Element;
        public TElement Element { get; private set; }

        public VisualElementRenderer()
        {
            NativeElement = CreateBaseComponent();
        }

        protected TNativeElement CreateBaseComponent()
        {
            var gameObject = new GameObject(typeof(TElement).Name, typeof(TNativeElement));

            var nativeElement = gameObject.GetComponent<TNativeElement>();
            nativeElement.BuildNativeRenderer();

            return nativeElement;
        }

        public void Dispose()
        {
            if (NativeElement == null)
            {
                return;
            }

            UnityEngine.Object.Destroy(NativeElement.gameObject);
            NativeElement = null;
        }
        
        public SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
        {
            return new SizeRequest(
                new Size(
                    Math.Min(NativeElement.RectTransform.rect.width, widthConstraint),
                    Math.Min(NativeElement.RectTransform.rect.height, heightConstraint)));
        }

        public void SetElement(VisualElement element)
        {
            var oldElement = Element;
            Element = (TElement)element;

            if (oldElement != null)
            {
                oldElement.PropertyChanged -= OnElementPropertyChanged;
                oldElement.FocusChangeRequested -= OnElementFocusChangeRequested;
            }

            if (element != null)
            {
                Element.PropertyChanged += OnElementPropertyChanged;
                Element.FocusChangeRequested += OnElementFocusChangeRequested;

                if (AutoPackage && packager == null)
                    packager = new VisualElementPackager(this);

                if (AutoTrack && Tracker == null)
                {
                    Tracker = new VisualElementTracker<TElement, TNativeElement>(Element, NativeElement);
                }

                // Disabled until reason for crashes with unhandled exceptions is discovered
                // Without this some layouts may end up with improper sizes, however their children
                // will position correctly
                //Loaded += (sender, args) => {
                if (packager != null)
                    packager.Load();

                //};
            }

            OnElementChanged(new ElementChangedEventArgs<TElement>(oldElement, Element));

            var controller = (IElementController)oldElement;

            if (controller != null && controller.EffectControlProvider == (IEffectControlProvider)this)
            {
                controller.EffectControlProvider = null;
            }

            controller = element;

            if (controller != null)
                controller.EffectControlProvider = this;
        }

        
        #region IEffectControlProvider
        void IEffectControlProvider.RegisterEffect(Effect effect)
        {
            throw new NotImplementedException();
        }
        #endregion

        /*-----------------------------------------------------------------*/

        #region Internals
        protected bool AutoPackage { get; set; } = true;

        protected bool AutoTrack { get; set; } = true;

        protected VisualElementTracker<TElement, TNativeElement> Tracker
        {
            get => tracker;
            set
            {
                if (Equals(tracker, value))
                {
                    return;
                }

                if (tracker != null)
                {
                    tracker.Updated -= OnTrackerUpdated;
                }

                tracker = value;

                if (tracker == null)
                {
                    return;
                }

                tracker.Updated += OnTrackerUpdated;
                UpdateTracker();
            }
        }

        protected virtual void UpdateBackgroundColor()
        {
            /*
            Color backgroundColor = Element.BackgroundColor;
            var control = Control as Control;
            if (control != null)
            {
                if (!backgroundColor.IsDefault)
                {
                    control.BackColor = backgroundColor.ToWindowsColor();
                }
                else
                {
                    control.BackColor = System.Drawing.SystemColors.Window;
                }
            }
            else
            {
                if (!backgroundColor.IsDefault)
                {
                    BackColor = backgroundColor.ToWindowsColor();
                }
                else
                {
                    BackColor = System.Drawing.SystemColors.Window;
                }
            }
            */
        }

        protected virtual void UpdateNativeControl()
        {
            UpdateEnabled();
            /*
            SetAutomationPropertiesHelpText();
            SetAutomationPropertiesName();
            SetAutomationPropertiesAccessibilityView();
            SetAutomationPropertiesLabeledBy();
            */
        }


        void UpdateEnabled()
        {
            if (NativeElement != null)
                NativeElement.gameObject.SetActive(Element.IsEnabled);

            /*else
                IsHitTestVisible = Element.IsEnabled && !Element.InputTransparent;*/
        }

        void UpdateTracker()
        {
            if (tracker == null)
                return;

            //_tracker.PreventGestureBubbling = PreventGestureBubbling;
            tracker.Element = Element;
        }
        #endregion

        /*-----------------------------------------------------------------*/

        #region Event Handler
        protected virtual void OnElementChanged(ElementChangedEventArgs<TElement> e)
        {
            var args = new VisualElementChangedEventArgs(e.OldElement, e.NewElement);
            ElementChanged?.Invoke(this, args);
        }

        protected virtual void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == VisualElement.IsEnabledProperty.PropertyName)
                UpdateEnabled();
            else if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName)
                UpdateBackgroundColor();

            /*
            else if (e.PropertyName == AutomationProperties.HelpTextProperty.PropertyName)
                SetAutomationPropertiesHelpText();
            else if (e.PropertyName == AutomationProperties.NameProperty.PropertyName)
                SetAutomationPropertiesName();
            else if (e.PropertyName == AutomationProperties.IsInAccessibleTreeProperty.PropertyName)
                SetAutomationPropertiesAccessibilityView();
            else if (e.PropertyName == AutomationProperties.LabeledByProperty.PropertyName)
                SetAutomationPropertiesLabeledBy();
            */
        }

        void OnControlGotFocus(object sender, EventArgs args)
        {
            ((IVisualElementController)Element).SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, true);
        }

        void OnControlLoaded(object sender, EventArgs args)
        {
            Element.IsNativeStateConsistent = true;
        }

        void OnControlLostFocus(object sender, EventArgs args)
        {
            ((IVisualElementController)Element).SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, false);
        }

        internal virtual void OnElementFocusChangeRequested(object sender, VisualElement.FocusRequestArgs args)
        {
            /*
            var control = Control as Control;
            if (control == null)
                return;
    
            if (args.Focus)
                args.Result = control.Focus(FocusState.Programmatic);
            else
            {
                UnfocusControl(control);
                args.Result = true;
            }
            */
        }

        void OnTrackerUpdated(object sender, EventArgs e)
        {
            UpdateNativeControl();
        }
        #endregion
    }
}