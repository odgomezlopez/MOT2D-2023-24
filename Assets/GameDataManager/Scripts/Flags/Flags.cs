using UnityEngine.SceneManagement;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using UnityEditor;

[System.Serializable]
public class Flags
{
    [SerializeField][SerializedDictionary("Flag", "State")]
    private SerializedDictionary<string, bool> flags = new SerializedDictionary<string, bool>();

    public void MarkFlag(string flagId)
	{
		if (!flags.ContainsKey(flagId))
		{
			flags.Add(flagId, true);
		}
	}

    public void UnMarkFlag(string flagId)
    {
        if (!flags.ContainsKey(flagId))
        {
            flags.Remove(flagId);
        }
    }

    public bool HasBeenMark(string flagId)
	{
		return flags.ContainsKey(flagId);
	}

	public void ClearFlags() { 
		flags.Clear();
	}
}
