using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public List<Button> buttons;  // Numaralý butonlarý buraya ekleyin
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
        Debug.Log("GameController baþladý ve butonlar dinleniyor.");
    }

    void OnButtonClicked(Button button)
    {
        int buttonIndex = buttons.IndexOf(button);
        Debug.Log("Butona týklanýldý: " + button.name + " (Ýndeks: " + buttonIndex + ")");

        if (buttonIndex == 0 && hasCompletedSequence)
        {
            Debug.Log("0. indekse týklandý ve sekans tamamlandý, sahne deðiþtiriliyor.");
            LoadNextScene();
            return;  // Diðer iþlemleri yapmadan çýk
        }

        if (buttonIndex == currentPointIndex)
        {
            Vector3 pointPosition = button.transform.position;
            pointPosition.z = 0; // Z deðerini ayarlýyoruz
            Debug.Log("Buton pozisyonu alýndý: " + pointPosition);

            points.Add(pointPosition);

            if (points.Count > 1)
            {
                lineRenderer.positionCount = points.Count;
                lineRenderer.SetPositions(points.ToArray());
                Debug.Log("Çizgi güncellendi. Noktalar: " + points.Count);

                // Çizgilerin görünürlüðünü kontrol etmek için ek ayarlar
                Debug.Log("LineRenderer ayarlarý: ");
                Debug.Log("Width: " + lineRenderer.startWidth + ", End Width: " + lineRenderer.endWidth);
                Debug.Log("Position Count: " + lineRenderer.positionCount);
            }
            else
            {
                Debug.LogWarning("Yeterli nokta yok, çizgi oluþturulamýyor.");
            }

            currentPointIndex++;
            Debug.Log("Sonraki nokta indeksi: " + currentPointIndex);

            // Tüm butonlar tamamlandýðýnda bu durumu kontrol edin
            if (currentPointIndex >= buttons.Count)
            {
                hasCompletedSequence = true;
                Debug.Log("Tüm butonlar tamamlandý. Sekans tamamlandý.");
            }
        }
        else
        {
            Debug.LogWarning("Yanlýþ sýrayla butona týklandý. Mevcut Ýndeks: " + currentPointIndex + ", Týklanan Ýndeks: " + buttonIndex);
        }
    }

    void LoadNextScene()
    {
        // Mevcut sahnenin indeksini alýr ve bir sonraki sahneyi yükler
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Bir sonraki sahnenin mevcut olup olmadýðýný kontrol et
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
