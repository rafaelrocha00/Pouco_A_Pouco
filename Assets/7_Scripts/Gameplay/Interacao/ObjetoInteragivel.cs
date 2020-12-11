using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoInteragivel : Interagivel
{
    protected string nomeParametroShader = "_ForcaHatch";
    protected Habilidade habilidadeAssociada;
    MeshRenderer meshRenderer;
    [SerializeField]bool podeInteragirB = true;

    public void Start()
    {
        Setar();
    }

    public override void Setar()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if(meshRenderer == null)
        {
            meshRenderer = GetComponentInChildren<MeshRenderer>();
        }
    }

    public override void Ligar()
    {
        //meshRenderer.material.SetFloat(nomeParametroShader, 1f);
    }

    public override void Desligar()
    {
        //meshRenderer.material.SetFloat(nomeParametroShader, 0f);
    }

    public override void Interagir()
    {
        Debug.Log("Interagiu com " + gameObject.name);
        //TODO:Interação;
    }

    public override Habilidade GetHabilidade()
    {
        return habilidadeAssociada;
    }

    public override bool podeInteragir()
    {
        return podeInteragirB;
    }

    protected override void TirarInteracao()
    {
        podeInteragirB = false;
    }

    protected override void ColocarInteracao()
    {
        podeInteragirB = true;
    }
}
