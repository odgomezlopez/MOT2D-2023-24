=Requisitos

Instalar en Unity com.unity.nuget.newtonsoft-json. Pasos:

1. Abrir Package Manager

2. + -> Add package from git URL

3 Introducir la ULR: com.unity.nuget.newtonsoft-json

4. Instalar

=Uso
1. Instanciar en la escena el prefab SaveManager y elegir la saving strategy (JSON, XOR o XOR+Base64)

2. Para cualquier GameObject con información a guardar se debe (para referencia se puede consultar el Prefab SaveTest): 
2.1. Asociar el script SaveableEntity
2.2. Extender de la interfaz ISaveable
2.3. Codificar los metodos JToken CaptureAsJToken() y void RestoreFromJToken(JToken state)

3. SaveWrapper permite probar el sistema de manera fácil, aunque se recomienda acabar sustituyendolo por vuestro propio manager. Solo requiere pasarle el objeto SaveManager