using HencoDesu.SimpleEventBus.Events;

namespace HencoDesu.SimpleEventBus;

public class EventBus : IEventBus
{
	private readonly List<IEvent> _events = new();

	public TEvent GetEvent<TEvent>()
		where TEvent : IEvent, new()
	{
		var @event = _events.OfType<TEvent>().SingleOrDefault();

		if (@event is null)
		{
			@event = new TEvent();
			_events.Add(@event);
		}

		return @event;
	}
}