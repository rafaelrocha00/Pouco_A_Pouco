using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SistemaDeDialogos
{
    [CreateAssetMenu(fileName = "Novo Dialogo", menuName = "Dialogo/AbrirPortaItem")]
    public class Dialogo_AbrirPortaComItem : Dialogo_RequerItem
    {
        public override void Agir(GameObject NPC)
        {
            NPC.GetComponent<Interagivel_MudarMapa>().enabled = true;
            NPC.tag = "Porta";
        }
    }

}
