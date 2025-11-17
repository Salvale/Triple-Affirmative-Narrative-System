using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

// This class allows the player to interact with IInteractable objects in the scene. It also contains basic movement scripting which may need to be removed or changed depending on your project. 

public class playerMove : MonoBehaviour
{
    // The DialogueUI is set via a field in the object inspector. 
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }

    //Basic parameters related to movement are set up here.
    public float moveSpd = 0.2f;
    Vector2 moveInput = Vector2.zero;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2d attached to the player object
    }

    // FixedUpdate is used to ensure regularity in player movement (not framerate dependant)
    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpd;

    }

    // Update is used to wait for the player to use the interact key to interact with the object.
    private void Update()
    {
        // KeyCode.E is assumed to be the default "interact" key. A more robust implementation of a rebindable "interact" key can be implemented at your discretion.
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Interactable != null)
            {
                Interactable.Interact(this);
            }
        }
    }

    // Boilerplate movement code
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // This code exists only for the purpose of demonstrating the default functionalities of the system. It can be replaced or removed at your discretion.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball") 
        {
            Debug.Log("Ball");
            GameObject.FindGameObjectWithTag("GameController").GetComponent<varTracker>().Increment("TalkedCount");
        }
    }
}
