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

	#region IDropHandler

	public void OnDrop(PointerEventData eventData)
	{
		if (eventData.pointerDrag is null) return;
		Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
		if (draggable is null) return;
		if (!draggable.IsDragging) return;

		draggable.Dropped();

		int tileIndex = Board.GetSlotTile(SlotIndex);
		int slotIndex = SlotIndex;
		int newTileIndex = draggable.TileIndex;
		int newSlotIndex = Board.GetTileSlot(draggable.TileIndex);

		if (tileIndex == -1)
		{
			if (newSlotIndex == -1)
			{
				Board.EmptySlot(slotIndex);
				Board.FillSlot(slotIndex, newTileIndex);
			}
			else
			{
				Board.EmptySlot(newSlotIndex);
				Board.FillSlot(slotIndex, newTileIndex);
			}
		}
		else
		{
			if (newSlotIndex == -1)
			{
				Board.EmptySlot(slotIndex);
				Board.FillSlot(slotIndex, newTileIndex);
			}
			else
			{
				Board.EmptySlot(slotIndex);
				Board.EmptySlot(newSlotIndex);
				Board.FillSlot(slotIndex, newTileIndex);
				Board.FillSlot(newSlotIndex, tileIndex);
			}
		}
		GameManager.Instance.EventsManager.Raise(Events.OnDropped);
	}

	#endregion

	protected void Awake()
	{
		RectTransform = GetComponent<RectTransform>();
		Board = TestController.Board;
	}
}
