using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using HencoDesu.SimpleEventBus.Events;

namespace SimpleEventBus.Reactive.Events;

public class ReactiveEvent : IEvent, IObservable<Unit>
{
	private readonly Subject<Unit> _subject = new();

	public void Raise()
	{
		_subject.OnNext(Unit.Default);
	}

	public IDisposable Subscribe(Action handler)
	{
		return _subject.Subscribe(_ => handler());
	}

	public IDisposable Subscribe(Func<Task> handler)
	{
		return _subject.Select(_ => Observable.FromAsync(async () => await handler()))
					   .Concat()
					   .Subscribe();
	}

	public IDisposable Subscribe(IObserver<Unit> observer)
	{
		return _subject.Subscribe(observer);
	}
}