using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]

// This class is the representation of the dialogue object in the game. A single DialogueObject accounts for a linear series of strings of dialogue, with optionally a set of choices and/or an "event" which changes a variable in the GameManager
public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue; //A list of dialogue lines, linearly displayed one by one
    [SerializeField] private Responses[] responses; //The list of responses that can happen at the end of the dialogue. If there are no responses, the dialogue ends.
    [SerializeField] public string[] triggerEvent; // The triggerEvent that causes a change in the GameManager  

    //Getters
    public string[] Dialogue => dialogue;

    public bool HasResponses => Responses != null && Responses.Length > 0;

    public Responses[] Responses => responses;
}
