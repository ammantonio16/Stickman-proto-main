using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscensorMineroOn : MonoBehaviour
{
    ActivarModificador activar;
    Ascensor_Panel ascensorUse;
    [SerializeField] SaveButton buttonEnabled;
    private void Awake()
    {
        ascensorUse = GetComponent<Ascensor_Panel>();
        activar = GetComponent<ActivarModificador>();
    }
    void Start()
    {
        AscensorON();
    }

    void AscensorON()
    {
        if (!activar.modificacion)
        {
            ascensorUse.canUseAscensor = true;
            activar.modificacion = true;
            if(buttonEnabled.usos <= 0)
            {
                buttonEnabled.gameObject.SetActive(false);
                
            }
            
        }
    }
}
