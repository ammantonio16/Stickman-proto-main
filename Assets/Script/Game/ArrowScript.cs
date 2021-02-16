using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ArrowScript : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 force;
    private Rigidbody2D rb;
    public float forceFactor;

    public GameObject trajectoryDot;
    public GameObject[] trajectoryDots; 
    public int number;

    public float range = 2;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        trajectoryDots = new GameObject[number];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = gameObject.transform.position;
            for (int i = 0; i < number; i++)
            {
                trajectoryDots[i] = Instantiate(trajectoryDot, gameObject.transform);
            }
        }
        if (Input.GetMouseButton(0))
        {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            force = endPos - startPos;
            gameObject.transform.position = endPos;
            if(force.magnitude > range)
            {
                endPos = endPos.normalized * range;
            }
            for (int i = 0; i < number; i++)
            {
                trajectoryDots[i].transform.position = calculatePosition(i * 0.1f);
            }



        }
        if (Input.GetMouseButtonUp(0))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.velocity = new Vector2(-force.x * forceFactor, -force.y * forceFactor);
            for (int i = 0; i < number; i++)
            {
                Destroy(trajectoryDots[i]);
            }
        }

    }
    private Vector2 calculatePosition (float elapsedTime)
    {
        return new Vector2(endPos.x, endPos.y) +
            new Vector2(-force.x * forceFactor, -force.y * forceFactor) * elapsedTime +
            0.5f * Physics2D.gravity * elapsedTime * elapsedTime;
    }
}
