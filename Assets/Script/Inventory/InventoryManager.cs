using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instanciaInventory;

    public int indexInventoryPosition; 

    public List<Vector2> inventoryPosition;
    private void Awake()
    {
        if (InventoryManager.instanciaInventory == null)
        {
            InventoryManager.instanciaInventory = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);

        }
    }
    
}
