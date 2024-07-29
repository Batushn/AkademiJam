using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
    
{
    public Button baslatButton, cikisButton;
    // Start is called before the first frame update
    void Start()
    {
        baslatButton.onClick.AddListener(BaslatFunction);
        cikisButton.onClick.AddListener(CikisFunction);

    }
    void BaslatFunction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void CikisFunction()
    {
        Application.Quit();
    }
}
