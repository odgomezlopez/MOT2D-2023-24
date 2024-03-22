=Dependencias
1. https://assetstore.unity.com/packages/tools/utilities/serialized-dictionary-243052
2. https://gist.github.com/mstevenson/4325117

=Uso
1. Crea un GameDataManager en la escena

2. Configurar el componente.
* Decidir si se quiere o no encriptado de datos (sin, XOR o XOR+Base)
* Se recompienda la comunicación con el componente utilizando el GameEventSO (Save, Load)
* Decidir como se va a utilizar el sistema. Algunas ideas de uso son:
** Opción 1. Activar flags de auto/guardado al empezar y acabar
** Opción 2. Llamar a los metodos LoadData(), SaveData() y DeleteData(), cuando se quiera (menú principal, menú de pausa).
*** Ejemplo 1. Al terminar un nivel suele ser util llamar a SaveData()
*** Ejemplo 2. Al morir suele ser recomendado llamar a LoadData() para que el jugador pierda el proceso realizado.
*** Ejemplo 3. Se pueden poner puntos de guardado desde lo que llamar a SaveData().

3. En cada gameObject que se quiera guardar/cargar su información se debe:
3.1. Añadir el componente SaveableEntity y establecer su ID (por defecto se genera uno aleatorio). (En la opción 1 de 3.2. este componente se añade automáticamente).
3.2. Debe haber a parte un componente que se encargue de la carga y guardado.
* Opción 1. Si se quiere guardar/cargar toda la información de un solo componente, basta con heredar de MonoBehaviourSaveable, MonoBehaviourSaveableSingleton o MonoBehaviourSaveableSingletonPersistent
* Opción 2. Hacer que el componente que se encarga del guardado implemente la interfaz ISaveable.

** Ejemplo 1. Componente simple, donde hay que guardar a si mismo u otra estructura que sea [System.Serializable]


    [RequireComponent(typeof(SaveableEntity))]
    public class TestComponent : MonoBehaviour, ISaveable 
    //Alternativamente, al ser un caso donde se guarda todo el componente, se puede heredar de la clase de ayuda MonoBehaviourSaveable.
    //En este caso hay que borrar los metodos de SaveData() y LoadData(string json). 
    {

        [SerializeField] float numPressSpace = 0;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                numPressSpace++;
            }
        }

        public string SaveData()
        {
            return JsonUtility.ToJson(this); //Guardar a si mismo
            // return JsonUtility.ToJson(objectoSalvable); //Guardar a otro

        }

        public void LoadData(string json)
        {
            if (json == null) return;

            JsonUtility.FromJsonOverwrite(json, this);//Cargarse a si mismo// Mandatory with Monobehaviour objects
            //objectoSalvable=JsonUtility.FromJson(json);//Cargarse a otro mismo// 
        }
    }

** Ejemplo 2: Guardar y cargar información de varios componentes

    [RequireComponent(typeof(PlayerController)), RequireComponent(typeof(SaveableEntity))]
    public class PlayerSavedData : MonoBehaviour, ISaveable
    {
    
        //Nuestro player controler tiene datos concretos que no queremos guardar (por ejemplo, referencias a componentes relativos a la escena), así que es mejor elegir lo que queremos (quizá para simplificar poner lo que se quiera guardar a este componente?)
        [System.Serializable]
        class SaveablePlayerData
        {
            public PlayerStats stats;
            public Vector3 pos;
        }
        private void Start()    {}

        public string SaveData()
        {
            //La pongo en la estructura
            SaveablePlayerData data = new SaveablePlayerData
            {
                stats = (PlayerStats)gameObject.GetComponent<PlayerController>().GetStats(),
                pos=transform.position,
            };

            //Devuelvo los datos a guardar
            return JsonUtility.ToJson(data);
        }

        public void LoadData(string json)
        {
            //Comprobación
            if (json == null) return;

            //Casteo al tipo
            SaveablePlayerData d = JsonUtility.FromJson<SaveablePlayerData>(json);

            //Pongo los datos donde toque
             ((PlayerStats)gameObject.GetComponent<PlayerController>().GetStats()).Update((PlayerStats)d.stats);
            //transform.position = d.pos;
        }
    }