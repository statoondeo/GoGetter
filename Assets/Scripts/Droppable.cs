using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class Droppable : MonoBehaviour, IDropHandler
{
	[SerializeField] protected TestController TestController;
	public int SlotIndex;

	protected RectTransform RectTransform;
	protected GoGetterBoard Board;

	public Vector2 Position => RectTransform.anchoredPosition;

	public void OnDrop(PointerEventData eventData)
	{
		if (eventData.pointerDrag is null) return;
		Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
		if (draggable is null) return;
		if (!draggable.IsDragging) return;

		draggable.Dropped();
		Board.SwitchTiles(SlotIndex, Board.GetSlotTile(SlotIndex), Board.GetTileSlot(draggable.TileIndex), draggable.TileIndex);
		GameManager.Instance.EventsManager.Raise(Events.OnDropped);
	}

	protected void Awake()
	{
		RectTransform = GetComponent<RectTransform>();
		Board = TestController.Board;
	}
}
