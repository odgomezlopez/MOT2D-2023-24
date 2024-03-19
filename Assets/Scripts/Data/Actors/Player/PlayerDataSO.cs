using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerData", menuName = "Actor/PlayerData", order = 1)]

public class PlayerDataSO : ScriptableObject
{
    [Header("Info")]
    public Sprite sprite;
    public RuntimeAnimatorController animator;
    public Color color = Color.white;

    //[Header("Stats")]
    //public PlayerStats playerStats;

    public void Initialize(PlayerController player)
    {

        //1. Actualizo los stats
        //if (!player.StatsInitialized)
        //{
        //((PlayerStats)player.GetStats()).Update(playerStats);
        //player.StatsInitialized = true;
        //}

        //2. Actualizo el GameObject
        GameObject g = player.GetGameObject();

        //2.1. Actualizo los elementos visuales 
        SpriteRenderer ren = g.GetComponentInChildren<SpriteRenderer>();
        Animator ani = g.GetComponentInChildren<Animator>();

        //2.1. Actualizo sprite y animator si puedo
        if (ren)
        {
            if (sprite != null) ren.sprite = sprite;
            ren.color = color;
        }
        if (ani)
        {
            if (animator != null) ani.runtimeAnimatorController = animator;
        }

        //2.2. Recalcular el colider (MODIFICAR EN 3D)
        Collider2D col = g.GetComponentInChildren<Collider2D>();
        if (col)
        {
            if (col is BoxCollider2D boxCol)
            {
                boxCol.size = new Vector2(ren.bounds.size.x, ren.bounds.size.y);
                boxCol.offset = new Vector2(ren.bounds.center.x - g.transform.position.x, ren.bounds.center.y - g.transform.position.y);
            }

            if (col is CapsuleCollider2D capCol)
            {
                capCol.size = new Vector2(ren.bounds.size.x, ren.bounds.size.y);
                capCol.offset = new Vector2(ren.bounds.center.x - g.transform.position.x, ren.bounds.center.y - g.transform.position.y);
            }
        }
    }
}
