using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using HencoDesu.SimpleEventBus.Events;
using SimpleEventBus.Reactive.Events;

namespace SimpleEventBus.Reactive.Extensions;

public static class EventExtensions
{
	public static void RaiseFromObservable<T>(this IEvent @event, IObservable<T> observable)
	{
		observable.Subscribe(_ => @event.Raise());
	}

	public static void RaiseFromObservable<TData>(this IEvent<TData> @event, IObservable<TData> observable)
	{
		observable.Subscribe(@event.Raise);
	}

	public static IObservable<Unit> AsObservable(this IEvent @event)
	{
		if (@event is ReactiveEvent reactiveEvent)
		{
			return reactiveEvent;
		}
		
		var subject = new Subject<Unit>();
		@event.Subscribe(() => subject.OnNext(Unit.Default));
		return subject.AsObservable();
	}

	public static IObservable<TData> AsObservable<TData>(this IEvent<TData> @event)
	{
		if (@event is ReactiveEvent<TData> reactiveEvent)
		{
			return reactiveEvent;
		}
		
		var subject = new Subject<TData>();
		@event.Subscribe(data => subject.OnNext(data));
		return subject.AsObservable();
	}
}