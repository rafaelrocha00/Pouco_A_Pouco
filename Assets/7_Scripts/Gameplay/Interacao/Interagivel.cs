using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interagivel : MonoBehaviour
{
    public abstract void Interagir();

    public abstract void Ligar();

    public abstract void Desligar();

    public abstract void Setar();

    public abstract Habilidade GetHabilidade();

    public abstract bool podeInteragir();

    protected abstract void TirarInteracao();

    protected abstract void ColocarInteracao();  
}
