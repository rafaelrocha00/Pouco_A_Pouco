using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SistemaDeDialogos
{
    [CreateAssetMenu(fileName = "Novo Dialogo", menuName = "Dialogo/LevarDano")]
    public class Dialogo_LevarDano : Dialogo
    {
        public override void Agir(GameObject NPC)
        {
            Debug.Log("Agir foi chamado");
            GameManager.instancia.GetComponent<Jogador>().AdicionarStress();
        }
    }
}
