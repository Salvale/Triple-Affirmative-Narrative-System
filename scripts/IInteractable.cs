//A basic interface to prop up the DialogueActivator class. Feel free to modify.
public interface IInteractable
{
    //The only thing that's necessary is that the player (the thing that is interacting) contains the playerMove script
    void Interact(playerMove player);

}
