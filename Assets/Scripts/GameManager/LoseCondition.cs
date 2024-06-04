using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCondition : MonoBehaviour
{

    private void OnEnable()
    {
        NuclearBase.lose += Quit;
    }


    private void Quit()
    {
        Application.Quit();
    }
}
