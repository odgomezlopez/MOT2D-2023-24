using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerData", menuName = "PlayerData", order = 1)]

public class PlayerDataSO : ScriptableObject
{
    public Sprite sprite;
    public AnimatorController animator;
    public Color color = Color.white;

    public PlayerStats playerStats;
}