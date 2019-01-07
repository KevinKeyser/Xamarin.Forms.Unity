using System;
using System.Linq;

namespace Xamarin.Forms.Platform.Unity
{
	public static class VisualElementExtensions
	{
		public static IVisualElementRenderer GetOrCreateRenderer(this VisualElement self)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			var renderer = Platform.GetRenderer(self);

			if (renderer != null)
			{
				return renderer;
			}

			renderer = Platform.CreateRenderer(self);
			Platform.SetRenderer(self, renderer);

			return renderer;
		}

		internal static void Cleanup(this VisualElement self)
		{
			if (self == null)
			{
				throw new ArgumentNullException(nameof(self));
			}

			var renderer = Platform.GetRenderer(self);

			foreach (var visual in self.Descendants().OfType<VisualElement>())
			{
				var childRenderer = Platform.GetRenderer(visual);

				if (childRenderer == null)
				{
					continue;
				}

				childRenderer.Dispose();
				Platform.SetRenderer(visual, null);
			}

			if (renderer == null)
			{
				return;
			}

			renderer.Dispose();
			Platform.SetRenderer(self, null);
		}
	}
}