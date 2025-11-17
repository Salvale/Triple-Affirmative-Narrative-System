using UnityEngine;


// This class determines which "Suites" of objects should be loaded when a scene is loaded.
public class SceneLoaderScript : MonoBehaviour
{
    [SerializeField] public GameObject[] suites; // All possible suites
    [SerializeField] public Conditional[] conditions; // Conditions corresponding to them
    void Start() 
    {   // Loop through all suites and check if their corresponding condition is true. Load the suite if it is true
        // __All__ suites with valid conditionals are loaded. 
        varTracker vars = GameObject.FindGameObjectWithTag("GameController").GetComponent<varTracker>();
        for (int i = 0; i < suites.Length; i++) 
        {
            if (vars.checkData(conditions[i]))
            {
                suites[i].SetActive(true);
            }
        }
    }
}
