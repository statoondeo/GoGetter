using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup), typeof(RectTransform))]
public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
	[SerializeField] protected Canvas Canvas;
	[SerializeField] protected TestController TestController;
	public int TileIndex;

	protected RectTransform RectTransform;
	protected CanvasGroup CanvasGroup;
	protected Vector2 InitialPosition;
	protected Droppable DroppableSlot;
	protected GoGetterBoard Board;
	protected bool DroppedOnSlot;

	protected void Awake()
	{
		RectTransform = GetComponent<RectTransform>();
		CanvasGroup = GetComponent<CanvasGroup>();
		Board = TestController.Board;
	}
	protected void Start()
	{
		IsDragging = false;
		InitialPosition = RectTransform.anchoredPosition;
	}

	public bool IsDragging { get; protected set; }
	public void Dropped() => DroppedOnSlot = true;
	public void OnBeginDrag(PointerEventData eventData)
	{
		GameManager.Instance.EventsManager.Raise(Events.OnDragBegin);
		transform.SetAsLastSibling();
		IsDragging = true;
		DroppedOnSlot = false;
	}
	public void OnDrag(PointerEventData eventData)
	{
		if (!IsDragging) return;
		RectTransform.anchoredPosition += eventData.delta / Canvas.scaleFactor;
	}
	public void OnEndDrag(PointerEventData eventData)
	{
		if (!IsDragging) return;
		IsDragging = false;
		if (!DroppedOnSlot) RectTransform.anchoredPosition = InitialPosition;
		GameManager.Instance.EventsManager.Raise(Events.OnDragEnd);
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		if (!PointerEventData.InputButton.Right.Equals(eventData.button)) return;
		RectTransform.rotation *= Quaternion.Euler(.0f, .0f, 90.0f);
	}
	public void DragStarted()
	{
		CanvasGroup.blocksRaycasts = false;
		CanvasGroup.alpha = .75f;
	}
	public void DragEnded()
	{
		CanvasGroup.blocksRaycasts = true;
		CanvasGroup.alpha = 1f;
	}
	public void MoveTo(Droppable droppable) => RectTransform.anchoredPosition = droppable is null ? InitialPosition : droppable.Position;
}
