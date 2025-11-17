using UnityEngine;
using UnityEngine.SceneManagement;


// A script that  door
public class DoorScript : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("ThisIsAScene");
        }
    }


    
     
}
