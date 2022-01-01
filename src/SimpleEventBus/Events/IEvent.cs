namespace HencoDesu.SimpleEventBus.Events;

/// <summary>
///     Represents an simple event that can be happen anywhere
/// </summary>
public interface IEvent
{
	/// <summary>
	///     Fires all registered handlers to be executed
	/// </summary>
	void Raise();

	/// <summary>
	///     Subscribes a handler to be executed when <see cref="Raise" /> will be called somewhere
	/// </summary>
	/// <param name="handler">Handler for that event</param>
	/// <returns>
	///     <see cref="IDisposable" /> that will unsubscribe handler when <see cref="IDisposable.Dispose" /> will be
	///     called
	/// </returns>
	IDisposable Subscribe(Action handler);

	/// <inheritdoc cref="Subscribe(System.Action)" />
	IDisposable Subscribe(Func<Task> handler);
}