using System;

namespace EventBus.Tests;

public class SomeObjectWithEvent
{
	public event EventHandler? SomeEvent;

	public event EventHandler<string>? SomeEventWithData;

	public void RaiseEvent()
	{
		SomeEvent?.Invoke(this, EventArgs.Empty);
	}

	public void RaiseEvent(string data)
	{
		SomeEventWithData?.Invoke(this, data);
	}
}