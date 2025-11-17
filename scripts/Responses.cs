using System;
using UnityEngine;

[System.Serializable]

// This class represents the responses that are selectable in dialogue.
public class Responses
{
    [SerializeField] private string responseText; //The text that the player selects
    [SerializeField] private DialogueObject dialogueObject; //The dialogueObject that is triggered when the player selects the response
    [SerializeField] private Conditional[] conditions; //Conditions that must be met in order for the response to be shown

    public string ResponseText()
    {
        // A quick loop through the conditions to determine if the response should be shown
        varTracker vars = GameObject.FindGameObjectWithTag("GameController").GetComponent<varTracker>();
        for (int i = 0; i < conditions.Length; i++)
        {
            if (!vars.checkData(conditions[i]))
            {
                return null;
            }
            
        } //If all conditions are passed, return the response.

        return responseText;
    }
    //Getter
    public DialogueObject DialogueObject => dialogueObject;
}

