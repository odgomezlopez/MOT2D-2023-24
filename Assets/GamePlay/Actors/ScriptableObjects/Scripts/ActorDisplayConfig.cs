using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "ActorDisplayConfig", menuName = "Actor/DisplayConfig", order = 1)]
public class ActorDisplayConfig : ScriptableObject
{
    [Header("Display")]
    public Sprite sprite;
    public RuntimeAnimatorController animator;
    public Color baseColor = Color.white;
    public bool defalutFlip = false;

    public void ApplyGraphics(GameObject actor)
    {
        if (actor == null) return;

        SpriteRenderer renderer = actor.GetComponentInChildren<SpriteRenderer>();
        Animator animatorComponent = actor.GetComponentInChildren<Animator>();

        if (renderer)
        {
            if (sprite) renderer.sprite = sprite;
            renderer.flipX = defalutFlip;
            renderer.color = baseColor;
        }

        if (animatorComponent && animator)
        {
            animatorComponent.runtimeAnimatorController = animator;
        }

        //AdjustCollider(actor, renderer);
    }

    private void AdjustCollider(GameObject actor, SpriteRenderer renderer)
    {
        if (renderer == null) return;

        Collider2D col = actor.GetComponentInChildren<Collider2D>();
        if (col)
        {
            if (col is BoxCollider2D boxCol)
            {
                boxCol.size = new Vector2(renderer.bounds.size.x, renderer.bounds.size.y);
                boxCol.offset = new Vector2(renderer.bounds.center.x - actor.transform.position.x, renderer.bounds.center.y - actor.transform.position.y);
            }

            if (col is CapsuleCollider2D capCol)
            {
                capCol.size = new Vector2(renderer.bounds.size.x, renderer.bounds.size.y);
                capCol.offset = new Vector2(renderer.bounds.center.x - actor.transform.position.x, renderer.bounds.center.y - actor.transform.position.y);
            }
        }
    }

}
