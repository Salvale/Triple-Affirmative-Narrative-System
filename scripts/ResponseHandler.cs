using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using NUnit.Framework;
using Unity.VisualScripting;

//This script tells the canvas how to handle branching responses. 
public class ResponseHandler : MonoBehaviour
{
    // Fields required for functionality
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private DialogueUI dialogueUI;

    private List<GameObject> tempResponseButtons = new List<GameObject>();

    // The start method only get the dialogueUI object.
    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
    }

    // The critical function. This method decides which possible responses are displayed for the player to choose. 
    // @params: Responses[] responses: The list of all possible responses
    public void ShowResponses(Responses[] responses)
    {
        // The boxHeight value ensures that the responses do not all occupy the same space. 
        float boxHeight = 0;

        // This loop continually shifts the responses so that they do not all occupy the same space, and also sets up buttons to click. 
        foreach (Responses response in responses)
        {
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText(); 
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));

            tempResponseButtons.Add(responseButton);

            boxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, boxHeight);
        responseBox.gameObject.SetActive(true);
    }

    // This function runs when a response is clicked. It loads the dialogueObject associated with the response
    // @params: Responses response: this field is a reference to the Responses object that was selected
    private void OnPickedResponse(Responses response)
    {
        //Clean up the response area.
        responseBox.gameObject.SetActive(false);

        foreach (GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }
        tempResponseButtons.Clear();
        // Show the dialogue associated with the response
        dialogueUI.ShowDialogue (response.DialogueObject);
    }
}
