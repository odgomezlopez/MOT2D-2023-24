using UnityEngine;
using System.Collections;

public class MonoBehaviourSingleton<T> : MonoBehaviour
	where T : Component
{
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            //DontDestroyOnLoad(gameObject); // Optional: only if you want this to persist across scenes
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        _instance=null;
    }

    private static T _instance;
	public static T Instance {
		get {
			if (_instance == null) {
				var objs = FindObjectsOfType (typeof(T)) as T[];
				if (objs.Length > 0)
					_instance = objs[0];
				if (objs.Length > 1) {
					Debug.LogError ("There is more than one " + typeof(T).Name + " in the scene.");
				}
				if (_instance == null) {
					GameObject obj = new GameObject ();
					obj.hideFlags = HideFlags.HideAndDontSave;
					_instance = obj.AddComponent<T> ();
				}
			}
			return _instance;
		}
	}

	/*
	protected virtual void Awake()
	{

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	protected virtual void OnDestroy()
	{
		// Unsubscribe from the sceneLoaded event
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (_instance) _instance = null;
	}
	*/
}


public class MonoBehaviourSingletonPersistent<T> : MonoBehaviour
	where T : Component
{
	public static T Instance { get; private set; }
	
	public virtual void Awake ()
	{
		if (Instance == null) {
			Instance = this as T;
			DontDestroyOnLoad (this);
		} else {
			Destroy (gameObject);
		}
	}
}
