using UnityEngine;
using SistemaDeInventario;

namespace SistemaDeDialogos
{
    public class Dialogo_DarItem : MonoBehaviour
    {
        [SerializeField] Item itens;
        [SerializeField] Dialogar dialogo;

        private void Start()
        {
            dialogo = GetComponent<Dialogar>();
            dialogo.AdicionarAcaoAoFimDeDialogo(AdicionarItem);
        }

        void AdicionarItem()
        {
            dialogo.RetirarINDialogo(AdicionarItem);
            GameManager.instancia.DarItem(itens);
        }
    }
}


