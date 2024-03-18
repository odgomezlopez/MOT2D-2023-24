using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class InfoHUD : MonoBehaviour
{
    //Información del HP
    [SerializeField] GameObject HPBar;

    // Utilizo el Stats para asociar los elementos de la UI con los stats que quiera
    void Start()
    {
        //Asocio el HPBar
        if (HPBar)
        {
            //Obtenemos el dato con el que queremos sincronizar la UI
            Stats stats=null;
            if (GameObject.FindObjectOfType<PlayerController>())
            {
                stats = GameObject.FindObjectOfType<PlayerController>().GetStats();
            }


            //Asociamos el stat con la UI
            if (stats!=null)
            {
                //UpdateFilledImageUI updater = HPBar.GetComponent<UpdateFilledImageUI>();
                //updater.WatchEvent(stats.HP.OnIndicatorChange);
            }

        }
    }
}
