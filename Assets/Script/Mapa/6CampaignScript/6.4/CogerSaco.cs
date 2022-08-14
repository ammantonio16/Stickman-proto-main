using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogerSaco : MonoBehaviour
{
    bool catchBag;
    [Header ("Soldier Info")]
    [SerializeField] SoldierLife vidaSoldier;
    [SerializeField] GameObject itemToCatch;
    public string nameSoldier;
    [Header("Time to take an action")]
    public float timeToCatchLimit;
    public float timeToTakeBag;
    float timeToCatch;
    
    public enum ActionBags
    {
        Leave_bag,
        Put_bag
    }
    [Header("What action")]
    public ActionBags soldierAction;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PickUpBag();
    }
    void PickUpBag()
    {
        if(vidaSoldier.vida > 0)
        {
            if (catchBag)
            {
                timeToCatch += Time.deltaTime;
                if (timeToCatch >= timeToCatchLimit)
                {
                    switch (soldierAction)
                    {
                        case ActionBags.Leave_bag:
                            itemToCatch.SetActive(false);
                            break;
                        case ActionBags.Put_bag:
                            itemToCatch.SetActive(true);
                            break;
                    }
                    catchBag = false;
                }
            }
            else timeToCatch = 0;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == nameSoldier)
        {
            catchBag = true;
            Debug.Log("Guillem the Golem");
            vidaSoldier.GetComponent<SoldadoNormal>().maxTiempoUbi = timeToTakeBag;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == nameSoldier)
        {
            catchBag = false;
        }
    }
}
