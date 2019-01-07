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
    public class NativeVisualElement : MonoBehaviour
    {
        RectTransform rectTransform;
        CanvasGroup canvasGroup;


        public RectTransform RectTransform => rectTransform;

        public CanvasGroup CanvasGroup
        {
            get
            {
                if (canvasGroup == null)
                {
                    canvasGroup = gameObject.AddComponent<CanvasGroup>();
                }
                return canvasGroup;
            }
        }
        
        public double Opacity
        {
            get
            {
                return canvasGroup != null ? canvasGroup.alpha : 1.0;
            }

            set
            {
                if (canvasGroup == null && value >= 1.0)
                {
                    return;
                }

                CanvasGroup.alpha = (float)value;
            }
        }

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            if (rectTransform == null)
            {
                rectTransform = gameObject.AddComponent<RectTransform>();
            }

            canvasGroup = gameObject.GetComponent<CanvasGroup>();
        }

        public virtual void BuildNativeRenderer()
        {
            
        }
    }
}
