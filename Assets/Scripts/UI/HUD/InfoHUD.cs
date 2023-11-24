using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoHUD : MonoBehaviour
{
    //Elementos de la UI a asociar
    [SerializeField] GameObject HPBar;

    // Utilizo el Stats para asociar los elementos de la UI con los stats que quiera
    void Start()
    {
        //Asocio el HPBar
        if (HPBar)
        {
            //Obtenemos el dato con el que queremos sincronizar la UI
            Stats stats;
            if (GameObject.FindObjectOfType<Data>())
            {
                stats = GameObject.FindObjectOfType<Data>().stats;
            }
            else
            {
                stats = GameObject.FindObjectOfType<PlayerController>().GetStats();
            }

            //Asociamos el stat con la UI
            UpdateFilledImageUI updater = HPBar.GetComponent<UpdateFilledImageUI>();
            stats.HP.OnIndicatorChange.AddListener(updater.UpdateFilledImage);
        }
    }
}
