using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(EnemyController))]
public class EnemyPatrol : MonoBehaviour
{
    //Referencia al enemyController
    EnemyStats stats;
    //Otras referencias
    SpriteRenderer sprite;
    Animator animator;
    Rigidbody2D rb;

    //Patrulla
    [SerializeField] int currentDestination = 0;
    [SerializeField] List<Vector3> patrolDestinations;
    [SerializeField] bool inverseFlip = false;


    //Corutina activa
    Coroutine cortina;

    // Start is called before the first frame update
    void Start()
    {
        #region Inicialización
        //Obtenemos los stats
        stats = (EnemyStats) GetComponent<EnemyController>().GetStats();
        //Obtenemos otras referencias
        sprite=GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        rb = GetComponent<Rigidbody2D>();

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
        #endregion
        //Inicializo la posición inicial a la que voy
        currentDestination = 0;

        //Iniciamos en el Start
        cortina=StartCoroutine(Patrol());
    }

    // Update is called once per frame
    void Update()
    {
        //Patrol();

        //Compruebo si hace falta Flip
        Flip();

        //Paso la información al animator
        UpdateAnimatorParameters();
    }

    private IEnumerator Patrol()
    {
        //Compruebo si he llegado a mi destino actual y si es así lo cambio
        while (true)
        {
            if (Vector3.Distance(transform.position, patrolDestinations[currentDestination]) < 0.3f)
            {
                currentDestination = (currentDestination + 1) % patrolDestinations.Count;
                //yield return new WaitForSecondsRealtime(1f);
            }

            //Me muevo hacia mi siguiente destino
            Vector3 dir = (patrolDestinations[currentDestination] - transform.position).normalized;
            rb.velocity = dir * stats.movementSpeed;

            yield return new WaitForEndOfFrame();

        }

        /*float checkInterval = 0.5f;
        float lastCheckTime = Time.time;
        Vector3 currentDirection = (patrolDestinations[currentDestination] - transform.position).normalized;

        while (true)
        {
            if (Time.time - lastCheckTime > checkInterval)
            {
                lastCheckTime = Time.time;
                if (Vector3.Distance(transform.position, patrolDestinations[currentDestination]) < 0.3f)
                {
                    currentDestination = (currentDestination + 1) % patrolDestinations.Count;
                    currentDirection = (patrolDestinations[currentDestination] - transform.position).normalized;
                    yield return new WaitForSeconds(1); // Optional wait at the new destination
                }
            }
            rb.velocity = currentDirection * stats.movementSpeed;
            yield return new WaitForEndOfFrame();
        }*/
    }

    private void Flip()
    {

        Vector2 aux = sprite.transform.localScale;
        if (rb.velocity.x > 0) aux.x = Mathf.Abs(aux.x);
        else if (rb.velocity.x < 0) aux.x = -Mathf.Abs(aux.x);

        if(inverseFlip) aux.x = -aux.x;

        sprite.transform.localScale = aux;
    }

    private void UpdateAnimatorParameters()
    {
        animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("velocityY", rb.velocity.y);
        animator.SetBool("isGrounded", Utils.Utils.IsGrounded2D(gameObject,0.5f));
    }
}
