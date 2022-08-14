using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    GameObject objetoClone;

    Camera cam;

    public bool canDestroyObject;
    public float timeToDestroyClon;
    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("CameraDisparo").GetComponent<Camera>();
    }
    void Update()
    {
        if(objetoClone != null)
        {
            objetoClone.transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z));
            //Cuando tengas un objeto o una de dos, no puedes abrir el menu o cuando lo abres el objeto se destruye
        }

        if (Input.GetButtonDown("Fire1"))
        {
            canDestroyObject = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
        {
            Destroy(objetoClone);
        }

        if (canDestroyObject)
        {
            timeToDestroyClon += Time.deltaTime;
            if(timeToDestroyClon >= 0.05f)
            {
                Destroy(objetoClone);
                timeToDestroyClon = 0;
                canDestroyObject = false;
            }
        }
        
    }
    public void SpawnObjectFromInventory(GameObject objeto)
    {
   
        objetoClone = Instantiate(objeto);
        Inventory.gameIsPause = false;

    }
    
}
