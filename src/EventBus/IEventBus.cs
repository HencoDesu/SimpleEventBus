using HencoDesu.EventBus.Events;

namespace HencoDesu.EventBus;

public interface IEventBus
{
	TEvent GetEvent<TEvent>() 
		where TEvent : IEvent, new();
}