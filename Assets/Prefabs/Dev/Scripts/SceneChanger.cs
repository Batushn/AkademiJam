using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject interactableObject;
    public float mindistance = 2.0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E tuþuna basýldý.");
            if (IsPlayerNearInteractable())
            {
                Debug.Log("Etkileþimdeki obje yakýnýnda.");
                LoadNextScene();
            }
            else
            {
                Debug.Log("Etkileþimdeki obje çok uzakta.");
            }
        }
    }

    bool IsPlayerNearInteractable()
    {
        float distance = Vector3.Distance(transform.position, interactableObject.transform.position);
        Debug.Log("Mesafe: " + distance);
        return distance < mindistance;
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Sahnenin gerçekten var olup olmadýðýný kontrol edin
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Bir sonraki sahne yükleniyor. Ýndeks: " + nextSceneIndex);
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("Sonraki sahne mevcut deðil.");
        }
    }
}
