using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Xamarin.Forms.Platform.Unity
{
	public class VisualElementTracker<TElement, TNativeElement>
		where TElement : VisualElement
		where TNativeElement : NativeVisualElement
	{
		private readonly TNativeElement control;
		private TElement element;
		private bool invalidateArrangeNeeded;

		public TNativeElement Control => control;
		
		public TElement Element
		{
			get => element;
			set
			{
				if (Equals(element, value))
				{
					return;
				}

				if (element != null)
				{
					element.BatchCommitted -= OnRedrawNeeded;
					element.PropertyChanged -= OnPropertyChanged;
				}

				element = value;

				if (element != null)
				{
					element.BatchCommitted += OnRedrawNeeded;
					element.PropertyChanged += OnPropertyChanged;
				}

				UpdateNativeControl();
			}
		}

		public event EventHandler Updated;
		
		public VisualElementTracker(TElement element, TNativeElement control)
		{
			this.control = control;
			Element = element;
		}

		#region Event Handler

		protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (Element.Batched)
			{
				if (e.PropertyName == VisualElement.XProperty.PropertyName ||
					e.PropertyName == VisualElement.YProperty.PropertyName ||
					e.PropertyName == VisualElement.WidthProperty.PropertyName ||
					e.PropertyName == VisualElement.HeightProperty.PropertyName ||
					e.PropertyName == VisualElement.AnchorXProperty.PropertyName ||
					e.PropertyName == VisualElement.AnchorYProperty.PropertyName)
				{
					invalidateArrangeNeeded = true;
				}
				return;
			}

			if (e.PropertyName == VisualElement.XProperty.PropertyName ||
				e.PropertyName == VisualElement.YProperty.PropertyName ||
				e.PropertyName == VisualElement.WidthProperty.PropertyName ||
				e.PropertyName == VisualElement.HeightProperty.PropertyName ||
				e.PropertyName == VisualElement.AnchorXProperty.PropertyName ||
				e.PropertyName == VisualElement.AnchorYProperty.PropertyName)
			{
				MaybeInvalidate();
			}
			else if (e.PropertyName == VisualElement.ScaleProperty.PropertyName)
			{
				UpdateScale(Element, Control);
			}
			else if (e.PropertyName == VisualElement.TranslationXProperty.PropertyName ||
					 e.PropertyName == VisualElement.TranslationYProperty.PropertyName ||
					 e.PropertyName == VisualElement.RotationProperty.PropertyName ||
					 e.PropertyName == VisualElement.RotationXProperty.PropertyName ||
					 e.PropertyName == VisualElement.RotationYProperty.PropertyName)
			{
				UpdateRotation(Element, Control);
			}
			else if (e.PropertyName == VisualElement.IsVisibleProperty.PropertyName)
			{
				UpdateVisibility(Element, Control);
			}
			else if (e.PropertyName == VisualElement.OpacityProperty.PropertyName)
			{
				UpdateOpacity(Element, Control);
			}
			else if (e.PropertyName == VisualElement.InputTransparentProperty.PropertyName)
			{
				UpdateInputTransparent(Element, Control);
			}
		}

		#endregion

		/*-----------------------------------------------------------------*/
		#region Internals

		protected virtual void UpdateNativeControl()
		{
			if (Element == null || Control == null)
			{
				return;
			}

			UpdateVisibility(Element, Control);
			UpdateOpacity(Element, Control);
			UpdatePositionSizeAnchor(Element, Control);
			UpdateScale(Element, Control);
			UpdateRotation(Element, Control);
			UpdateInputTransparent(Element, Control);

			if (invalidateArrangeNeeded)
			{
				MaybeInvalidate();
				invalidateArrangeNeeded = false;
			}

			OnUpdated();
		}

		private void OnUpdated()
		{
			Updated?.Invoke(this, EventArgs.Empty);
		}

		private void OnRedrawNeeded(object sender, EventArgs e)
		{
			UpdateNativeControl();
		}

		private void MaybeInvalidate()
		{
			if (Element.IsInNativeLayout)
			{
				return;
			}

			//var parent = (Control)Container.Parent;
			//parent?.InvalidateMeasure();
			//Container.InvalidateMeasure();
		}

		private static void UpdateInputTransparent(VisualElement view, NativeVisualElement control)
		{
			control.gameObject.SetActive(view.IsEnabled && view.IsVisible && !view.InputTransparent);
		}

		private static void UpdateOpacity(VisualElement view, NativeVisualElement control)
		{
			control.Opacity = view.Opacity;
		}

		private static void UpdatePositionSizeAnchor(VisualElement view, NativeVisualElement control)
		{
			var position = new Vector2((float)view.X, (float)view.Y);
			var size = new Vector2(Mathf.Max((float)view.Width, 0.0f), Mathf.Max((float)view.Height, 0.0f));
			var pivot = new Vector2((float)view.AnchorX, (float)view.AnchorY);
			var ap = new Vector2(position.x + size.x * pivot.x, -(position.y + size.y * pivot.y));

			/*var parent = view.Parent as VisualElement;
			if (parent != null)
			{
				var parentRenderer = Platform.GetRenderer(parent);
				if (parentRenderer != null)
				{
					ap.y = -ap.y;
				}
			}*/

			control.RectTransform.anchorMin = new Vector2(0.0f, 1.0f);
			control.RectTransform.anchorMax = new Vector2(0.0f, 1.0f);
			control.RectTransform.anchoredPosition = ap;
			control.RectTransform.sizeDelta = size;
			control.RectTransform.pivot = pivot;

			//Debug.Log(string.Format("Layout: {0} ({1}) pt={2} sz={3} pivot={4} ancpt={5}",
			//	view.GetType(), rectTransform.GetInstanceID(),
			//	position, size, pivot, ap));
		}

		private static void UpdateRotation(VisualElement view, NativeVisualElement control)
		{
			control.RectTransform.localEulerAngles = new Vector3((float)view.RotationX, (float)view.RotationY, (float)view.Rotation);
			/*
			double anchorX = view.AnchorX;
			double anchorY = view.AnchorY;
			double rotationX = view.RotationX;
			double rotationY = view.RotationY;
			double rotation = view.Rotation;
			double translationX = view.TranslationX;
			double translationY = view.TranslationY;
			double scale = view.Scale;

			if (rotationX % 360 == 0 && rotationY % 360 == 0 && rotation % 360 == 0 && translationX == 0 && translationY == 0 && scale == 1)
			{
				control.Projection = null;
			}
			else
			{
				control.Projection = new PlaneProjection
				{
					CenterOfRotationX = anchorX,
					CenterOfRotationY = anchorY,
					GlobalOffsetX = scale == 0 ? 0 : translationX / scale,
					GlobalOffsetY = scale == 0 ? 0 : translationY / scale,
					RotationX = -rotationX,
					RotationY = -rotationY,
					RotationZ = -rotation
				};
			}
			*/
		}

		private static void UpdateScale(VisualElement view, NativeVisualElement control)
		{
			var scale = (float)view.Scale;
			control.RectTransform.localScale = new Vector3(scale, scale, 0.0f);
		}

		private static void UpdateVisibility(VisualElement view, NativeVisualElement control)
		{
			UpdateInputTransparent(view, control);
		}

		#endregion
	}
}
