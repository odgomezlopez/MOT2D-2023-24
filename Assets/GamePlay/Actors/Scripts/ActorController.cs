using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

//Se hace una clase por simplicidad. Si se quisiera un compartamiento o stats diferentes entre un Player y Enemy, se recomienda diseñar la interfaz IActorController e implementar desde PlayerController e EnemyController. Alternativamente, se puede hace uso de herencia de la actual ActorControler más polimorfismo.

[ExecuteInEditMode]
public class ActorController : MonoBehaviour
{
    #region Data
    [Header("Player Info")]
    [SerializeField][Expandable] public ActorDisplayConfig displayConfig;
    [SerializeField][Expandable] public ActorMovementConfig movementConfig;
    //Referencia a los Stats
    [Header("Player Current Stats")]

    [SerializeField] public Stats stats;

    //Eventos generales
    [Header("Eventos generales")]
    public UnityEvent onHurt;
    public UnityEvent onDie;

    #region Getter/Setters
    public Stats Stats => stats;
    public ActorMovementConfig MovementConfig => movementConfig;
    #endregion

    #endregion
    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (!movementConfig) Debug.LogError("Not movement config attached to player controller");
        displayConfig?.ApplyGraphics(gameObject);

        //1. Me suscribo a los cambios de HP de los stats
        stats.HP.Reset();
        stats.HP.OnValueUpdate.AddListener(OnHPUpdate);


        //Comprobaciones de por si acaso
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), false);
    }

    protected virtual void OnDestroy()
    {
        //Desengachamos los eventos
        stats.HP.OnValueUpdate.RemoveListener(OnHPUpdate);
    }


    #region Init Editor
    private void OnRenderObject()
    {
        displayConfig?.ApplyGraphics(gameObject);      
    }

    private void OnValidate()
    {
        displayConfig?.ApplyGraphics(gameObject);
    }
    #endregion

    protected virtual void OnHPUpdate(float val)
    {
        if (val <= 0)        {
            onDie.Invoke();
        }
    }

    public virtual void OnHeal(float heal)
    {
        stats.HP.CurrentValue += heal;
    }
    public virtual void OnDamage(float damage)
    {
        if (stats.invulnerable) return;

        stats.HP.CurrentValue -= damage;
        if (stats.HP.CurrentValue > 0) //Lanzar corutina de invulnerabilidad temporal
        {
            onHurt.Invoke();
        }
    }

    public void OnDie()
    {
        stats.HP.CurrentValue = 0;
        //onDie?.Invoke();
    }

    #region Invulnerabilidad
    public void TemporalInvulneravility2D()
    {
        StartCoroutine(TemporalInvulneravilityCoroutine2D());
    }

    private IEnumerator TemporalInvulneravilityCoroutine2D()
    {
        //1. Activo invulnerabilidad
        stats.invulnerable = true;

        //1.1. Activar la invulnarabilidad utilizando las capas (Layer)
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"),true);

        //1.2. Hacer cambios al sprite para que el usuario sepa que es invulnerable. Rojo
        SpriteRenderer ren=GetComponentInChildren<SpriteRenderer>();//NO ES EFICIENTE
        Color colorBase = ren.color;

        ren.color = movementConfig.invulnerabilityColor;

        //2. Esperar el tiempo de la invulnerabilidad
        yield return new WaitForSecondsRealtime(movementConfig.invulnerabilitySeconds);

        //3.1. Deshacemos los cambios al sprite
        ren.color = colorBase;

        //3.2. Desactivar la invulnarabilidad utilizando las capas (Layer)
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), false);
        
        //3.3. Desactivo la invulnerabilidad en los stats
        stats.invulnerable = false;

        yield return null;
    }

    public void TemporalInvulneravility2D(float seconds)
    {
        StartCoroutine(TemporalInvulneravilityCoroutine2D(seconds));
    }
    private IEnumerator TemporalInvulneravilityCoroutine2D(float seconds)
    {
        stats.invulnerable = true;
        yield return new WaitForSecondsRealtime(seconds);
        stats.invulnerable = false;
    }
    #endregion
}

//#if UNITY_EDITOR
//[CustomEditor(typeof(ActorController))]
//public class ActorControllerEditor : Editor
//{
//    private bool showDisplayConfig;
//    private bool showMovementConfig;

//    public override void OnInspectorGUI()
//    {
//        serializedObject.Update(); // Sync serialized object

//        SerializedProperty property = serializedObject.GetIterator();
//        property.NextVisible(true); // Skip the script reference

//        while (property.NextVisible(false))
//        {
//            EditorGUILayout.PropertyField(property, true);

//            // Insert foldouts directly after relevant properties
//            if (property.name == nameof(ActorController.displayConfig))
//            {
//                DrawFoldout("Display Config", ref showDisplayConfig, (target as ActorController).displayConfig);
//            }
//            else if (property.name == nameof(ActorController.movementConfig))
//            {
//                DrawFoldout("Movement Config", ref showMovementConfig, (target as ActorController).movementConfig);
//            }
//        }

//        serializedObject.ApplyModifiedProperties(); // Apply changes
//    }

//    private void DrawFoldout(string title, ref bool foldoutState, Object configObject)
//    {
//        foldoutState = EditorGUILayout.Foldout(foldoutState, title, true, EditorStyles.foldoutHeader);
//        if (foldoutState)
//        {
//            if (configObject != null)
//            {
//                Editor configEditor = CreateEditor(configObject);
//                configEditor.OnInspectorGUI();
//            }
//            else
//            {
//                EditorGUILayout.HelpBox($"No {title} assigned.", MessageType.Warning);
//            }
//        }
//    }

//    private void AddSeparator()
//    {
//        GUILayout.Space(10);
//        var rect = EditorGUILayout.GetControlRect(false, 2);
//        EditorGUI.DrawRect(rect, Color.gray);
//        GUILayout.Space(10);
//    }
//}
//#endif