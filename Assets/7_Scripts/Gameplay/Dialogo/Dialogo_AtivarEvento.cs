using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SistemaDeEventos;

namespace SistemaDeDialogos
{
    [CreateAssetMenu(fileName = "Novo Dialogo", menuName = "Dialogo/AtivarEvento")]
    public class Dialogo_AtivarEvento : Dialogo
    {
        public override void Agir(GameObject NPC)
        {
            Debug.Log("Agir foi chamado");
           NPC.GetComponent<ListaDeEventos>().Interagir(true);
        }
    }
}

