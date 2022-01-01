namespace HencoDesu.SimpleEventBus.Events;

internal class EventSubscription : IDisposable
{
	private readonly Action _unsubscribeAction;
	private bool _disposed;

	public EventSubscription(Action unsubscribeAction)
	{
		_unsubscribeAction = unsubscribeAction;
	}

	public void Dispose()
	{
		if (!_disposed)
		{
			_unsubscribeAction();
			_disposed = true;
		}
	}
}