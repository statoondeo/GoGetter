﻿using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
	private static T instance;

	public static T Instance
	{
		get
		{
			if (instance != null) return (instance);

			instance = FindObjectOfType<T>();
			if (instance != null) return (instance);

			instance = new GameObject
			{
				name = typeof(T).Name,
				hideFlags = HideFlags.DontSave
			}.AddComponent<T>();

			return instance;
		}
	}

	protected virtual void Awake()
	{
		if (instance == null)
		{
			instance = this as T;
			DontDestroyOnLoad(gameObject);
			return;
		}
		Destroy(gameObject);
	}
}