using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaElevador : MonoBehaviour
{
    Interagivel_MudarMapa porta;
    [SerializeField] int pontosNecessarios;
    C_Habilidade habilidade;
    [SerializeField]Animator anim;
  
    void Start()
    {
        if(porta == null)
        {
            porta = GetComponent<Interagivel_MudarMapa>();
        }
        habilidade = GameManager.instancia.GetComponent<C_Habilidade>();
        habilidade.AdicionarINGanharPontos(AbrirPorta);
        AbrirPorta();

    }

    public void AbrirPorta()
    {
         if(anim != null)
        anim.SetFloat("Pontos", habilidade.GetPontos());
        if(habilidade.GetPontos() >= pontosNecessarios)
        {
            anim.SetBool("Abriu", true);
            porta.enabled = true;
            porta.gameObject.tag = "Porta";
            Destroy(this);
        }
      
    }
    
}
