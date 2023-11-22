using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    //Referencia al enemyController
    EnemyStats stats;

    //Patrulla
    [SerializeField] int currentDestination = 0;
    [SerializeField] List<Vector3> patrolDestinations;

    // Start is called before the first frame update
    void Start()
    {
        //Obtenemos los stats
        stats = (EnemyStats) GetComponent<EnemyController>().GetStats();

        //Obtengo la lista de puntos a recorrer
        patrolDestinations = new List<Vector3>();

        //Añadir la posición inicial
        patrolDestinations.Add(transform.position);

        //Rellenarlo con la ruta
        Transform patrolGO = gameObject.transform.Find("Patrol");
        for(int i=0;i < patrolGO.childCount; i++)
        {
            patrolDestinations.Add(patrolGO.GetChild(i).position);
        }

        //Inicializo la posición inicial a la que voy
        currentDestination = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
