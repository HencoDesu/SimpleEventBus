using FluentAssertions;
using HencoDesu.SimpleEventBus;
using HencoDesu.SimpleEventBus.Extensions;
using Xunit;

namespace SimpleEventBus.Tests;

public class EventBusTests
{
	[Fact]
	public void GetEventTest()
	{
		var eventBus = new EventBus();
		var firstInstance = eventBus.GetEvent<TestEvent>();
		var secondInstance = eventBus.GetEvent<TestEvent>();

		secondInstance.Should().Be(firstInstance);
	}

	[Fact]
	public void SubscribeWithoutDataTest()
	{
		var eventBus = new EventBus();
		var invoked = false;
		eventBus.GetEvent<TestEvent>().Subscribe(() => invoked = true);
		
		eventBus.GetEvent<TestEvent>().Raise();

		invoked.Should().BeTrue();
	}
	
	[Fact]
	public void SubscribeWithDataTest()
	{
		var eventBus = new EventBus();
		const string testData = "testData";
		var dataFromEvent = "";
		eventBus.GetEvent<TestEvent>().Subscribe(d => dataFromEvent = d);
		
		eventBus.GetEvent<TestEvent>().Raise(testData);

		dataFromEvent.Should().Be(testData);
	}

	[Fact]
	public void SubscribeWithoutDataDisposingTest()
	{
		var eventBus = new EventBus();
		var invocations = 0;
		var subscription = eventBus.GetEvent<TestEvent>().Subscribe(() => invocations++);
		
		eventBus.GetEvent<TestEvent>().Raise();
		invocations.Should().Be(1);
		
		subscription.Dispose();
		eventBus.GetEvent<TestEvent>().Raise();
		invocations.Should().Be(1);
	}
	
	[Fact]
	public void SubscribeWithDataDisposingTest()
	{
		var eventBus = new EventBus();
		var invocations = 0;
		var subscription = eventBus.GetEvent<TestEvent>().Subscribe(_ => invocations++);
		
		eventBus.GetEvent<TestEvent>().Raise("");
		invocations.Should().Be(1);
		
		subscription.Dispose();
		eventBus.GetEvent<TestEvent>().Raise("");
		invocations.Should().Be(1);
	}

	[Fact]
	public void RaiseFromEventPatternExtensionTest()
	{
		var caller = new SomeObjectWithEvent();
		var eventBus = new EventBus();
		var invocations = 0;
		const string testData = "testData";
		
		eventBus.GetEvent<TestEvent>().Subscribe(() => invocations++);
		eventBus.GetEvent<TestEvent>().RaiseFromEventPattern(listener => caller.SomeEvent += listener);
		
		caller.RaiseEvent();
		invocations.Should().Be(1);
		
		var dataFromEvent = "";
		eventBus.GetEvent<TestEvent>().Subscribe(d => dataFromEvent = d);
		eventBus.GetEvent<TestEvent>().RaiseFromEventPattern(listener => caller.SomeEventWithData += listener);
		
		caller.RaiseEvent(testData);
		dataFromEvent.Should().Be(testData);
	}
}