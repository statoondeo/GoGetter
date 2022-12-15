public class GameManager : Singleton<GameManager>
{
	public EventsManager EventsManager { get; protected set; }

	protected override void Awake() => EventsManager = new EventsManager();
}
