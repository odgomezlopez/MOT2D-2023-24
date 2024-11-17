using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

//Clase de ayuda por si quieres guardar 

[RequireComponent(typeof(ActorController)), RequireComponent(typeof(SaveableEntity))]
public class PlayerSavedData : MonoBehaviour, ISaveable
{
    [System.Serializable]
    class SaveablePlayerData
    {
        public Stats stats;
        public Vector3 pos;
    }
    private void Start()    {}

    public string SaveData()
    {
        //La pongo en la estructura
        SaveablePlayerData data = new SaveablePlayerData
        {
            stats = gameObject.GetComponent<ActorController>().Stats,
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
         (gameObject.GetComponent<ActorController>().Stats).Update(d.stats);
        //transform.position = d.pos;
    }
}
