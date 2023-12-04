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
            IActorController actor = collision.gameObject.GetComponent<IActorController>();

            if (newAction != null && actor != null)
            {
                if (type == ActionType.Action1) actor.GetStats().action1 = newAction;
                if (type == ActionType.Action2) actor.GetStats().action2 = newAction;
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(triggerTag))
        {
            IActorController actor = collision.GetComponentInParent<IActorController>();

            if(newAction!=null && actor!=null)
            {
                if (type == ActionType.Action1) actor.GetStats().action1 = newAction;
                if (type == ActionType.Action2) actor.GetStats().action2 = newAction;
            }
            Destroy(gameObject);
        }
    }
}
