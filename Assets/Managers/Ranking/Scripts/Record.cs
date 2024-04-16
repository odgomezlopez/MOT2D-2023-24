using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Record
{
    public string name;
    public int points;

    public Record(string name,int points)
    {
        this.name = name;
        this.points = points;
    }
}

class SurnameComparer : IComparer<Record>
{
    public int Compare(Record e1, Record e2)
    {
        return e2.points.CompareTo(e1.points);
    }
}
