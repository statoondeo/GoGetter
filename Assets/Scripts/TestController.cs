using UnityEngine;

public class TestController : MonoBehaviour
{
	[SerializeField] protected Droppable[] Droppables;
	[SerializeField] protected Draggable[] Draggables;

	public GoGetterBoard Board;

	protected void Awake() => Board = new GoGetterBoard();

	protected void OnEnable()
	{
		GameManager.Instance.EventsManager.Register(Events.OnDragBegin, OnBeginDragCallback);
		GameManager.Instance.EventsManager.Register(Events.OnDropped, OnDroppedCallback);
		GameManager.Instance.EventsManager.Register(Events.OnDragEnd, OnEndDragCallback);
	}
	protected void OnDisable()
	{
		GameManager.Instance.EventsManager.UnRegister(Events.OnDragBegin, OnBeginDragCallback);
		GameManager.Instance.EventsManager.UnRegister(Events.OnDropped, OnDroppedCallback);
		GameManager.Instance.EventsManager.UnRegister(Events.OnDragEnd, OnEndDragCallback);
	}
	protected void OnBeginDragCallback(EventArg arg)
	{
		for (int i = 0; i < Draggables.Length; i++)
		{
			Draggables[i].DragStarted();
		}
	}
	protected void OnEndDragCallback(EventArg arg)
	{
		for (int i = 0; i < Draggables.Length; i++)
		{
			Draggables[i].DragEnded();
		}
	}
	protected void OnDroppedCallback(EventArg arg)
	{
		for (int i = 0; i < Draggables.Length; i++)
		{
			Draggable draggable = Draggables[i];
			int slotIndex = Board.GetTileSlot(draggable.TileIndex);
			draggable.MoveTo(Board.GetSlotTile(slotIndex) == -1 ? null : Droppables[slotIndex]);
		}
	}
}
