using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    void SaveData(GameData g);
    void LoadData(GameData g);
}
