using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class MovePlataform : MonoBehaviour
{
    [Header("Plataform Movement Parameters")]
    [SerializeField] float speed = 5;

    int nextPosition;
    [SerializeField] List<Vector3> positions;

    //Eventos
    public UnityEvent actionEnded;

    private void Start()
    {
        //Rellenarlo con la ruta
        Transform patrolGO = gameObject.transform.Find("Patrol");
        for (int i = 0; i < patrolGO.childCount; i++)
        {
            positions.Add(patrolGO.GetChild(i).position);
        }

        //Añadir la posición inicial
        positions.Add(transform.position);

        //Siguiente posición
        nextPosition = 0;
    }

    public void Use()
    {
        StartCoroutine(MoveToNextPosition());
    }
    private IEnumerator MoveToNextPosition()
    {
        while (transform.position != positions[nextPosition])//Si no es necesaria exactitud utilizar Vector.Distance >1
        {
            transform.position = Vector3.MoveTowards(transform.position, positions[nextPosition], speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        nextPosition= (nextPosition+1) % positions.Count;
        actionEnded.Invoke();
    }
}