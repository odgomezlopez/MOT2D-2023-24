using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class RankingManager : MonoBehaviourSingleton<RankingManager> {

    public List<Record> ranking;

    #region Save/Load
    // Start is called before the first frame update
    [SerializeField] string rankingFile = "ranking.dat";
    string fullFilePath;
    //[SerializeField] EncriptDecriptStrategy encriptDecriptStrategy;//Descomentar aquí y sus usos si se quiere encriptar el Ranking

    private void Awake()
    {
        try
        {
            fullFilePath = Path.Combine(Application.persistentDataPath, rankingFile);
            if (System.IO.File.Exists(fullFilePath))
            {
                string text = System.IO.File.ReadAllText(fullFilePath);//Leemos
                //if (encriptDecriptStrategy) text = encriptDecriptStrategy.DecodeString(text);//Desencriptamos
                JsonUtility.FromJsonOverwrite(text, this);//Pasamos el JSON al archivo local
            }
        }
        catch { }
    }

    private void OnDestroy()
    {
        string text = JsonUtility.ToJson(this); //Pasamos a JSON
        //if (encriptDecriptStrategy) text = encriptDecriptStrategy.EncodeString(text);//Encriptamos
        System.IO.File.WriteAllText(fullFilePath, text);//Guardamos
    }

    public void DeleteData()
    {
        fullFilePath = Path.Combine(Application.persistentDataPath, rankingFile);
        if (System.IO.File.Exists(fullFilePath))
        {
            System.IO.File.Delete(fullFilePath);
        }
    }
    #endregion

    void Start()
    {
        if (ranking == null) ranking = new List<Record>();
    }

    public bool CheckName(string newName)
    {
        for (int i = 0; i < ranking.Count; i++)
        {
            if (ranking[i].name == newName) return false;
        }
        return true;
    }

    public int AddPlayerRank(string name, int points)
    {
        if (!CheckName(name)) return -1;

        Record r = new Record(name, points);
        ranking.Add(r);
        ranking.Sort(new SurnameComparer());

        //Devuelvo la posición inciial
        return ranking.IndexOf(r);
    }

    public int RankingCount()
    {
        return ranking.Count;
    }

    public List<(int, Record)> GetFullRanking()
    {
        ranking.Sort(new SurnameComparer());

        List<(int, Record)> lista = new List<(int, Record)>();

        for (int i=0;i<ranking.Count;i++)
        {
            lista.Add((i + 1, ranking[i]));
        }

        return lista;
    }

    public List<(int, Record)> GetReducedRanking(int currentPlayerPos, int maxShowable = 6)
    {
        //Ordeno
        ranking.Sort(new SurnameComparer());

        //Decido las posiciones del ranking a mostrar
        List<(int, Record)> lista = new List<(int, Record)>();

        List<int> posicionesMostrar = new List<int>();

        //calculo las posiciones de la lista a mostrar
        if (ranking.Count < maxShowable || currentPlayerPos < maxShowable)
        {
            for (int i = 0; (i < ranking.Count) && i < maxShowable; i++)
            {
                posicionesMostrar.Add(i);
            }
        }
        else
        {
            posicionesMostrar.Add(0);
            posicionesMostrar.Add(1);
            posicionesMostrar.Add(2);

            if (currentPlayerPos == ranking.Count - 1) posicionesMostrar.Add(currentPlayerPos - 2);
            posicionesMostrar.Add(currentPlayerPos - 1);
            posicionesMostrar.Add(currentPlayerPos);
            if (currentPlayerPos != ranking.Count - 1) posicionesMostrar.Add(currentPlayerPos + 1);
        }
        //añado los elementos
        foreach (int i in posicionesMostrar)
        {
            lista.Add((i + 1, ranking[i]));
        }

        return lista;
    }
}
#if UNITY_EDITOR

[CustomEditor(typeof(RankingManager))]
public class RankingManagerrEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws the default inspector

        RankingManager t = (RankingManager)target;

        if (GUILayout.Button("Delete Ranking Data"))
        {
            t.DeleteData();
        }
    }
}
#endif