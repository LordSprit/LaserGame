using UnityEngine;
using System.Collections;

public class TokenController : MonoBehaviour
{
    private Vector2 screenPoint;

    void Update()
    {
        if (GameController.sGameStatus == GameController.GameStatus.MOVING)
        {
            int i = 0;
            while (i < Input.touchCount)
            {
                Physics2D.raycastsHitTriggers = true;
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit.collider != null)
                {
                    Debug.Log("Holis");
                    if (Input.touchCount > 0 && Input.GetTouch(i).phase == TouchPhase.Moved)
                    {
                        Debug.Log("Dragging");
                    }
                    else if (Input.touchCount > 0 && Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        Debug.Log("Tap");
                    }
                }
                ++i;
            }
        }
    }

    void OnMouseDown()
    {
        if (GameController.sGameStatus == GameController.GameStatus.MOVING)
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Debug.Log("OnMouseDown");
    }

    void OnMouseDrag()
    {
        if (GameController.sGameStatus == GameController.GameStatus.MOVING)
        {
            Vector2 currentScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 currentPos = Camera.main.ScreenToWorldPoint(currentScreenPoint);
            transform.position = currentPos;
        }
        Debug.Log("OnMouseDrag");
    }
}
