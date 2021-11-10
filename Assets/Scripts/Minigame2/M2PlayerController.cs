using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2PlayerController : MonoBehaviour
{
    GridController gridController;
    
    int playerRow = 0;
    int playerCol = 0;
    Direction playerDirection;

    bool keyDown;

    bool gameStarted = false;

    public LineRenderer grabLine;

    [Header("SFX")]
    public AudioClip walkClip;
    AudioSource audioSource;

    void Start()
    {
        gridController = GameObject.Find("GridController").GetComponent<GridController>();
        gridController.SetPositionValue(playerRow, playerCol, GridValue.Player);
        grabLine.gameObject.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    public void AllowMovement()
    {
        gameStarted = true;
    }

    void Update()
    {
        if (!gameStarted) return;
        
        if (!keyDown)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                playerDirection = Direction.Up;
                TryMove(playerRow + 1, playerCol);
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                playerDirection = Direction.Down;
                TryMove(playerRow - 1, playerCol);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                playerDirection = Direction.Left;
                TryMove(playerRow, playerCol - 1);
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                playerDirection = Direction.Right;
                TryMove(playerRow, playerCol + 1);
            }
        }

        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0) keyDown = false;

        if (Input.GetKeyDown(KeyCode.F))
        {
            // Check for box
            if (gridController.HasBoxInView(playerRow, playerCol, playerDirection))
            {
                //Pull box
                Debug.Log("Pull Box");
                gridController.PullBox(playerDirection);
            }

            StartCoroutine(RenderGrabLine());
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            // Check for box
            if (gridController.HasBoxInView(playerRow, playerCol, playerDirection))
            {
                //Push box
                Debug.Log("Push Box");
                gridController.PushBox(playerDirection);
            }

            StartCoroutine(RenderGrabLine());
        }
    }

    IEnumerator RenderGrabLine()
    {
        float currentTime = 0;
        float maxTime = 0.2f;
        LayerMask mask = LayerMask.GetMask("LaserHit");
        grabLine.gameObject.SetActive(true);
        grabLine.SetPosition(0, Vector3.zero);

        while (currentTime < maxTime)
        { 
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, mask))
            {
                grabLine.SetPosition(1, Vector3.forward * hit.distance);
            }
            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        grabLine.gameObject.SetActive(false);
    }

    void TryMove(int newRow, int newCol)
    {
        keyDown = true;
        audioSource.clip = walkClip;
        audioSource.Play();

        if (!gridController.IsValidPosition(newRow, newCol)) return;
        if (gridController.GetPositionValue(newRow, newCol) != GridValue.Space) return;

        gridController.SetPositionValue(playerRow, playerCol, GridValue.Space);
        gridController.SetPositionValue(newRow, newCol, GridValue.Player);

        Vector3 gridPos = gridController.GetPosition(newRow, newCol);
        gridPos.y = transform.position.y;
        transform.position = gridPos;

        playerRow = newRow;
        playerCol = newCol;

    }
}
