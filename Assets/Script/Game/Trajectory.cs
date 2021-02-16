using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] int dotsNumber;
    [SerializeField] GameObject dotsParents;
    [SerializeField] GameObject dotPrefab;
    [SerializeField] float dotSpacing;

    Transform[] dotsList;

    Vector2 posDot;

    float timeStamp;

    private void Start()
    {
        //Hide();
        //PrepareDots();
    }
    /*void PrepareDots()
    {
        dotsList = new Transform[dotsNumber];

        for (int i = 0; i < dotsNumber; i++)
        {
            dotsList[i] = Instantiate(dotPrefab,null).transform;
            dotsList[i].parent = dotsParents.transform;
        }
    }
    public void UpdateDots(Vector3 arrowPos, Vector2 forceAppli)
    {
        timeStamp = dotSpacing;
        for (int i = 0; i < dotsNumber; i++)
        {
            posDot.x = arrowPos.x + forceAppli.x * timeStamp;
            posDot.y = (arrowPos.y + forceAppli.y * timeStamp)-(Physics2D.gravity.magnitude * timeStamp * timeStamp)/2f;

            dotsList[i].position = posDot;
            timeStamp += dotSpacing;

        }
    }
    public void Show()
    {
        dotsParents.SetActive(true);
    }
    public void Hide()
    {
        dotsParents.SetActive(false);
    }*/
}
