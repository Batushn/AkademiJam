using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject interactableObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E tu�una bas�ld�.");
            if (IsPlayerNearInteractable())
            {
                Debug.Log("Etkile�imdeki obje yak�n�nda.");
                LoadNextScene();
            }
            else
            {
                Debug.Log("Etkile�imdeki obje �ok uzakta.");
            }
        }
    }

    bool IsPlayerNearInteractable()
    {
        float distance = Vector3.Distance(transform.position, interactableObject.transform.position);
        Debug.Log("Mesafe: " + distance);
        return distance < 2.0f;
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Sahnenin ger�ekten var olup olmad���n� kontrol edin
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Bir sonraki sahne y�kleniyor. �ndeks: " + nextSceneIndex);
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("Sonraki sahne mevcut de�il.");
        }
    }
}
