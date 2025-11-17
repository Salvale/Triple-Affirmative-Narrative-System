using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.InputSystem;
using System.Collections;

// This class must be attached to an object with the tag "GameManager" in order to function. 
// This class tracks game data in one (or more) string, int dictionaries
// Conditionals are checked against this class.
public class varTracker : MonoBehaviour
{
    public static varTracker Instance;
    public static Dictionary<string, int> variables = new Dictionary<string, int>();

    // Initialize all "Default" variables here.
    // "TalkedCount" is only here as a demonstration. Remove or change as you'd like
    void Start()
    {
        variables.Add("TalkedCount", 0);
    }

    // Our starting dictionary
    public Dictionary<string, int> Variables => variables;

    // Increment any variable by a given amount
    // @params: string key: the key of the item to change. int amount: the amount to increment by. Defaults to 1.
    public void Increment(string key, int amount=1)
    {
        if (!variables.ContainsKey(key))
        {
            Debug.Log("Response parser error: key " + key + " does not exist");
            return;
        }
        variables[key] += amount;
        Debug.Log(variables[key]);
    }

    // This function checks if a provided conditional is valid.
    // @params: Conditional expression: The conditional object to be evaluated
    // @return: whether the conditional evaluates to true or not. 
    public bool checkData(Conditional expression)
    {
        // Dissecting the expression into key, comparator, and value.
        string key = expression.Key;
        string comparator = expression.Comparator;
        int value = expression.Value;

        if (!variables.ContainsKey(key))
        {
            Debug.Log("Response parser error: key " + key + " does not exist");
            return false;
        }

        int valueGot = variables[key];

        // This switch statement evaluates the expression based on the comparator. More may be added
        switch (comparator)
        {
            case ">":
                if (valueGot > value)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "<":
                if (valueGot < value)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case ">=":
                if (valueGot >= value)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "<=":
                if (valueGot <= value)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "=": // = and == are considered interchangeable.
            case "==":
                if (valueGot == value)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "!=":
                if (valueGot == value)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            default: // Default, i.e., the comparator was none of the above and so is not valid.
                Debug.Log("Response parser error: comparator " + comparator + "not a valid comparator");
                return false;
        }
    }

    // Alter an item in the variables dictionary.
    public void parseEvent(string expression)
    {
        string[] conditionList = expression.Split(" "); //Expected format: "VariableName modifier integer" - modifier is either change (by) or set (to)
        string key = conditionList[0];
        string modifier = conditionList[1];
        int value = int.Parse(conditionList[2]);

        // Ensure specified key exists
        if (!variables.ContainsKey(key))
        {
            Debug.Log("Response parser error: key " + key + " does not exist");
            return;
        }

        // This switch statement checks that the modifier is either change or set and performs the operation accordingly.
        switch (modifier)
        {
            case "c":
            case "change":
                variables[key] += value;
                return;
            case "s":
            case "set":
                variables[key] = value;
                return;
            default:
                Debug.Log("Response parser error: comparator " + modifier + "not a valid modifier");
                return;

        }

    }

    // There must be exactly one varTracker in the scene. If another exists, then this one is destroyed. 
    private void Awake()
    {
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            // If another instance already exists, destroy this one
            Destroy(this.gameObject);
        }
        else
        {
            // If this is the first instance, set it as the singleton
            Instance = this;
            // Prevent this GameObject from being destroyed on scene load
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
