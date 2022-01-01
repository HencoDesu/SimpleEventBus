# Simple Event Bus
Simple implementation for 
[Event Bus pattern](https://medium.com/elixirlabs/event-bus-implementation-s-d2854a9fafd5)

|Package| Nuget                                                                                                                                          |
|-------|------------------------------------------------------------------------------------------------------------------------------------------------|
|HencoDesu.SimpleEventBus| [![NuGet](https://img.shields.io/nuget/v/HencoDesu.SimpleEventBus)](https://www.nuget.org/packages/HencoDesu.SimpleEventBus)                   |
|HencoDesu.SimpleEventBus.Reactive| [![NuGet](https://img.shields.io/nuget/v/HencoDesu.SimpleEventBus.Reactive)](https://www.nuget.org/packages/HencoDesu.SimpleEventBus.Reactive) |

## Define own events
Events are just an objects inherited from ```Event``` class.
```CSharp
using HencoDesu.SimpleEventBus.Events;

namespace MyGreatNamespace;

public class MyGreatEvent : Event {}
```

Or if you want to pass some data to handlers you can use ```Event<TData>``` as base class
```CSharp
using HencoDesu.SimpleEventBus.Events;

namespace MyGreatNamespace;

public class MyGreatEventWithData : Event<MyEventDataType> {}
```
Just remember that your events should have public parameterless constructor

## Raising and subscribing to events
```CSharp
using HencoDesu.SimpleEventBus;

namespace MyGreatNamespace;

public class MyGreatEventHandler 
{
    public MyGreatEventHandler(IEventBus eventBus) 
    {
        //Subscribe to event
        eventBus.GetEvent<MyGreatEvent>().Subscribe(OnSomethingGreat);
        
        //Raise event
        eventBus.GetEvent<AnotherGreatEvent>.Raise();
    }
    
    private void OnSomethingGreat()
    {
        Console.WriteLine("Something great was happend!");
    }
}
```

## Pass standard C# events to Event Bus
```CSharp
using HencoDesu.SimpleEventBus;

namespace MyGreatNamespace;

public class MyGreatClass 
{
    public MyGreatClass(IEventBus eventBus) 
    {
        var someObjectWithEvent = new SomeObjectWithEvent();
        
        //Subscribe to standard C# event and pass it to Event Bus
        eventBus.GetEvent<AnotherGreatEvent>().RaiseFromEventPattern(h => someObjectWithEvent.SomeEvent += h);
    }
}
```

## Use with System.Reactive
By installing [HencoDesu.SimpleEventBus.Reactive](https://www.nuget.org/packages/HencoDesu.SimpleEventBus.Reactive/) 
and use ```ReactiveEvent``` and ```ReactiveEvent<TData>``` as 
base class for your events you can use all power of [System.Reactive](https://github.com/dotnet/reactive)!

Also with this extension you can raise events from ```IObservable``` and listen events created without this 
extension as ```IObservable```
```CSharp
eventBus.GetEvent<MyReactiveEvent>().Where(d => d.IsGreat).Subscribe(OnMyGreatReactiveEvent);
eventBus.GetEvent<MyEvent>().AsObservable().Where(d => d.IsGreat).Subscribe(OnMyGreatReactiveEvent);
```

## Customize behavior
You always free to use your custom type as base type for events, just implement ```IEvent``` or ```IEvent<TData>```
for this classes and remember that event types should have public parameterless constructor