using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NameList/Names")]
public class NameList : ScriptableObject
{
    public List<string> names = new List<string>();

    public string PickRandomName()
    {
        return names[Random.Range(0, names.Count)];
    }
}
