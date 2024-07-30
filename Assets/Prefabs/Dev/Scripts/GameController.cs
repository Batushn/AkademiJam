using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public List<Button> buttons;  // Numaral� butonlar� buraya ekleyin
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
        Debug.Log("GameController ba�lad� ve butonlar dinleniyor.");
    }

    void OnButtonClicked(Button button)
    {
        int buttonIndex = buttons.IndexOf(button);
        Debug.Log("Butona t�klan�ld�: " + button.name + " (�ndeks: " + buttonIndex + ")");

        if (buttonIndex == 0 && hasCompletedSequence)
        {
            Debug.Log("0. indekse t�kland� ve sekans tamamland�, sahne de�i�tiriliyor.");
            LoadNextScene();
            return;  // Di�er i�lemleri yapmadan ��k
        }

        if (buttonIndex == currentPointIndex)
        {
            Vector3 pointPosition = button.transform.position;
            pointPosition.z = 0; // Z de�erini ayarl�yoruz
            Debug.Log("Buton pozisyonu al�nd�: " + pointPosition);

            points.Add(pointPosition);

            if (points.Count > 1)
            {
                lineRenderer.positionCount = points.Count;
                lineRenderer.SetPositions(points.ToArray());
                Debug.Log("�izgi g�ncellendi. Noktalar: " + points.Count);

                // �izgilerin g�r�n�rl���n� kontrol etmek i�in ek ayarlar
                Debug.Log("LineRenderer ayarlar�: ");
                Debug.Log("Width: " + lineRenderer.startWidth + ", End Width: " + lineRenderer.endWidth);
                Debug.Log("Position Count: " + lineRenderer.positionCount);
            }
            else
            {
                Debug.LogWarning("Yeterli nokta yok, �izgi olu�turulam�yor.");
            }

            currentPointIndex++;
            Debug.Log("Sonraki nokta indeksi: " + currentPointIndex);

            // T�m butonlar tamamland���nda bu durumu kontrol edin
            if (currentPointIndex >= buttons.Count)
            {
                hasCompletedSequence = true;
                Debug.Log("T�m butonlar tamamland�. Sekans tamamland�.");
            }
        }
        else
        {
            Debug.LogWarning("Yanl�� s�rayla butona t�kland�. Mevcut �ndeks: " + currentPointIndex + ", T�klanan �ndeks: " + buttonIndex);
        }
    }

    void LoadNextScene()
    {
        // Mevcut sahnenin indeksini al�r ve bir sonraki sahneyi y�kler
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Bir sonraki sahnenin mevcut olup olmad���n� kontrol et
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
