﻿/// <summary>
/// Classe décrivant les contraintes à appliquer dans le calcul des chemins
/// </summary>
public sealed class NoGraphConstraint<T> : IGraphConstraint<T> where T : IGraphData
{
	/// <summary>
	/// Contrôle si la connexion peuut être empruntée pour le chemin en cours
	/// </summary>
	/// <param name="graphData">Données de la connexion</param>
	/// <returns>true si le chemin est limitépar les contraintes, false sinon</returns>
	public bool IsConstrainted(T graphData) => false;
}