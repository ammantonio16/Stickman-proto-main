using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFollowSoldier : MonoBehaviour
{
    [SerializeField] Transform ubiTrigger;
    void Update()
    {
        transform.position = ubiTrigger.position;
    }
}
