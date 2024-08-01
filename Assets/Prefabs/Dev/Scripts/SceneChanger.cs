using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    void Awake()
    {
        UnityEngine.Cursor.visible = true;
    }
    public GameObject interactableObject;
    public float mindistance = 2.0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
           
            if (IsPlayerNearInteractable())
            {
                
                LoadNextScene();
            }
            
            
        }
    }

    bool IsPlayerNearInteractable()
    {
        float distance = Vector3.Distance(transform.position, interactableObject.transform.position);
      
        return distance < mindistance;
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }
}
