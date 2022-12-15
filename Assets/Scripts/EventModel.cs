using System;

public class EventModel
{
	protected Action<EventArg> Listeners;
	public void Raise(EventArg eventArg) => Listeners?.Invoke(eventArg);
	public void RegisterListener(Action<EventArg> callback) => Listeners += callback;
	public void UnregisterListener(Action<EventArg> callback) => Listeners -= callback;
}