using System.Collections;
using UnityEngine;

public class RotateOnClick : MonoBehaviour
{
    GameObject[] puzzleObjects;
    public string targetTag = "PuzzleObject";
    public float rotationSpeed = 60f;
    public float rotationAngle = 30f;
    public float error = 1.5f;

    private bool rotate = false;

    private void Start()
    {
        puzzleObjects = GameObject.FindGameObjectsWithTag(targetTag);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && rotate == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            if (Physics.Raycast(ray, out RaycastHit hit) && hits[0].collider.CompareTag(targetTag))
            {
                rotate = true;
                GameObject rotateObject = hits[0].collider.gameObject;
                Vector3 clickedWorldPos = hits[0].point;

                Vector3 localClickPos = transform.InverseTransformPoint(clickedWorldPos);
                Vector3 localObjPos = transform.InverseTransformPoint(rotateObject.transform.position);

                if (localClickPos.x > localObjPos.x)
                {
                    StartCoroutine(RotateObject(rotateObject, -rotationAngle));
                }
                else
                {
                    StartCoroutine(RotateObject(rotateObject, rotationAngle));
                }
            }
        }
    }
    IEnumerator RotateObject(GameObject gameObject, float rotationAngle)
    {
        if (rotate == false)
        {
            yield break;
        }

        float startAngle = gameObject.transform.eulerAngles.z;

        float startTime = Time.time;
        float duration = Mathf.Abs(rotationAngle) / rotationSpeed;

        while (Time.time < startTime + duration)
        {

            float angle = Mathf.Lerp(0, rotationAngle, (Time.time - startTime) / duration);
            gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, startAngle + angle);
            yield return null;
        }
        gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, startAngle + rotationAngle);
        if (!IsPuzzleFinished())
        {
            rotate = false;
        }
        else
        {
            // TODO go to next scene
            Debug.Log("Puzzle finished");
        }


    }

    bool IsPuzzleFinished()
    {
        foreach (GameObject puzzleObject in puzzleObjects)
        {
            if (puzzleObject.transform.eulerAngles.z < -error || puzzleObject.transform.eulerAngles.z > error)
            {
                return false;
            }
        }
        return true;
    }
}
