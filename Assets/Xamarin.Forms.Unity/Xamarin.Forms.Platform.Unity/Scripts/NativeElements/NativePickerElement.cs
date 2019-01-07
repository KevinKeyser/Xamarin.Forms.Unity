using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Xamarin.Forms.Platform.Unity
{
    public class NativePickerElement : NativeVisualElement
    {
        private Dropdown dropdownComponent;
        private Text textComponent;
        private Text itemTextComponent;

        public EventHandler<int> OnSelectionChanged;

        public List<Dropdown.OptionData> Options
        {
            get => dropdownComponent.options;
            set => dropdownComponent.options = value;
        }

        public string CaptionText
        {
            get => textComponent.text;
            set => textComponent.text = value;
        }

        public FontStyle FontStyle
        {
            get => textComponent.fontStyle;
            set => textComponent.fontStyle = value;
        }

        public UnityEngine.Color Foreground
        {
            get => textComponent.color;
            set
            {
                textComponent.color = value;
                itemTextComponent.color = value;
            }
        }

        public int SelectedIndex
        {
            get => dropdownComponent.value;
            set => dropdownComponent.value = value;
        }

        public UnityEngine.Font Font
        {
            get => textComponent.font;
            set
            {
                textComponent.font = value;
                itemTextComponent.font = value;
            }
        }

        public int FontSize
        {
            get => textComponent.fontSize;
            set
            {
                textComponent.fontSize = value;
                itemTextComponent.fontSize = value;
            }
        }

        public override void BuildNativeRenderer()
        {
            base.BuildNativeRenderer();
            var self = gameObject;
            self.AddComponent<CanvasRenderer>();
            var dropdownBackgroundImage = self.AddComponent<UnityEngine.UI.Image>();
            dropdownComponent = self.AddComponent<Dropdown>();

            #region Label
            textComponent = new GameObject("Label",
                                           typeof(RectTransform),
                                           typeof(CanvasRenderer),
                                           typeof(Text))
               .GetComponent<Text>();

            var textRectTransform = textComponent.rectTransform;

            textRectTransform.SetParent(self.transform, false);
            textRectTransform.anchorMin = Vector2.zero;
            textRectTransform.anchorMax = Vector2.one;
            textRectTransform.offsetMin = new Vector2(10, 6);
            textRectTransform.offsetMax = -new Vector2(25, 7);

            // Options
            textComponent.text = "Option A";
            textComponent.fontStyle = FontStyle.Normal;
            textComponent.fontSize = 14;
            textComponent.font = new UnityEngine.Font();
            textComponent.lineSpacing = 1;
            textComponent.supportRichText = true;
            textComponent.alignment = TextAnchor.MiddleLeft;
            textComponent.alignByGeometry = false;
            textComponent.horizontalOverflow = HorizontalWrapMode.Overflow;
            textComponent.verticalOverflow = VerticalWrapMode.Truncate;
            textComponent.resizeTextForBestFit = false;
            textComponent.color = new UnityEngine.Color(0.1960784f, 0.1960784f, 0.1960784f);
            textComponent.material = null;
            textComponent.raycastTarget = true;
            #endregion

            #region Arrow
            var arrowImageComponent = new GameObject("Arrow",
                                                     typeof(RectTransform),
                                                     typeof(CanvasRenderer),
                                                     typeof(UnityEngine.UI.Image))
               .GetComponent<UnityEngine.UI.Image>();

            var arrowRectTransform = arrowImageComponent.rectTransform;
            arrowRectTransform.SetParent(self.transform, false);
            arrowRectTransform.anchorMin = new Vector2(1, .5f);
            arrowRectTransform.anchorMax = new Vector2(1, .5f);
            arrowRectTransform.anchoredPosition = new Vector2(-15, 0);
            arrowRectTransform.sizeDelta = new Vector2(20, 20);

            // Options
            arrowImageComponent.sprite = null;
            arrowImageComponent.color = UnityEngine.Color.white;
            arrowImageComponent.material = null;
            arrowImageComponent.raycastTarget = true;
            arrowImageComponent.type = UnityEngine.UI.Image.Type.Simple;
            arrowImageComponent.useSpriteMesh = false;
            arrowImageComponent.preserveAspect = false;
            #endregion

            #region Template
            var templateScrollRect = new GameObject("Template",
                                                    typeof(RectTransform),
                                                    typeof(CanvasRenderer),
                                                    typeof(Image),
                                                    typeof(ScrollRect))
               .GetComponent<ScrollRect>();

            var templateRectTransform = (RectTransform)templateScrollRect.transform;

            templateRectTransform.SetParent(self.transform, false);
            templateRectTransform.anchorMin = Vector2.zero;
            templateRectTransform.anchorMax = new Vector2(1, 0);
            templateRectTransform.pivot = new Vector2(.5f, 1);
            templateRectTransform.anchoredPosition = new Vector2(0, 2);
            templateRectTransform.sizeDelta = new Vector2(0, 150);

            #region Viewport
            var viewportImage = new GameObject("Viewport",
                                               typeof(RectTransform),
                                               typeof(CanvasRenderer),
                                               typeof(Mask),
                                               typeof(UnityEngine.UI.Image))
               .GetComponent<UnityEngine.UI.Image>();

            var viewportMask = viewportImage.GetComponent<Mask>();
            var viewportRectTransform = viewportImage.rectTransform;
            viewportRectTransform.SetParent(templateRectTransform, false);
            viewportRectTransform.anchorMin = Vector2.zero;
            viewportRectTransform.anchorMax = Vector2.one;
            viewportRectTransform.pivot = new Vector2(0, 1);
            viewportRectTransform.offsetMin = Vector2.zero;
            viewportRectTransform.offsetMax = -new Vector2(18, 0);

            // Image Options
            viewportImage.sprite = null;
            viewportImage.color = UnityEngine.Color.white;
            viewportImage.material = null;
            viewportImage.raycastTarget = true;
            viewportImage.type = UnityEngine.UI.Image.Type.Sliced;
            viewportImage.fillCenter = true;

            // Mask Options
            viewportMask.showMaskGraphic = false;

            #region Content
            var contentRectTransform = new GameObject("Content", typeof(RectTransform))
               .GetComponent<RectTransform>();

            contentRectTransform.SetParent(viewportRectTransform, false);
            contentRectTransform.anchorMin = new Vector2(0, 1);
            contentRectTransform.anchorMax = new Vector2(1, 1);
            contentRectTransform.pivot = new Vector2(.5f, 1);
            contentRectTransform.sizeDelta = new Vector2(0, 28);

            #region Item
            var itemToggle = new GameObject("Item",
                                            typeof(RectTransform),
                                            typeof(Toggle))
               .GetComponent<Toggle>();

            var itemRectTransform = (RectTransform)itemToggle.transform;
            itemRectTransform.SetParent(contentRectTransform, false);
            itemRectTransform.anchorMin = new Vector2(0, .5f);
            itemRectTransform.anchorMax = new Vector2(1, .5f);
            itemRectTransform.sizeDelta = new Vector2(0, 20);

            #region Item Background
            var itemBackgroundImage = new GameObject("Item Background",
                                                     typeof(RectTransform),
                                                     typeof(CanvasRenderer),
                                                     typeof(UnityEngine.UI.Image))
               .GetComponent<UnityEngine.UI.Image>();

            var itemBackgroundRectTransform = itemBackgroundImage.rectTransform;
            itemBackgroundRectTransform.SetParent(itemRectTransform, false);
            itemBackgroundRectTransform.anchorMin = Vector2.zero;
            itemBackgroundRectTransform.anchorMax = Vector2.one;
            itemBackgroundRectTransform.offsetMin = Vector2.zero;
            itemBackgroundRectTransform.offsetMax = Vector2.zero;

            // Options
            itemBackgroundImage.sprite = null;
            itemBackgroundImage.color = UnityEngine.Color.white;
            itemBackgroundImage.material = null;
            itemBackgroundImage.raycastTarget = true;
            itemBackgroundImage.type = UnityEngine.UI.Image.Type.Simple;
            itemBackgroundImage.useSpriteMesh = false;
            itemBackgroundImage.preserveAspect = false;
            #endregion Item Background

            #region Item Checkmark
            var itemCheckmarkImage = new GameObject("Item Checkmark",
                                                    typeof(RectTransform),
                                                    typeof(CanvasRenderer),
                                                    typeof(UnityEngine.UI.Image))
               .GetComponent<UnityEngine.UI.Image>();

            var itemCheckmarkRectTransform = itemCheckmarkImage.rectTransform;
            itemCheckmarkRectTransform.SetParent(itemRectTransform, false);
            itemCheckmarkRectTransform.anchorMin = new Vector2(0, .5f);
            itemCheckmarkRectTransform.anchorMax = new Vector2(0, .5f);
            itemCheckmarkRectTransform.anchoredPosition = new Vector2(10, 0);
            itemCheckmarkRectTransform.sizeDelta = new Vector2(20, 20);

            // Options
            itemCheckmarkImage.sprite = null;
            itemCheckmarkImage.color = UnityEngine.Color.black;
            itemCheckmarkImage.material = null;
            itemCheckmarkImage.raycastTarget = true;
            itemCheckmarkImage.type = UnityEngine.UI.Image.Type.Simple;
            itemCheckmarkImage.useSpriteMesh = false;
            itemCheckmarkImage.preserveAspect = false;
            #endregion Item Checkmark

            #region Item Label
            itemTextComponent = new GameObject("Item Label",
                                               typeof(RectTransform),
                                               typeof(CanvasRenderer),
                                               typeof(Text))
               .GetComponent<Text>();

            var itemLabelRectTransform = itemTextComponent.rectTransform;
            itemLabelRectTransform.SetParent(itemRectTransform, false);
            itemLabelRectTransform.anchorMin = Vector2.zero;
            itemLabelRectTransform.anchorMax = Vector2.one;
            itemLabelRectTransform.offsetMin = new Vector2(20, 1);
            itemLabelRectTransform.offsetMax = -new Vector2(10, 2);

            // Options
            textComponent.text = "Option A";
            textComponent.font = new UnityEngine.Font();
            textComponent.fontStyle = FontStyle.Normal;
            textComponent.fontSize = 14;
            textComponent.lineSpacing = 1;
            textComponent.supportRichText = true;
            textComponent.alignment = TextAnchor.MiddleLeft;
            textComponent.alignByGeometry = false;
            textComponent.horizontalOverflow = HorizontalWrapMode.Overflow;
            textComponent.verticalOverflow = VerticalWrapMode.Overflow;
            textComponent.resizeTextForBestFit = false;
            textComponent.color = new UnityEngine.Color(0.1960784f, 0.1960784f, 0.1960784f);
            textComponent.material = null;
            textComponent.raycastTarget = true;
            #endregion Item Label

            // Options
            itemToggle.interactable = true;
            itemToggle.transition = Selectable.Transition.ColorTint;
            itemToggle.targetGraphic = itemBackgroundImage;
            itemToggle.colors = ColorBlock.defaultColorBlock;
            itemToggle.navigation = Navigation.defaultNavigation;
            itemToggle.isOn = true;
            itemToggle.toggleTransition = Toggle.ToggleTransition.Fade;
            itemToggle.graphic = itemCheckmarkImage;
            itemToggle.group = null;

            // itemToggle.onValueChanged
            #endregion Item
            #endregion Content
            #endregion Viewport

            #region Scrollbar
            var scrollbar = new GameObject("Scrollbar",
                                           typeof(RectTransform),
                                           typeof(CanvasRenderer),
                                           typeof(UnityEngine.UI.Image),
                                           typeof(Scrollbar))
               .GetComponent<Scrollbar>();

            var scrollbarImage = scrollbar.GetComponent<UnityEngine.UI.Image>();

            var scrollbarRectTransform = (RectTransform)scrollbar.transform;
            scrollbarRectTransform.SetParent(templateRectTransform, false);
            scrollbarRectTransform.anchorMin = new Vector2(1, 0);
            scrollbarRectTransform.anchorMax = Vector2.one;
            scrollbarRectTransform.pivot = Vector2.one;
            scrollbarRectTransform.sizeDelta = new Vector2(20, 0);

            #region Sliding Area
            var slidingRectTransform = new GameObject("Sliding Area",
                                                      typeof(RectTransform))
               .GetComponent<RectTransform>();

            slidingRectTransform.SetParent(scrollbarRectTransform, false);
            slidingRectTransform.anchorMin = Vector2.zero;
            slidingRectTransform.anchorMax = Vector2.one;
            slidingRectTransform.offsetMin = new Vector2(10, 10);
            slidingRectTransform.offsetMax = -new Vector2(10, 10);

            #region Handle
            var handleImage = new GameObject("Handle",
                                             typeof(RectTransform),
                                             typeof(CanvasRenderer),
                                             typeof(UnityEngine.UI.Image))
               .GetComponent<UnityEngine.UI.Image>();

            var handleRectTransform = handleImage.rectTransform;
            handleRectTransform.SetParent(slidingRectTransform, false);
            handleRectTransform.anchorMin = Vector2.zero;
            handleRectTransform.anchorMax = new Vector2(1, .2f);
            handleRectTransform.offsetMin = new Vector2(-10, -10);
            handleRectTransform.offsetMax = -new Vector2(-10, -10);

            // Options
            handleImage.sprite = null;
            handleImage.color = UnityEngine.Color.white;
            handleImage.material = null;
            handleImage.raycastTarget = true;
            handleImage.type = UnityEngine.UI.Image.Type.Sliced;
            handleImage.fillCenter = true;
            #endregion Handle
            #endregion Sliding Area

            // Scrollbar Options
            scrollbar.interactable = true;
            scrollbar.transition = Selectable.Transition.ColorTint;
            scrollbar.targetGraphic = handleImage;
            scrollbar.colors = ColorBlock.defaultColorBlock;
            scrollbar.navigation = Navigation.defaultNavigation;
            scrollbar.handleRect = handleRectTransform;
            scrollbar.direction = Scrollbar.Direction.BottomToTop;
            scrollbar.value = 0;
            scrollbar.size = 1;
            scrollbar.numberOfSteps = 0;

            // Image Options
            scrollbarImage.sprite = null;
            scrollbarImage.color = UnityEngine.Color.white;
            scrollbarImage.material = null;
            scrollbarImage.raycastTarget = true;
            scrollbarImage.type = UnityEngine.UI.Image.Type.Sliced;
            scrollbarImage.fillCenter = true;
            #endregion Scrollbar

            // ScrollRect Options
            templateScrollRect.content = contentRectTransform;
            templateScrollRect.horizontal = false;
            templateScrollRect.vertical = true;
            templateScrollRect.movementType = ScrollRect.MovementType.Clamped;
            templateScrollRect.inertia = true;
            templateScrollRect.decelerationRate = .135f;
            templateScrollRect.scrollSensitivity = 1;
            templateScrollRect.viewport = viewportRectTransform;
            templateScrollRect.horizontalScrollbar = null;
            templateScrollRect.horizontalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            templateScrollRect.horizontalScrollbarSpacing = -3;
            templateScrollRect.verticalScrollbar = scrollbar;
            templateScrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            templateScrollRect.verticalScrollbarSpacing = -3;
            // templateScrollRect.onValueChanged

            // Image Options
            scrollbarImage.sprite = null;
            scrollbarImage.color = UnityEngine.Color.white;
            scrollbarImage.material = null;
            scrollbarImage.raycastTarget = true;
            scrollbarImage.type = UnityEngine.UI.Image.Type.Sliced;
            scrollbarImage.fillCenter = true;

            // Disable
            templateRectTransform.gameObject.SetActive(false);
            #endregion Template

            // Image Options
            dropdownBackgroundImage.sprite = null;
            dropdownBackgroundImage.color = UnityEngine.Color.white;
            dropdownBackgroundImage.material = null;
            dropdownBackgroundImage.raycastTarget = true;
            dropdownBackgroundImage.type = UnityEngine.UI.Image.Type.Sliced;
            dropdownBackgroundImage.fillCenter = true;

            // Dropdown Options
            dropdownComponent.interactable = true;
            dropdownComponent.transition = Selectable.Transition.ColorTint;
            dropdownComponent.targetGraphic = dropdownBackgroundImage;
            dropdownComponent.colors = ColorBlock.defaultColorBlock;
            dropdownComponent.navigation = Navigation.defaultNavigation;
            dropdownComponent.template = templateRectTransform;
            dropdownComponent.captionText = textComponent;
            dropdownComponent.captionImage = null;
            dropdownComponent.itemText = itemTextComponent;
            dropdownComponent.itemImage = null;
            dropdownComponent.value = 0;
            dropdownComponent.options = new List<Dropdown.OptionData>()
            {
                new Dropdown.OptionData("Option A"),
                new Dropdown.OptionData("Option B"),
                new Dropdown.OptionData("Option C")
            };
            dropdownComponent.onValueChanged.AddListener(value => OnSelectionChanged?.Invoke(this, value));
        }
    }
}