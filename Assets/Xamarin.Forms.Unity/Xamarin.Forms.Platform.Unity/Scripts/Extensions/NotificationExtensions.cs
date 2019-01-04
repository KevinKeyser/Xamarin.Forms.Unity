using System.Collections.Specialized;

namespace Xamarin.Forms.Platform.Unity
{
	public static class NotificationExtensions
	{
		public static bool AddCollectionChangedEvent(this System.Collections.IEnumerable coll, NotifyCollectionChangedEventHandler handler)
		{
			if (coll is INotifyCollectionChanged cc)
			{
				cc.CollectionChanged += handler;
				return true;
			}
			return false;
		}

		public static bool RemoveCollectionChangedEvent(this System.Collections.IEnumerable coll, NotifyCollectionChangedEventHandler handler)
		{
			if (coll is INotifyCollectionChanged cc)
			{
				cc.CollectionChanged -= handler;
				return true;
			}
			return false;
		}
	}
}
