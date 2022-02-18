using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyLife : MonoBehaviour
{
    public static EnemyLife life;
    public Image lifeBar;
    [Range(0, 100)]
    public float health = 100;

    private void Start()
    {
        life = this;
    }

    protected void Update()
    {
        lifeBar.fillAmount = (health) / 100;
    }

}