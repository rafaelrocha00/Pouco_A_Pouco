using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EstadoMental
{
    [SerializeField]EstadoMental proximoEstado;
    [SerializeField]int medoNecessario;
    [SerializeField]List<Reacao> Reacoes = new List<Reacao>();

    public void ReagirCorrespondente(GameObject objeto, Habilidade habilidade, NPC NPCparaAssustar)
    {
        List<Reacao> ReacoesAssociadas = Reacoes.FindAll(x => x.getObjeto() == objeto);
        Reacao reacaoCorreta = ReacoesAssociadas.Find(x => x.getHabilidade() == habilidade);
        if(reacaoCorreta == null)
        {
            Debug.Log("Não há reacão setada");
            return;
        }
        Debug.Log("Reacao encontrada");
        reacaoCorreta.Reagir(NPCparaAssustar);
    }

    public int GetMedoNecessario()
    {
        return medoNecessario;
    }

}
