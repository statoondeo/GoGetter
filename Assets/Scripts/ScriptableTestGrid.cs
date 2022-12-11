using UnityEngine;

[CreateAssetMenu(fileName="Test", menuName="Test Grid")]
public class ScriptableTestGrid : ScriptableObject
{
	public Vector2Int[] TilesPositionAndRotation;
}