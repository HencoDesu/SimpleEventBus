namespace HencoDesu.EventBus.Events;

public interface IEvent
{
	void Raise();
	IDisposable Subscribe(Action handler);
	IDisposable Subscribe(Func<Task> handler);
}