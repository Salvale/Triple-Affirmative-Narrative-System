using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Conditional 
{
    [SerializeField] private string key;
    [SerializeField] private string comparator; // Can only be: =, ==, !=, >, >=, <, <=
    [SerializeField] private int value;

    public string Key { get { return key; } }
    public string Comparator { get { return comparator; } }
    public int Value { get { return value; } }
}
