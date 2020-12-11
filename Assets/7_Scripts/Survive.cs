using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survive : MonoBehaviour
{
     static GameObject objeto;

    private void Awake()
    {

        if ((objeto != gameObject) && (objeto != null))
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            objeto = gameObject;
            DontDestroyOnLoad(gameObject);
        }

    }
}
