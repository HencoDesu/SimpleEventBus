using HencoDesu.EventBus.Events;

namespace HencoDesu.EventBus.Extensions;

public static class EventExtensions
{
	public static void RaiseFromEventPattern(
		this IEvent @event, 
		Action<EventHandler> subscribeAction)
	{
		subscribeAction((_, _) => @event.Raise());
	}

	public static void RaiseFromEventPattern<TData>(
		this IEvent<TData> @event,
		Action<EventHandler<TData>> subscribeAction)
	{
		subscribeAction((_, data) => @event.Raise(data));
	}
}