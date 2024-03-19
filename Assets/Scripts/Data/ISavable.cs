using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    void SaveInfoToGameData(GameData g);
    void LoadInfoFromGameData(GameData g);
}
