using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData {

    //Puntos
    [SerializeField] public float points=0;
    [SerializeField] public Flags flags;

    GameData() {
        points = 0;//Valor inicial
    }


    public void AddPoints(float p)
    {
        points += p;
    }
}