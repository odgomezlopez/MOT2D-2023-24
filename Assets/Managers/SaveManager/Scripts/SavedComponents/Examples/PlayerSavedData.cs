using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

//Clase de ayuda por si quieres guardar 

[RequireComponent(typeof(PlayerController)), RequireComponent(typeof(SaveableEntity))]
public class PlayerSavedData : MonoBehaviour, ISaveable
{
    
    [System.Serializable]
    class SaveablePlayerData
    {
        public PlayerStats stats;
        public Vector3 pos;
    }
    private void Start()    {}

    public string SaveData()
    {
        //La pongo en la estructura
        SaveablePlayerData data = new SaveablePlayerData
        {
            stats = (PlayerStats)gameObject.GetComponent<PlayerController>().GetStats(),
            pos=transform.position,
        };

        //Devuelvo los datos a guardar
        return JsonUtility.ToJson(data);
    }

    public void LoadData(string json)
    {
        //Comprobación
        if (json == null) return;

        //Casteo al tipo
        SaveablePlayerData d = JsonUtility.FromJson<SaveablePlayerData>(json);

        //Pongo los datos donde toque
         ((PlayerStats)gameObject.GetComponent<PlayerController>().GetStats()).Update((PlayerStats)d.stats);
        //transform.position = d.pos;
    }
}
