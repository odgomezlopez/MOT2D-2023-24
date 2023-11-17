using UnityEngine;

public class UpdateCaller : MonoBehaviour
{
    private static UpdateCaller instance = null;
    public static System.Action OnUpdate;

    void Update()
    {
        if (OnUpdate != null)
            OnUpdate();
    }
}

/*void Awake()
{
    if (instance == null)
    {
        instance = this;
        DontDestroyOnLoad(this);
    }
    else if (this != instance)
        Destroy(this);
}*/