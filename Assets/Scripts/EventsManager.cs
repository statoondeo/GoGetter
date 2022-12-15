using System.Collections.Generic;
using System;

public class EventsManager
{
	protected readonly IDictionary<Events, EventModel> EventsAtlas;

	public EventsManager()
	{
		EventsAtlas = new Dictionary<Events, EventModel>();
		Events[] events = (Events[])Enum.GetValues(typeof(Events));
		for (int i = 0, nbItems = events.Length; i < nbItems; i++) EventsAtlas.Add(events[i], new EventModel());
	}

	private EventModel GetEvent(Events eventName) => EventsAtlas[eventName];
	public void Raise(Events eventName) => Raise(eventName, EventArg.Empty);
	public void Raise(Events eventName, EventArg eventArg) => GetEvent(eventName).Raise(eventArg);
	public void Register(Events eventToListen, Action<EventArg> callback) => GetEvent(eventToListen).RegisterListener(callback);
	public void UnRegister(Events eventToListen, Action<EventArg> callback) => GetEvent(eventToListen).UnregisterListener(callback);
}
