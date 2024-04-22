using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new LocalizerData", menuName = "LocalizerData", order = 1)]
public class LocalizerDataConnector : ScriptableObject
{
    public int numberOfDeaths => GameDataManager.Instance.gameData?.numberOfDeaths ?? 0;
}
