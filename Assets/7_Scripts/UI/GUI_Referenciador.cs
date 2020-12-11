using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Referenciador : MonoBehaviour
{
    [HideInInspector] public static GUI_Referenciador instancia;
    public GameObject sinalizador;
    public GameObject ObjetoInteragiveis;

    private void Awake()
    {

        if ((instancia != this) && (instancia != null))
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instancia = this;
            DontDestroyOnLoad(this);
        }

    }

}
