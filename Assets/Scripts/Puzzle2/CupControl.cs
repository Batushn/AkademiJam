using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class CupControl : MonoBehaviour
{
    public float rotationSpeed = 25.0f;
    public float scrollSpeed = 50.0f;
    public string targetTag = "PuzzleObject";


    private bool isRotating = false;
    private string lastHitPuzzleObjectName = null;
    new Rigidbody rigidbody;
    public int puzzleObjectCount = 3;
    private int clickedObjectCount = 0;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDrag()
    {
        isRotating = true;
        if(lastHitPuzzleObjectName != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.CompareTag(targetTag))
            {
                lastHitPuzzleObjectName = hit.collider.gameObject.name;
            }
            else
            {
                lastHitPuzzleObjectName = null;
            }
        }
    }
 
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isRotating == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.CompareTag(targetTag))
            {
                lastHitPuzzleObjectName = hit.collider.gameObject.name;
            }
            else
            {
                lastHitPuzzleObjectName = null;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
            
            if (lastHitPuzzleObjectName != null && isRotating == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.CompareTag(targetTag) && hit.collider.gameObject.name == lastHitPuzzleObjectName)
                {
                    hit.collider.gameObject.tag = "Untagged";
                    hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color (1f, 0.73f, 0.73f, 1f));
                    clickedObjectCount++;
                    if(clickedObjectCount == puzzleObjectCount) 
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
                    }
                }
                else
                { 
                    lastHitPuzzleObjectName = null;
                }
            }
            
        }
        else if(isRotating == false)
        {
            float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
            if (mouseScroll != 0)
            {
                float rotationAmount = mouseScroll * scrollSpeed;

                transform.Rotate(0, 0, rotationAmount);
            }
        }
    }
    private void FixedUpdate()
    {
        if (isRotating)
        {
            float x = Input.GetAxis("Mouse X") * rotationSpeed * 10 * Time.fixedDeltaTime;
            rigidbody.AddTorque(Vector3.down * x);

            float y = Input.GetAxis("Mouse Y") * rotationSpeed * 10 * Time.fixedDeltaTime;
            rigidbody.AddTorque(Vector3.right * y);
        }
    }

}
