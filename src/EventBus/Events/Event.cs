namespace HencoDesu.EventBus.Events;

public class Event : IEvent
{
	private Action? _handler;
	
	public void Raise()
	{
		_handler?.Invoke();
	}

	public IDisposable Subscribe(Action handler)
	{
		_handler += handler;
		return new EventSubscription(() => _handler -= handler);
	}

	public IDisposable Subscribe(Func<Task> handler)
	{
		return Subscribe(() => Task.Run(handler).ConfigureAwait(false));
	}
}