using System;

/// <summary>
/// Classe de base représentant le distributeur d'objet
/// </summary>
/// <typeparam name="T">Type des données attachées aux connexions (poids, temps, etc)</typeparam>
public abstract class BasePoolManager<T> : IPoolManager
{
	protected readonly IPoolable[] PooledItems;
	protected readonly int Size;
	protected int CurrentIndex;
	protected IPoolableFactory ItemsFactory;

	/// <summary>
	/// Construction, tous les décorateurs sont créés ici.
	/// Ensuite un tableau cyclique est utilisé.
	/// </summary>
	/// <param name="size">Taille maxi du pool</param>
	/// <param name="itemsFactory">Usine de fabrication des décorateurs</param>
	/// <exception cref="ArgumentOutOfRangeException">La taille du pool doit être > 0</exception>
	/// <exception cref="ArgumentNullException">L'usine de fabrication est obligatoire</exception>
	protected BasePoolManager(int size, IPoolableFactory itemsFactory)
	{
		if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size));
		ItemsFactory = itemsFactory ?? throw new ArgumentNullException(nameof(itemsFactory));
		Size = size;
		CurrentIndex = 0;
		PooledItems = new IPoolable[Size];
		for (int i = 0; i < Size; i++) PooledItems[i] = ItemsFactory.Create();
	}

	/// <summary>
	/// Récupération d'un décorateur disponible
	/// </summary>
	/// <param name="vertex">Noeud à décorer</param>
	/// <returns>Le noued décoré</returns>
	/// <exception cref="InvalidOperationException">Si il n'y a plus de décorateur disponible</exception>
	public IPoolable GetNew()
	{
		int index = 0;

		// On fait au maximum un tour de tableau pour trouver le prochain décorateur disponible
		while(index < Size)
		{
			index++;
			if (!PooledItems[CurrentIndex].IsAvailable) continue;
			int selectedIndex = CurrentIndex;
			CurrentIndex = (CurrentIndex + 1) % Size;
			return (PooledItems[selectedIndex]);
		}
		// Aucun décorateur disponible
		throw new InvalidOperationException("Pool exceeded");
	}
}