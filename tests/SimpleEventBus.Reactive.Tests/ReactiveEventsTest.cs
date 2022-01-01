using System;
using System.Reactive;
using System.Reactive.Subjects;
using FluentAssertions;
using HencoDesu.SimpleEventBus;
using SimpleEventBus.Reactive.Extensions;
using Xunit;

namespace SimpleEventBus.Reactive.Tests;

public class ReactiveEventsTest
{
	[Fact]
	public void SubscribeWithoutDataTest()
	{
		var eventBus = new HencoDesu.SimpleEventBus.EventBus();
		var invoked = false;
		eventBus.GetEvent<TestReactiveEvent>().Subscribe(() => invoked = true);
		
		eventBus.GetEvent<TestReactiveEvent>().Raise();

		invoked.Should().BeTrue();
	}
	
	[Fact]
	public void SubscribeWithDataTest()
	{
		var eventBus = new HencoDesu.SimpleEventBus.EventBus();
		const string testData = "testData";
		var dataFromEvent = "";
		eventBus.GetEvent<TestReactiveEvent>().Subscribe(d => dataFromEvent = d);
		
		eventBus.GetEvent<TestReactiveEvent>().Raise(testData);

		dataFromEvent.Should().Be(testData);
	}

	[Fact]
	public void SubscribeWithoutDataDisposingTest()
	{
		var eventBus = new EventBus();
		var invocations = 0;
		var subscription = eventBus.GetEvent<TestReactiveEvent>().Subscribe(() => invocations++);
		
		eventBus.GetEvent<TestReactiveEvent>().Raise();
		invocations.Should().Be(1);
		
		subscription.Dispose();
		eventBus.GetEvent<TestReactiveEvent>().Raise();
		invocations.Should().Be(1);
	}
	
	[Fact]
	public void SubscribeWithDataDisposingTest()
	{
		var eventBus = new EventBus();
		var invocations = 0;
		var subscription = eventBus.GetEvent<TestReactiveEvent>().Subscribe(_ => invocations++);
		
		eventBus.GetEvent<TestReactiveEvent>().Raise("");
		invocations.Should().Be(1);
		
		subscription.Dispose();
		eventBus.GetEvent<TestReactiveEvent>().Raise("");
		invocations.Should().Be(1);
	}

	[Fact]
	public void RaiseFromObservableExtensionTest()
	{
		var subject = new Subject<Unit>();
		var eventBus = new EventBus();
		var invocations = 0;
		
		eventBus.GetEvent<TestReactiveEvent>().Subscribe(() => invocations++);
		eventBus.GetEvent<TestReactiveEvent>().RaiseFromObservable(subject);
		
		subject.OnNext(Unit.Default);
		invocations.Should().Be(1);
		
		const string testData = "testData";
		var dataFromEvent = "";
		var dataSubject = new Subject<string>();
		
		eventBus.GetEvent<TestReactiveEvent>().Subscribe(d => dataFromEvent = d);
		eventBus.GetEvent<TestReactiveEvent>().RaiseFromObservable(dataSubject);
		
		dataSubject.OnNext(testData);
		dataFromEvent.Should().Be(testData);
	}

	[Fact]
	public void AsObservableExtensionTest()
	{		
		var eventBus = new EventBus();
		const string testData = "testData";
		var dataFromEvent = "";

		eventBus.GetEvent<TestReactiveEvent>().AsObservable().Subscribe(d => dataFromEvent = d);
		eventBus.GetEvent<TestReactiveEvent>().Raise(testData);
		
		dataFromEvent.Should().Be(testData);
	}
}