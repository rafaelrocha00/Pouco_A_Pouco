using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Switch : MonoBehaviour
{
    [SerializeField] bool estado = false;
    [SerializeField] Action InEstadomudado;

    public virtual void mudarEstado(bool novoEstado)
    {
        estado = novoEstado;
    }

    public bool getEstado()
    {
        return estado;
    }
}
