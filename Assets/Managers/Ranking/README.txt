=Requisitos=
- Input System https://docs.unity3d.com/Packages/com.unity.inputsystem@1.8/manual/Installation.html

=Uso básico=
1. Crear una nueva escena para el Ranking
2. Instanciar los dos objetos, RankingManager y Ranking UI en una escena. 
- Notese que RankingManager guarda el ranking en un archivo de guardado a parte.
3. En RankingUI cambiar la línea "int newScore = 5;" por un acceso al origen del nuevo valor del ranking. Por ejemplo: "int newScore => (int) ScoreManager.Instance.GetScore();"
4. Si se quiere modificar el número de entradas del Ranking, basta con modificar la función GenerateRanking() en RankingUI.

=Extra=
5. Si se quiere encriptar el archivo de guardado del ranking, en RankingManager, descomentar las lineas en las que aparezca "encriptDecriptStrategy". 
6. Si se quiere localizar se debe:
* Localizar los TextMeshPro de Name y Points del panel de Input
* En RankingUI, cambiar el tipo pointString de string a LocalizedString y cambiar donde aparezca "pointString"  por "pointString.GetLocalizedString()".