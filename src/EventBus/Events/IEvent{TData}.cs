namespace HencoDesu.EventBus.Events;

public interface IEvent<TData> : IEvent
{
	void Raise(TData data);
	IDisposable Subscribe(Action<TData> handler);
	IDisposable Subscribe(Func<TData, Task> handler);
}