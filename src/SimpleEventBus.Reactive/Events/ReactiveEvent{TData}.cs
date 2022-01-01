using System.Reactive.Linq;
using System.Reactive.Subjects;
using HencoDesu.SimpleEventBus.Events;

namespace SimpleEventBus.Reactive.Events;

public class ReactiveEvent<TData> : ReactiveEvent, IEvent<TData>, IObservable<TData>
{
	private readonly Subject<TData> _subject = new();
	
	public void Raise(TData data)
	{
		_subject.OnNext(data);
		base.Raise();
	}

	public IDisposable Subscribe(Action<TData> handler)
	{
		return _subject.Subscribe(handler);
	}

	public IDisposable Subscribe(Func<TData, Task> handler)
	{
		return _subject.Select(data => Observable.FromAsync(async () => await handler(data)))
					   .Concat()
					   .Subscribe();
	}

	public IDisposable Subscribe(IObserver<TData> observer)
	{
		return _subject.Subscribe(observer);
	}
}