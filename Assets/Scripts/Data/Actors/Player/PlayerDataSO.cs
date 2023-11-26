using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerData", menuName = "Actor/PlayerData", order = 1)]

public class PlayerDataSO : ScriptableObject
{
    [Header("Info")]
    public Sprite sprite;
    public AnimatorController animator;
    public Color color = Color.white;

    [Header("Stats")]
    public PlayerStats playerStats;
}