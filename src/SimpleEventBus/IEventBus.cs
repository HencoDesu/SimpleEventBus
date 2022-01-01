using HencoDesu.SimpleEventBus.Events;

namespace HencoDesu.SimpleEventBus;

/// <summary>
/// Represents a simple event bus pattern entry point
/// </summary>
public interface IEventBus
{
	/// <summary>
	/// Provides access to any <see cref="IEvent"/>
	/// </summary>
	/// <typeparam name="TEvent">Event type</typeparam>
	/// <returns>Instance of <see cref="TEvent"/></returns>
	TEvent GetEvent<TEvent>()
		where TEvent : IEvent, new();
}