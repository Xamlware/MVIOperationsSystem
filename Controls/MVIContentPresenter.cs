using System.Windows;
using System.Windows.Controls;

namespace MVIOperationsSystem.Controls
{
	public class MVIContentPresenter : ContentPresenter
	{
		#region RE: ContentChanged
		public static RoutedEvent ContentChangedEvent = EventManager.RegisterRoutedEvent("ContentChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MVIContentPresenter));
		public event RoutedEventHandler ContentChanged
		{
			add { AddHandler(ContentChangedEvent, value); }
			remove { RemoveHandler(ContentChangedEvent, value); }
		}
		public static void AddContentChangedHandler(UIElement el, RoutedEventHandler handler)
		{
			el.AddHandler(ContentChangedEvent, handler);
		}
		public static void RemoveContentChangedHandler(UIElement el, RoutedEventHandler handler)
		{
			el.RemoveHandler(ContentChangedEvent, handler);
		}
		#endregion

		protected override void OnVisualChildrenChanged(System.Windows.DependencyObject visualAdded, System.Windows.DependencyObject visualRemoved)
		{
			base.OnVisualChildrenChanged(visualAdded, visualRemoved);
			RaiseEvent(new RoutedEventArgs(ContentChangedEvent, this));
		}
	}
}