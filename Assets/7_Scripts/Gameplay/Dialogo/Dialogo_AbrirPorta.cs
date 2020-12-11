using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SistemaDeDialogos
{
    [CreateAssetMenu(fileName = "Novo Dialogo", menuName = "Dialogo/AbrirPorta")]
    public class Dialogo_AbrirPorta : Dialogo
    {

        public override void Agir(GameObject NPC)
        {
            NPC.GetComponent<Interagivel_MudarMapa>().enabled = true;
            NPC.tag = "Porta";
        }
     
    }
}

