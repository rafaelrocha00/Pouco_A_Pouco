using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interagivel_Flutuar : ObjetoInteragivel
{
    [SerializeField]Animator anim;
    [SerializeField] float raioDeEfeito = 1f;
    [SerializeField] bool UnicaVez = false;
    [SerializeField] List<Collider> colisoresPararTirar = new List<Collider>();
    LayerMask Interagiveis;
    C_Graficos graficos;

    public override void Setar()
    {
        base.Setar();
        if(anim == null)
        anim = GetComponent<Animator>();
        anim.enabled = false;
        Interagiveis = GameManager.instancia.GetLayerInteragiveis();
        habilidadeAssociada = GameManager.instancia.GetComponent<C_Habilidade>().GetHabilidade(1);
        graficos = GameManager.instancia.GetComponent<C_Graficos>();
    }

    public override void Interagir()
    {
        GameManager.instancia.GetComponent<C_Audio>().MostrarSomFlutuar();
        anim.enabled = true;
        anim.SetTrigger("Interagiu");
        Collider[] InteragiveisProximos = Physics.OverlapSphere(this.transform.position, raioDeEfeito, Interagiveis);
        List<NPC> npcsProximos = new List<NPC>();
        Debug.Log("Procurando NPCS");

        for (int i = 0; i < InteragiveisProximos.Length; i++)
        {
            if(InteragiveisProximos[i].tag == "NPC")
            {
                Debug.Log("Encontrou um npc");
                npcsProximos.Add(InteragiveisProximos[i].gameObject.GetComponent<NPC>());
            }
        }

        if (npcsProximos == null)
        {
            Debug.Log("Não há nenhum npc");
        }
        for (int i = 0; i < npcsProximos.Count; i++)
        {
            Debug.Log("NPC encontrado");
            npcsProximos[i].gameObject.GetComponent<NPC>().ObservarAmbiente(gameObject, GetHabilidade());
        }

        if (UnicaVez)
        {
            for (int i = 0; i < colisoresPararTirar.Count; i++)
            {
                colisoresPararTirar[i].enabled = false;
            }
        }

        graficos.AtivarEfeitoAberration(0.6f,0.1f);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(this.transform.position, raioDeEfeito);
    }

}
