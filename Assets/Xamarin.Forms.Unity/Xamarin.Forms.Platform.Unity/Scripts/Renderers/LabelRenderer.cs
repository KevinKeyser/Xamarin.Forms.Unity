﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;
using UnityEngine;

namespace Xamarin.Forms.Platform.Unity
{
	public class LabelRenderer : ViewRenderer<Label, UnityEngine.UI.Text>
	{
		/*-----------------------------------------------------------------*/
		#region Field

		UnityEngine.Color _defaultTextColor;

		#endregion

		/*-----------------------------------------------------------------*/
		#region MonoBehavior

		protected override void Awake()
		{
			base.Awake();

			var label = Component;
			if (label != null)
			{
				//	Prefab の設定値がデフォルトカラー
				_defaultTextColor = label.color;
			}
		}

		#endregion

		/*-----------------------------------------------------------------*/
		#region Event Handler

		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				//_isInitiallyDefault = Element.IsDefault();

				UpdateText();
				UpdateColor();
				UpdateAlign();
				UpdateFont();
				UpdateLineBreakMode();
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == Label.TextProperty.PropertyName ||
				e.PropertyName == Label.FormattedTextProperty.PropertyName)
			{
				UpdateText();
			}
			else if (e.PropertyName == Label.TextColorProperty.PropertyName)
			{
				UpdateColor();
			}
			else if (e.PropertyName == Label.HorizontalTextAlignmentProperty.PropertyName ||
				e.PropertyName == Label.VerticalTextAlignmentProperty.PropertyName)
			{
				UpdateAlign();
			}
			else if (e.PropertyName == Label.FontSizeProperty.PropertyName ||
				e.PropertyName == Label.FontAttributesProperty.PropertyName)
			{
				UpdateFont();
			}
			else if (e.PropertyName == Label.LineBreakModeProperty.PropertyName)
			{
				UpdateLineBreakMode();
			}

			base.OnElementPropertyChanged(sender, e);
		}

		#endregion

		/*-----------------------------------------------------------------*/
		#region Internals

		void UpdateText()
		{
			//_perfectSizeValid = false;

			var nativeElement = Component;
			var label = Element;
			if (nativeElement == null || label == null)
			{
				return;
			}

			nativeElement.text = label.Text;
		}

		void UpdateColor()
		{
			var nativeElement = Component;
			var label = Element;
			if (nativeElement == null || label == null)
			{
				return;
			}

			if (label.TextColor != Color.Default)
			{
				nativeElement.color = label.TextColor.ToUnityColor();
			}
			else
			{
				nativeElement.color = _defaultTextColor;
			}
		}

		void UpdateFont()
		{
			var nativeElement = Component;
			var label = Element;
			if (nativeElement == null || label == null)
			{
				return;
			}

			nativeElement.fontSize = (int)label.FontSize;
			nativeElement.fontStyle = label.FontAttributes.ToUnityFontStyle();
		}

		void UpdateLineBreakMode()
		{
			var nativeElement = Component;
			var label = Element;
			if (nativeElement == null || label == null)
			{
				return;
			}

			nativeElement.horizontalOverflow =
				label.LineBreakMode == LineBreakMode.CharacterWrap || label.LineBreakMode == LineBreakMode.WordWrap ?
					HorizontalWrapMode.Wrap : HorizontalWrapMode.Overflow;
		}

		void UpdateAlign()
		{
			var nativeElement = Component;
			var label = Element;
			if (nativeElement == null || label == null)
			{
				return;
			}

			switch (label.HorizontalTextAlignment)
			{
				case TextAlignment.Start:
					switch (label.VerticalTextAlignment)
					{
						case TextAlignment.Start:
							nativeElement.alignment = TextAnchor.UpperLeft;
							break;

						case TextAlignment.Center:
							nativeElement.alignment = TextAnchor.MiddleLeft;
							break;

						case TextAlignment.End:
							nativeElement.alignment = TextAnchor.LowerLeft;
							break;
					}
					break;

				case TextAlignment.Center:
					switch (label.VerticalTextAlignment)
					{
						case TextAlignment.Start:
							nativeElement.alignment = TextAnchor.UpperCenter;
							break;

						case TextAlignment.Center:
							nativeElement.alignment = TextAnchor.MiddleCenter;
							break;

						case TextAlignment.End:
							nativeElement.alignment = TextAnchor.LowerCenter;
							break;
					}
					break;

				case TextAlignment.End:
					switch (label.VerticalTextAlignment)
					{
						case TextAlignment.Start:
							nativeElement.alignment = TextAnchor.UpperRight;
							break;

						case TextAlignment.Center:
							nativeElement.alignment = TextAnchor.MiddleRight;
							break;

						case TextAlignment.End:
							nativeElement.alignment = TextAnchor.LowerRight;
							break;
					}
					break;

			}
		}

		#endregion
	}
}