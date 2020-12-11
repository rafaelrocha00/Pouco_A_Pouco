using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SistemaDeDialogos
{
    public class SwitchDialogo : Switch
    {
        [SerializeField] Dialogar dialogoTarget;
        Dialogar dialogoParaMudar;
        [SerializeField] Dialogo dialogo;

        private void Start()
        {
            dialogoParaMudar = GetComponent<Dialogar>();
            if (dialogoTarget == null)
            {
                dialogoTarget = dialogoParaMudar;
            }
            dialogoTarget.AdicionarAcaoAoFimDeDialogo(MudarDialogo);
        }

        public void MudarDialogo()
        {
            if (dialogo != null)
                dialogoParaMudar.MudarDialogo(dialogo);

            mudarEstado(true);
        }

    }
}


