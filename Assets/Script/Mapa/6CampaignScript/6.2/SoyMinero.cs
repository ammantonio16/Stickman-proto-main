using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoyMinero : MonoBehaviour
{
    [SerializeField] PutGasMask haveMask;
    [SerializeField] List<Detectar_y_Echar> detectoresEnemys;
    int breakloop;
    void Start()
    {
        YaNoSoyMinero();
    }

    // Update is called once per frame
    void Update()
    {
        SerMinero();
    }
    void SerMinero()
    {
        //Chage tag for not detected
        //If I put on the mask
        if (haveMask.putMask)
        {
            if (breakloop < detectoresEnemys.Count) 
            {
                for (int i = 0; i < detectoresEnemys.Count; i++)
                {
                    detectoresEnemys[i].enabled = false;
                    Debug.Log("NiGGAAAAAA" + i);
                    breakloop++;
                }
            }
        }
    }
    void YaNoSoyMinero()
    {
        if (!haveMask.putMask)
        {
            for (int i = 0; i < detectoresEnemys.Count; i++)
            {
                detectoresEnemys[i].enabled = true;
                Debug.Log("NiGGAAAAAA" + i);
                
            }
        }
    }
}
