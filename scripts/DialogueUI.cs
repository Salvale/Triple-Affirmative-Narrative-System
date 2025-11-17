using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;
using System.Security;
using Unity.VisualScripting;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    public bool IsOpen {  get; private set; }

    [SerializeField] private DialogueObject text;
    private KeyCode interact = KeyCode.Space;
    private ResponseHandler responseHandler;
    public varTracker vars;


    private void Start()
    {
        responseHandler = GetComponent<ResponseHandler>();
        vars = GameObject.FindGameObjectWithTag("GameController").GetComponent<varTracker>();

        //textLabel.text = "Hello\nThis is my second line.";
        CloseDialogueBox(); 
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
    

        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return TypeText(dialogue, textLabel);

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
            

            if (dialogueObject.triggerEvent.Length > 0)
            {
                for (int j = 0; j < dialogueObject.triggerEvent.Length; j++)
                {
                    vars.parseEvent(dialogueObject.triggerEvent[j]);
                }
            }

            yield return new WaitUntil(() => Input.GetKeyDown(interact));


        }



        if (dialogueObject.HasResponses) 
        {

            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }
    }

    private IEnumerator TypeText(string text, TMP_Text textLabel)
    {

        textLabel.text = string.Empty;

        textLabel.text = text;
        yield return null;
    }

    private void CloseDialogueBox()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text =  string.Empty;
    }
}