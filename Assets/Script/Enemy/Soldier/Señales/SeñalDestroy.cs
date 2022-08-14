using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeñalDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public void DestroySeñal()
    {
        Destroy(this.gameObject);
    }
}
