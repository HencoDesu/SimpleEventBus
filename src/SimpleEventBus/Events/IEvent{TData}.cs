namespace HencoDesu.SimpleEventBus.Events;

/// <summary>
///     Represents an event that can be happen anywhere and contains some data
/// </summary>
/// <typeparam name="TData">Event data type</typeparam>
public interface IEvent<TData> : IEvent
{
	/// <summary>
	///     Fires all registered handlers to be executed
	/// </summary>
	/// <remarks>
	///     Handlers that not accept data will be executed too
	/// </remarks>
	void Raise(TData data);

	/// <summary>
	///     Subscribes a handler to be executed when <see cref="Raise" /> will be called somewhere
	/// </summary>
	/// <param name="handler">Handler for that event</param>
	/// <returns>
	///     <see cref="IDisposable" /> that will unsubscribe handler when <see cref="IDisposable.Dispose" /> will be
	///     called
	/// </returns>
	IDisposable Subscribe(Action<TData> handler);

	/// <inheritdoc cref="Subscribe(System.Action{TData})" />
	IDisposable Subscribe(Func<TData, Task> handler);
}