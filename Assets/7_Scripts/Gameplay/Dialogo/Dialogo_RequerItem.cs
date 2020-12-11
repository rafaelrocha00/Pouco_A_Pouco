using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SistemaDeInventario;

namespace SistemaDeDialogos
{
    [CreateAssetMenu(fileName = "Novo Dialogo", menuName = "Dialogo/RequerItem")]
    public class Dialogo_RequerItem : Dialogo
    {
        [SerializeField] Item item;

        public override bool checarRequisito()
        {
            Inventario inve = GameManager.instancia.GetInventario();
            return inve.ChecarItem(item.name);
        }
    }
}


