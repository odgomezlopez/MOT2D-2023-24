using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class SaveablePlayer : SaveableEntity
{
    //Clase de ayuda por si quieres guardar 
    [System.Serializable]
    class SaveablePlayerData
    {
        public PlayerStats stats;
    }
    SaveablePlayerData data;


    protected override void SaveData(GameData g) {
        //Obtengo la info a guardar
        PlayerStats stats = (PlayerStats) gameObject.GetComponent<PlayerController>().GetStats();

        //La pongo en la estructura
        data = new();
        data.stats = stats;
     
        //Guardo los datos
        SaveToStorage(data);
    }
    protected override void LoadData(GameData g) {
        //Recupero los datos
        data = RecoverFromStorage<SaveablePlayerData>();
  
        //Pongo los dats donde toque
        if(data!=null) ((PlayerStats)gameObject.GetComponent<PlayerController>().GetStats()).Update(data.stats);

    }
}
