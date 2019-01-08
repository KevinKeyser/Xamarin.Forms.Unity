using System;

using UnityEngine;
using UnityEngine.UI;

namespace Xamarin.Forms.Platform.Unity
{
    public class NativeScrollViewElement : NativeVisualElement
    {
        private ScrollRect scrollRectComponent;

        public EventHandler<Vector2> OnScrollChanged;

        public RectTransform Content => scrollRectComponent.content;

        public bool Horizontal
        {
            get => scrollRectComponent.horizontal;
            set => scrollRectComponent.horizontal = value;
        }

        public bool Vertical
        {
            get => scrollRectComponent.vertical;
            set => scrollRectComponent.vertical = value;
        }


        public Vector2 ScrollValue
        {
            get => new Vector2(scrollRectComponent.horizontalScrollbar.value, scrollRectComponent.verticalScrollbar.value);
            set
            {
                scrollRectComponent.horizontalScrollbar.value = value.x;
                scrollRectComponent.verticalScrollbar.value = value.y;
            }
        }
        
        public override void BuildNativeRenderer()
        {
            base.BuildNativeRenderer();

            var self = gameObject;
            self.AddComponent<CanvasRenderer>();
            var backgroundImage = self.AddComponent<UnityEngine.UI.Image>();
            scrollRectComponent = self.AddComponent<ScrollRect>();

            #region Viewport
            var viewportImage = new GameObject("Viewport",
                                                       typeof(RectTransform),
                                                       typeof(CanvasRenderer),
                                                       typeof(Mask),
                                                       typeof(UnityEngine.UI.Image))
               .GetComponent<UnityEngine.UI.Image>();

            var viewportMask = viewportImage.GetComponent<Mask>();
            var viewportRectTransform = viewportImage.rectTransform;
            viewportRectTransform.SetParent(RectTransform, false);
            viewportRectTransform.anchorMin = Vector2.zero;
            viewportRectTransform.anchorMax = Vector2.one;
            viewportRectTransform.pivot = new Vector2(0, 1);
            // viewportRectTransform.sizeDelta = new Vector2(-17, -17);

            // Image Options
            backgroundImage.sprite = null;
            backgroundImage.color = UnityEngine.Color.white;
            backgroundImage.material = null;
            backgroundImage.raycastTarget = true;
            backgroundImage.type = UnityEngine.UI.Image.Type.Sliced;
            backgroundImage.fillCenter = true;
            
            // Mask Options
            viewportMask.showMaskGraphic = false;
            
            #region Content
            var contentRectTransform = new GameObject("Content", typeof(RectTransform))
               .GetComponent<RectTransform>();
            
            contentRectTransform.SetParent(viewportRectTransform, false);
            contentRectTransform.anchorMin = new Vector2(0, 1);
            contentRectTransform.anchorMax = new Vector2(0, 1);
            contentRectTransform.pivot = new Vector2(0, 1);
            contentRectTransform.sizeDelta = new Vector2(0, 300);
            #endregion Content
            #endregion Viewport

            #region Scrollbar Horizontal
            var scrollbarHorizontal = new GameObject("Scrollbar Horizontal",
                                                     typeof(RectTransform),
                                                     typeof(CanvasRenderer),
                                                     typeof(UnityEngine.UI.Image),
                                                     typeof(Scrollbar))
               .GetComponent<Scrollbar>();

            var scrollbarHorizontalImage = scrollbarHorizontal.GetComponent<UnityEngine.UI.Image>();

            var scrollbarHorizontalRectTransform = (RectTransform)scrollbarHorizontal.transform;
            scrollbarHorizontalRectTransform.SetParent(RectTransform, false);
            scrollbarHorizontalRectTransform.anchorMin = new Vector2(0, 0);
            scrollbarHorizontalRectTransform.anchorMax = new Vector2(1, 0);
            scrollbarHorizontalRectTransform.pivot = new Vector2(0, 0);
            scrollbarHorizontalRectTransform.sizeDelta = new Vector2(-17, 20);

            #region Sliding Area
            var horizontalSlidingRectTransform = new GameObject("Sliding Area",
                                                                typeof(RectTransform))
               .GetComponent<RectTransform>();

            horizontalSlidingRectTransform.SetParent(scrollbarHorizontalRectTransform, false);
            horizontalSlidingRectTransform.anchorMin = Vector2.zero;
            horizontalSlidingRectTransform.anchorMax = Vector2.one;
            horizontalSlidingRectTransform.offsetMin = new Vector2(10, 10);
            horizontalSlidingRectTransform.offsetMax = -new Vector2(10, 10);

            #region Handle
            var horizontalHandleImage = new GameObject("Handle",
                                                               typeof(RectTransform),
                                                               typeof(CanvasRenderer),
                                                               typeof(UnityEngine.UI.Image))
               .GetComponent<UnityEngine.UI.Image>();

            var horizontalHandleRectTransform = horizontalHandleImage.rectTransform;
            horizontalHandleRectTransform.SetParent(horizontalSlidingRectTransform, false);
            horizontalHandleRectTransform.anchorMin = Vector2.zero;
            horizontalHandleRectTransform.anchorMax = new Vector2(1, .2f);
            horizontalHandleRectTransform.offsetMin = new Vector2(-10, -10);
            horizontalHandleRectTransform.offsetMax = -new Vector2(-10, -10);

            // Options
            horizontalHandleImage.sprite = null;
            horizontalHandleImage.color = UnityEngine.Color.white;
            horizontalHandleImage.material = null;
            horizontalHandleImage.raycastTarget = true;
            horizontalHandleImage.type = UnityEngine.UI.Image.Type.Sliced;
            horizontalHandleImage.fillCenter = true;
            #endregion Handle
            #endregion Sliding Area

            // Scrollbar Options
            scrollbarHorizontal.interactable = true;
            scrollbarHorizontal.transition = Selectable.Transition.ColorTint;
            scrollbarHorizontal.targetGraphic = horizontalHandleImage;
            scrollbarHorizontal.colors = ColorBlock.defaultColorBlock;
            scrollbarHorizontal.navigation = Navigation.defaultNavigation;
            scrollbarHorizontal.handleRect = horizontalHandleRectTransform;
            scrollbarHorizontal.direction = Scrollbar.Direction.LeftToRight;
            scrollbarHorizontal.value = 0;
            scrollbarHorizontal.size = 1;
            scrollbarHorizontal.numberOfSteps = 0;
            scrollbarHorizontal.onValueChanged.AddListener(value => OnScrollChanged?.Invoke(this, ScrollValue));
            
            // Image Options
            scrollbarHorizontalImage.sprite = null;
            scrollbarHorizontalImage.color = UnityEngine.Color.white;
            scrollbarHorizontalImage.material = null;
            scrollbarHorizontalImage.raycastTarget = true;
            scrollbarHorizontalImage.type = UnityEngine.UI.Image.Type.Sliced;
            scrollbarHorizontalImage.fillCenter = true;
            #endregion Scrollbar Horizontal

            #region Scrollbar Vertical
            var scrollbarVertical = new GameObject("Scrollbar Vertical",
                                                   typeof(RectTransform),
                                                   typeof(CanvasRenderer),
                                                   typeof(UnityEngine.UI.Image),
                                                   typeof(Scrollbar))
               .GetComponent<Scrollbar>();

            var scrollbarVerticalImage = scrollbarVertical.GetComponent<UnityEngine.UI.Image>();
            
            var scrollbarVerticalRectTransform = (RectTransform)scrollbarVertical.transform;
            scrollbarVerticalRectTransform.SetParent(RectTransform, false);
            scrollbarVerticalRectTransform.anchorMin = new Vector2(1, 0);
            scrollbarVerticalRectTransform.anchorMax = new Vector2(1, 1);
            scrollbarVerticalRectTransform.pivot = Vector2.one;
            scrollbarVerticalRectTransform.sizeDelta = new Vector2(20, -17);

            #region Sliding Area
            var verticalSlidingRectTransform = new GameObject("Sliding Area",typeof(RectTransform))
               .GetComponent<RectTransform>();

            verticalSlidingRectTransform.SetParent(scrollbarVerticalRectTransform, false);
            verticalSlidingRectTransform.anchorMin = Vector2.zero;
            verticalSlidingRectTransform.anchorMax = Vector2.one;
            verticalSlidingRectTransform.offsetMin = new Vector2(10, 10);
            verticalSlidingRectTransform.offsetMax = -new Vector2(10, 10);

            #region Handle
            var verticalHandleImage = new GameObject("Handle",
                                                             typeof(RectTransform),
                                                             typeof(CanvasRenderer),
                                                             typeof(UnityEngine.UI.Image))
               .GetComponent<UnityEngine.UI.Image>();

            var verticalHandleRectTransform = verticalHandleImage.rectTransform;
            verticalHandleRectTransform.SetParent(verticalSlidingRectTransform, false);
            verticalHandleRectTransform.anchorMin = Vector2.zero;
            verticalHandleRectTransform.anchorMax = new Vector2(1, .2f);
            verticalHandleRectTransform.offsetMin = new Vector2(-10, -10);
            verticalHandleRectTransform.offsetMax = -new Vector2(-10, -10);

            // Options
            verticalHandleImage.sprite = null;
            verticalHandleImage.color = UnityEngine.Color.white;
            verticalHandleImage.material = null;
            verticalHandleImage.raycastTarget = true;
            verticalHandleImage.type = UnityEngine.UI.Image.Type.Sliced;
            verticalHandleImage.fillCenter = true;
            #endregion Handle
            #endregion Sliding Area

            // Scrollbar Options
            scrollbarVertical.interactable = true;
            scrollbarVertical.transition = Selectable.Transition.ColorTint;
            scrollbarVertical.targetGraphic = verticalHandleImage;
            scrollbarVertical.colors = ColorBlock.defaultColorBlock;
            scrollbarVertical.navigation = Navigation.defaultNavigation;
            scrollbarVertical.handleRect = verticalHandleRectTransform;
            scrollbarVertical.direction = Scrollbar.Direction.BottomToTop;
            scrollbarVertical.value = 0;
            scrollbarVertical.size = 1;
            scrollbarVertical.numberOfSteps = 0;
            scrollbarVertical.onValueChanged.AddListener(value => OnScrollChanged?.Invoke(this, ScrollValue));
            
            // Image Options
            scrollbarVerticalImage.sprite = null;
            scrollbarVerticalImage.color = UnityEngine.Color.white;
            scrollbarVerticalImage.material = null;
            scrollbarVerticalImage.raycastTarget = true;
            scrollbarVerticalImage.type = UnityEngine.UI.Image.Type.Sliced;
            scrollbarVerticalImage.fillCenter = true;
            
            #endregion Scrollbar Vertical

            // ScrollRect Options
            scrollRectComponent.content = contentRectTransform;
            scrollRectComponent.horizontal = true;
            scrollRectComponent.vertical = true;
            scrollRectComponent.movementType = ScrollRect.MovementType.Elastic;
            scrollRectComponent.inertia = true;
            scrollRectComponent.decelerationRate = .135f;
            scrollRectComponent.scrollSensitivity = 1;
            scrollRectComponent.viewport = viewportRectTransform;
            scrollRectComponent.horizontalScrollbar = scrollbarHorizontal;
            scrollRectComponent.horizontalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            scrollRectComponent.horizontalScrollbarSpacing = -3;
            scrollRectComponent.verticalScrollbar = scrollbarVertical;
            scrollRectComponent.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            scrollRectComponent.verticalScrollbarSpacing = -3;
            // templateScrollRect.onValueChanged

            // Image Options
            backgroundImage.sprite = null;
            backgroundImage.color = new UnityEngine.Color(1f, 1f, 1f, 0.3921569f);
            backgroundImage.material = null;
            backgroundImage.raycastTarget = true;
            backgroundImage.type = UnityEngine.UI.Image.Type.Sliced;
            backgroundImage.fillCenter = true;
        }
    }
}