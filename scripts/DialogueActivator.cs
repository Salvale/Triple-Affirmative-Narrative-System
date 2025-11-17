using Unity.VisualScripting;
using UnityEngine;

// This script is attached to any object that can initiate dialogue.

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject[] dialogueObject;   // 1 to n dialogue objects may be attached to this object.
    [SerializeField] private Conditional[] conditions;          // If there is more than one dialogueObject, there should always be n-1 conditionals
                                                                // The conditionals determine which dialogueObject is shown.
                                                                // The first conditional in the list corresponds to the first dialogueObject, etc.
                                                                // The last dialogueObject will always be shown if no conditions are met.
                                                                // When the class is determining which dialogueObject to show, the first dialogueObject (i.e. the one with the lowest index) that has its conditions met gets priority. 

    // This function allows the player to interact with this DialogueActivator
    // @param: Collider2D collision: the data for the collision that occured, function can only proceed with a "player"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //The object must have a collider (set to trigger) attached to it in order for this method to initiate. This trigger represents the radius from which the player can interact with the object. 
        if (collision.CompareTag("Player") && collision.TryGetComponent(out playerMove player))
        {
            player.Interactable = this;
        }
    }

    // This function ensures that the player cannot interact with this DialogueActivator once it has left the trigger zone.
    // @param: Collider2D collision: the data for the collision that occured, function can only proceed with a "player"
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out playerMove player))
        {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
            }
        }
    }


    // This function is called when the player presses the interact button (Default: "E") while the player's Interactable field is set to the object.
    // The function loops through all dialogueObjects attached to the object and compares them with their corresponding conditionals. 
    // As soon as the function finds a dialogueObject whose conditional is valid according to the GameController object, the function displays that dialogueObject.
    // @param: playerMove player: the player that is interacting with the object.
    public void Interact(playerMove player)
    {
        //The "Default" line (always the last one) is set to the line to show. 
        DialogueObject line = dialogueObject[dialogueObject.Length - 1];
        varTracker vars = GameObject.FindGameObjectWithTag("GameController").GetComponent<varTracker>();

        // Loop through potential starting dialogue until you hit one where all conditions are met
        for (int i = 0; i < conditions.Length; i++)
        {
            //we exit as soon as a line checks out (i.e., its conditions are satisfied)
            if (vars.checkData(conditions[i]))
            {
                line = dialogueObject[i];
                break;
            }
        } 
        
        player.DialogueUI.ShowDialogue(line);
    }

}
