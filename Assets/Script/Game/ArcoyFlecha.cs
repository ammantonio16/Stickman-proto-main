using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcoyFlecha : MonoBehaviour
{
    public float power = 10f;
    public float maxDrag = 5f;
    public Rigidbody2D rb;
    public LineRenderer lr;

    Vector3 dragStartPos;
    Vector3 dragStartPosMouse;
    Touch touch;
    // Start is called before the first frame update
    void Start()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                DragStart();
            }
            if (touch.phase == TouchPhase.Moved)
            {
                Dragging();
            }
            if (touch.phase == TouchPhase.Ended)
            {
                DragEnd();
            }
        }*/
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPosMouse = Input.mousePosition;
        }
    }
    void DragStart() {
        dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0;
        lr.positionCount = 1;
        lr.SetPosition(0, dragStartPos);
    }
    void Dragging() {
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
        draggingPos.z = 0;
        lr.positionCount = 2;
        lr.SetPosition(1, draggingPos);
    }
    void DragEnd() {
        Vector3 dragEnd = Camera.main.ScreenToWorldPoint(touch.position);
        dragEnd.z = 0;

        Vector3 force = dragStartPos - dragEnd;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;

        rb.AddForce(clampedForce, ForceMode2D.Impulse);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    void DragStartMouse()
    {
        dragStartPosMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragStartPosMouse.z = 0;
        lr.positionCount = 1;
        lr.SetPosition(0, dragStartPosMouse);
    }
    void DraggingMouse()
    {
        Vector3 draggingPosMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        draggingPosMouse.z = 0;
        lr.positionCount = 2;
        lr.SetPosition(1, draggingPosMouse);

    }
    void DragEndMouse()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        Vector3 dragEndMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragEndMouse.z = 0;

        Vector3 force = dragStartPosMouse - dragEndMouse;
        Vector3 clampedForceMouse = Vector3.ClampMagnitude(force, maxDrag) * power;

        rb.AddForce(clampedForceMouse, ForceMode2D.Impulse);


    }
    private void OnMouseDown()
    {
        DragStartMouse();

    }
    private void OnMouseDrag()
    {
        DraggingMouse();
    }
    private void OnMouseUp()
    {
        DragEndMouse();
    }
}
