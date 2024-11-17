using UnityEngine.Events;
using UnityEngine;

public class ChangePlayerAction : MonoBehaviour
{
    public ActionSO newAction;
    public enum ActionType { Action1, Action2 };
    public ActionType type;

    public string triggerTag = "Player";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(triggerTag))
        {
            ActorController actor = collision.gameObject.GetComponent<ActorController>();

            if (newAction != null && actor != null)
            {
                if (type == ActionType.Action1) actor.Stats.action1 = newAction;
                if (type == ActionType.Action2) actor.Stats.action2 = newAction;
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(triggerTag))
        {
            ActorController actor = collision.GetComponentInParent<ActorController>();

            if(newAction!=null && actor!=null)
            {
                if (type == ActionType.Action1) actor.Stats.action1 = newAction;
                if (type == ActionType.Action2) actor.Stats.action2 = newAction;
            }
            Destroy(gameObject);
        }
    }
}
