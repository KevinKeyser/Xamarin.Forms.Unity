using System;

using UnityEngine;
using UnityEngine.UI;

namespace Xamarin.Forms.Platform.Unity
{
    public class NativeSwitchElement : NativeVisualElement
    {
        private Toggle toggleComponent;
        private UnityEngine.UI.Image checkmarkImageComponent;

        public EventHandler<bool> OnToggled;
        
        public bool IsToggled
        {
            get => toggleComponent.isOn;
            set => toggleComponent.isOn = value;
        }

        public UnityEngine.Color OnColor
        {
            get => checkmarkImageComponent.color;
            set => checkmarkImageComponent.color = value;
        }
        
        public override void BuildNativeRenderer()
        {
            base.BuildNativeRenderer();
            var self = gameObject;
            var backgroundImage = self.AddComponent<UnityEngine.UI.Image>();
            toggleComponent = self.AddComponent<Toggle>();

            #region Checkmark
            checkmarkImageComponent = new GameObject("Checkmark",
                                                 typeof(RectTransform),
                                                 typeof(CanvasRenderer),
                                                 typeof(UnityEngine.UI.Image))
               .GetComponent<UnityEngine.UI.Image>();

            var checkmarkRectTransform = checkmarkImageComponent.rectTransform;
            checkmarkRectTransform.SetParent(self.transform, false);
            checkmarkRectTransform.anchorMin = Vector2.zero;
            checkmarkRectTransform.anchorMax = Vector2.one;
            checkmarkRectTransform.offsetMin = new Vector2(2, 2);
            checkmarkRectTransform.offsetMax = -new Vector2(2, 2);
            
            // Options
            checkmarkImageComponent.sprite = null;
            checkmarkImageComponent.color = UnityEngine.Color.black;
            checkmarkImageComponent.material = null;
            checkmarkImageComponent.raycastTarget = true;
            checkmarkImageComponent.type = UnityEngine.UI.Image.Type.Simple;
            checkmarkImageComponent.useSpriteMesh = false;
            checkmarkImageComponent.preserveAspect = false;
            #endregion

            // Toggle Options
            toggleComponent.interactable = true;
            toggleComponent.transition = Selectable.Transition.ColorTint;
            toggleComponent.targetGraphic = backgroundImage;
            toggleComponent.colors = ColorBlock.defaultColorBlock;
            toggleComponent.navigation = Navigation.defaultNavigation;
            toggleComponent.isOn = true;
            toggleComponent.toggleTransition = Toggle.ToggleTransition.Fade;
            toggleComponent.graphic = checkmarkImageComponent;
            toggleComponent.group = null;
            toggleComponent.onValueChanged.AddListener(value => OnToggled?.Invoke(this, value));
            
            // Image Options
            backgroundImage.sprite = null;
            backgroundImage.color = UnityEngine.Color.white;
            backgroundImage.material = null;
            backgroundImage.raycastTarget = true;
            backgroundImage.type = UnityEngine.UI.Image.Type.Sliced;
            backgroundImage.fillCenter = true;
        }
    }
}