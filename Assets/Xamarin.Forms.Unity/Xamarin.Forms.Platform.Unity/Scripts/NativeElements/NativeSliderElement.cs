using System;
using System.Security.Cryptography.X509Certificates;

using UnityEngine;
using UnityEngine.UI;

namespace Xamarin.Forms.Platform.Unity
{
    public class NativeSliderElement : NativeVisualElement
    {
        private UnityEngine.UI.Slider sliderComponent;
        private UnityEngine.UI.Image handleImageComponent;

        private float lastMinimum;
        private float lastMaximum;

        public EventHandler<float> OnValueChanged;
        public EventHandler<float> OnMinimumChanged;
        public EventHandler<float> OnMaximumChanged;

        public float Minimum
        {
            get => sliderComponent.minValue;
            set => sliderComponent.minValue = value;
        }

        public float Maximum
        {
            get => sliderComponent.maxValue;
            set => sliderComponent.maxValue = value;
        }
        
        public float Value
        {
            get => sliderComponent.value;
            set => sliderComponent.value = value;
        }

        public UnityEngine.Color HandleColor
        {
            get => handleImageComponent.color;
            set => handleImageComponent.color = value;
        }
        
        public override void BuildNativeRenderer()
        {
            base.BuildNativeRenderer();
            var self = gameObject;
            sliderComponent = self.AddComponent<UnityEngine.UI.Slider>();
            
            #region Background
            var backgroundImage  = new GameObject("Background",
                                                 typeof(RectTransform),
                                                 typeof(CanvasRenderer),
                                                 typeof(UnityEngine.UI.Image))
               .GetComponent<UnityEngine.UI.Image>();

            var backgroundRectTransform = backgroundImage.rectTransform;
            backgroundRectTransform.SetParent(RectTransform, false);
            backgroundRectTransform.anchorMin = new Vector2(0, .25f);
            backgroundRectTransform.anchorMax = new Vector2(1, .75f);
            backgroundRectTransform.offsetMin = Vector2.zero;
            backgroundRectTransform.offsetMax = Vector2.zero;
            
            // Options
            backgroundImage.sprite = null;
            backgroundImage.color = UnityEngine.Color.white;
            backgroundImage.material = null;
            backgroundImage.raycastTarget = true;
            backgroundImage.type = UnityEngine.UI.Image.Type.Sliced;
            backgroundImage.fillCenter = true;
            #endregion Background
            
            #region Fill Area
            var fillAreaRectTransform = new GameObject("Fill Area",
                                                       typeof(RectTransform))
               .GetComponent<RectTransform>();

            fillAreaRectTransform.SetParent(RectTransform, false);
            fillAreaRectTransform.anchorMin = new Vector2(0, .25f);
            fillAreaRectTransform.anchorMax = new Vector2(1, .75f);
            fillAreaRectTransform.offsetMin = new Vector2(5, 0);
            fillAreaRectTransform.offsetMax = -new Vector2(15, 0);

            #region Fill
            var fillImage = new GameObject("Fill",
                                                   typeof(RectTransform),
                                                   typeof(CanvasRenderer),
                                                   typeof(UnityEngine.UI.Image))
               .GetComponent<UnityEngine.UI.Image>();

            var fillRectTransform = fillImage.rectTransform;
            fillRectTransform.SetParent(fillAreaRectTransform, false);
            fillRectTransform.anchorMin = Vector2.zero;
            fillRectTransform.anchorMax = new Vector2(0, 1);
            fillRectTransform.sizeDelta = new Vector2(10, 0);

            // Options
            fillImage.sprite = null;
            fillImage.color = UnityEngine.Color.white;
            fillImage.material = null;
            fillImage.raycastTarget = true;
            fillImage.type = UnityEngine.UI.Image.Type.Sliced;
            fillImage.fillCenter = true;
            #endregion Fill

            #endregion Fill Area

            #region Handle Slide Area
            var handleSlideAreaRectTransform = new GameObject("Handle Slide Area",
                                                              typeof(RectTransform))
               .GetComponent<RectTransform>();

            handleSlideAreaRectTransform.SetParent(RectTransform, false);
            handleSlideAreaRectTransform.anchorMin = Vector2.zero;
            handleSlideAreaRectTransform.anchorMax = Vector2.one;
            handleSlideAreaRectTransform.offsetMin = new Vector2(10, 0);
            handleSlideAreaRectTransform.offsetMax = -new Vector2(10, 0);
            
            #region Handle
            handleImageComponent = new GameObject("Handle",
                                                     typeof(RectTransform),
                                                     typeof(CanvasRenderer),
                                                     typeof(UnityEngine.UI.Image))
               .GetComponent<UnityEngine.UI.Image>();

            var handleRectTransform = handleImageComponent.rectTransform;
            handleRectTransform.SetParent(handleSlideAreaRectTransform, false);
            handleRectTransform.anchorMin = Vector2.zero;
            handleRectTransform.anchorMax = new Vector2(0, 1);
            handleRectTransform.sizeDelta = new Vector2(20, 0);

            // Options
            handleImageComponent.sprite = null;
            handleImageComponent.color = UnityEngine.Color.white;
            handleImageComponent.material = null;
            handleImageComponent.raycastTarget = true;
            handleImageComponent.type = UnityEngine.UI.Image.Type.Simple;
            handleImageComponent.useSpriteMesh = false;
            handleImageComponent.preserveAspect = false;
            #endregion Handle

            #endregion Handle Slide Area

            // Options
            sliderComponent.interactable = true;
            sliderComponent.transition = Selectable.Transition.ColorTint;
            sliderComponent.targetGraphic = handleImageComponent;
            sliderComponent.colors = ColorBlock.defaultColorBlock;
            sliderComponent.navigation = Navigation.defaultNavigation;
            sliderComponent.fillRect = fillRectTransform;
            sliderComponent.handleRect = handleRectTransform;
            sliderComponent.direction = UnityEngine.UI.Slider.Direction.LeftToRight;
            sliderComponent.minValue = 0;
            sliderComponent.maxValue = 1;
            sliderComponent.wholeNumbers = false;
            sliderComponent.value = 0;
            sliderComponent.onValueChanged.AddListener(value => OnValueChanged?.Invoke(this, value));
            
            // Set last values for event purposes
            lastMinimum = Minimum;
            lastMaximum = Maximum;
        }

        public virtual void LateUpdate()
        {
            if(!Equals(lastMinimum, Minimum))
            {
                OnMinimumChanged?.Invoke(this, Minimum);   
            }
            if(!Equals(lastMaximum, Maximum))
            {
                OnMaximumChanged?.Invoke(this, Maximum);   
            }
            lastMinimum = Minimum;
            lastMaximum = Maximum;
        }
    }
}