using UnityEngine;
using System.Collections;

public class TokenController : MonoBehaviour
{
    private Vector2 screenPoint;
    private bool moving = false;
    private bool rotate = false;
    private bool unclick = false;
    private Transform spriteRendererTransform;
    private Transform emptyBoxInfoTransform;
    private SpriteRenderer spriteRenderer;
    private BoxController boxController;
    private Transform box;
    private Quaternion qTo = Quaternion.identity;
    private float timeTap = 0f;

    void Update()
    {
        Debug.Log("IS MOVING? " + moving);
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
                    Debug.Log("HIT");
                    if (Input.touchCount > 0 && Input.GetTouch(i).phase == TouchPhase.Moved)
                    {
                        Debug.Log("Dragging");
                    }
                    else if (Input.touchCount > 0 && Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        rotate = true;
                    }
                }
                ++i;
            }
            if (rotate)
                RotateToken();
            else
                this.transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo,  100 * Time.deltaTime);
            if (unclick)
                TranslateToBox();

        }
    }

    void OnMouseDown()
    {
        if (GameController.sGameStatus == GameController.GameStatus.MOVING)
        {
            
            timeTap = 0f;
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        }
            
        Debug.Log("OnMouseDown");
        
        
    }

    void OnMouseDrag()
    {
        if (GameController.sGameStatus == GameController.GameStatus.MOVING)
        {
            Vector2 currentScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 currentPos = Camera.main.ScreenToWorldPoint(currentScreenPoint);
            transform.position = currentPos;
            timeTap += Time.deltaTime;
        }
        Debug.Log("OnMouseDrag");
    }

    void OnMouseUp()
    {
        if(timeTap < 0.25f)
        {
            rotate = true;
        }

        

        if (box != null)
        {
            if (boxController.isEmpty)
                unclick = true;
        }
        else
        {
            //TODO Send token to initial place
        }
    }


    private void RotateToken()
    {
        if (this.transform.rotation.eulerAngles.z < 90.0f && this.transform.rotation.eulerAngles.z > 0.0f)
            this.transform.rotation = Quaternion.Euler(0, 0, 0f);
        else if (this.transform.rotation.eulerAngles.z > 90.0f && this.transform.rotation.eulerAngles.z < 180.0f)
            this.transform.rotation = Quaternion.Euler(0, 0, 90.0f);
        else if (this.transform.rotation.eulerAngles.z > 180.0f && this.transform.rotation.eulerAngles.z < 270.0f)
            this.transform.rotation = Quaternion.Euler(0, 0, 180.0f);
        else if (this.transform.rotation.eulerAngles.z > 270.0f && this.transform.rotation.eulerAngles.z < 360.0f)
            this.transform.rotation = Quaternion.Euler(0, 0, 270.0f);

        qTo = Quaternion.Euler(0, 0, this.transform.rotation.eulerAngles.z + 90);
        rotate = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag.Equals("FreePlace"))
        {
            box = other.transform.parent;
            spriteRendererTransform = box.GetChild(0).transform;
            emptyBoxInfoTransform = box.GetChild(1).transform;
            spriteRenderer = (SpriteRenderer)spriteRendererTransform.GetComponent<SpriteRenderer>();
            boxController = (BoxController)box.GetComponent(typeof(BoxController));

            if (boxController.isEmpty)
                spriteRenderer.color = Color.green;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag.Equals("FreePlace"))
        {
            spriteRenderer.color = Color.white;
            boxController.isEmpty = true;
            box = null;
            spriteRendererTransform = null;
            emptyBoxInfoTransform = null;
            spriteRenderer = null;
            boxController = null;
        }
    }

    private void TranslateToBox()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, box.transform.position, Time.deltaTime);
        if (this.transform.position.x == box.transform.position.x && this.transform.position.y == box.transform.position.y)
        {
            unclick = false;
            boxController.isEmpty = false;
        }
    }
}
