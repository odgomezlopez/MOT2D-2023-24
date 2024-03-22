using UnityEngine;

public interface ISaveable
{
    string SaveData();
    void LoadData(string json);
}
