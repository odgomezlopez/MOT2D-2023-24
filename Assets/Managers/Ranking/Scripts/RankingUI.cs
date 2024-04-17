using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RankingUI : MonoBehaviour
{
    [Header("InputPanel")]
    [SerializeField] GameObject input;
    [SerializeField] TMP_InputField nameValue;
    [SerializeField] TextMeshProUGUI pointValue;

    //int newScore => (int) ScoreManager.Instance.GetScore();//TODO Cambiar a la fuente de tus puntos

    [SerializeField] Button saveButton;

    [Header("RankingPanel")]
    [SerializeField] bool showRankingDirectly=false;

    [SerializeField] GameObject ranking;
    [SerializeField] Selectable firstSelecteObjectRanking;
    [SerializeField] TextMeshProUGUI rankingValue;

    [Header("Localización")]
    [SerializeField] string pointString = "points";

    void Start()
    {
        if (!showRankingDirectly)
        {
            input.SetActive(true);
            ranking.SetActive(false);

            pointValue.SetText(((int)ScoreManager.Instance.GetScore()).ToString("D3"));
            nameValue.Select();
        }
        else
        {
            ToRankingPanel();
        }

    }

    public void InputCheck()
    {
        saveButton.interactable = (nameValue.text.Length == 3) && RankingManager.Instance.CheckName(nameValue.text);
        if(saveButton.interactable) saveButton.Select();
    }

    public void OnSaveButton()
    {
        int currentPlayerPos=RankingManager.Instance.AddPlayerRank(nameValue.text.ToUpper(), (int)ScoreManager.Instance.GetScore());
        ToRankingPanel();

    }

    public void CancelButton()
    {
        ToRankingPanel();
    }

    public void ToRankingPanel(int currentPlayerPos=0)
    {
        GenerateRanking(currentPlayerPos);


        //Activamos la pantalla del ranking
        input.SetActive(false);
        ranking.SetActive(true);
        firstSelecteObjectRanking.Select();
    }


    private void GenerateRanking(int currentPlayerPos=0)
    {
        //Recuperamos los rankings a mostrar (un total de 7)
        List<(int, Record)> lista = RankingManager.Instance.GetReducedRanking(currentPlayerPos,6);//RankingManager.Instance.GetFullRanking();

        string cadena = "";
        string sep = "########";
        int lastPos = 0;
        for (int i = 0; i < lista.Count; i++)
        {
            if (lastPos + 1 != lista[i].Item1) cadena+= "\n"+sep;
            lastPos = lista[i].Item1;

            string pos = lista[i].Item1.ToString("D2");
            string name = lista[i].Item2.name.ToUpper();
            string points = lista[i].Item2.points.ToString("D3");

            if (i != 0) cadena += "\n";

            cadena += $"{pos} - {name} - {points} {pointString}";
        }

        if (lastPos != RankingManager.Instance.RankingCount()) cadena += "\n"+ sep;

        rankingValue.SetText(cadena);

    }
}
