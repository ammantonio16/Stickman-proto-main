using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseFloorAscensor : MonoBehaviour
{
    Rigidbody2D rb;
    int actualFloor;
    [HideInInspector] public int nextFloor;
    [HideInInspector] public bool elevatorActive;

    [Header("Velocity")]
    public float speed;

    [Header("Floor's Elevator")]
    public List<Transform> floors;

    [Header("Rope of Elevator")]
    [SerializeField] SpriteRenderer cuerda;
    float cuerda_Size;
    private void Awake()
    {
        nextFloor = 1;
        actualFloor = nextFloor;
        rb = GetComponent<Rigidbody2D>();
        cuerda_Size = speed / 10;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Elevator();
    }
    void Elevator()
    {
        Vector3 movePosition = transform.position;
        movePosition.y = Mathf.MoveTowards(transform.position.y, floors[nextFloor].position.y, speed * Time.fixedDeltaTime);
        rb.MovePosition(movePosition);

        //Mientras no hayas llegado a tu destino la cuerda aumentara
        if (transform.position != floors[nextFloor].position)
        {
            ElevatorRope();
            elevatorActive = true;
        }
        else 
        {
            actualFloor = nextFloor;
            elevatorActive = false;
        }
        
        
    }
    void ElevatorRope()
    {
        if(actualFloor < nextFloor)
        {
            //Rope Minus and Up = true;
            cuerda.size -= new Vector2(0f, cuerda_Size);
        }
        else if(actualFloor > nextFloor)
        {
            //Rope Increase and UP false
            cuerda.size += new Vector2(0f, cuerda_Size);
        }
    }
}
