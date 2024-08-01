using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public List<Button> buttons;  
    public LineRenderer lineRenderer;
    private List<Vector3> points = new List<Vector3>();
    private int currentPointIndex = 0;
    private bool hasCompletedSequence = false;

    void Start()
    {
        foreach (var button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClicked(button));
        }
        
    }

    void OnButtonClicked(Button button)
    {
        int buttonIndex = buttons.IndexOf(button);
       

        if (buttonIndex == 0 && hasCompletedSequence)
        {
            
            LoadNextScene();
            return;  
        }

        if (buttonIndex == currentPointIndex)
        {
            Vector3 pointPosition = button.transform.position;
            pointPosition.z = 0;
            

            points.Add(pointPosition);

            if (points.Count > 1)
            {
                lineRenderer.positionCount = points.Count;
                lineRenderer.SetPositions(points.ToArray());
                
            }
            
            

            currentPointIndex++;
            

            
            if (currentPointIndex >= buttons.Count)
            {
                hasCompletedSequence = true;
                
            }
        }
        
    }

    void LoadNextScene()
    {
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }
}
