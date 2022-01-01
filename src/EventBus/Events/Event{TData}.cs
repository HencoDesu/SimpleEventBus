namespace HencoDesu.EventBus.Events;

public class Event<TData> : Event, IEvent<TData>
{
	private Action<TData>? _handler;
	
	public void Raise(TData data)
	{
		base.Raise();
		_handler?.Invoke(data);
	}
	
	public IDisposable Subscribe(Action<TData> handler)
	{
		_handler += handler;
		return new EventSubscription(() => _handler -= handler);
	}

	public IDisposable Subscribe(Func<TData, Task> handler)
	{
		return Subscribe(data => Task.Run(() => handler(data)).ConfigureAwait(false));
	}
}