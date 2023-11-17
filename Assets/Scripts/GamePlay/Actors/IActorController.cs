using UnityEngine;

public interface IActorController
{
    //Stats
    public Stats GetStats();

    //Referencia
    public GameObject GetGameObject();

    //Metodos gesti�n HP
    public void OnHeal(float heal);
    public void OnDamage(float damage);

}
