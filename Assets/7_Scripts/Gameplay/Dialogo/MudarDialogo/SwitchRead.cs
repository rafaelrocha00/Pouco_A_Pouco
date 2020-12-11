using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SistemaDeDialogos;

public class SwitchRead : MonoBehaviour
{
    [SerializeField]Dialogo novoDialogo;
    Dialogar dialogar;
    [SerializeField]bool estadoDesejado = true;
    [SerializeField] List<Switch> switchs = new List<Switch>();

    private void Start()
    {
        dialogar = GetComponent<Dialogar>();
        StartCoroutine(testar());
    }

    IEnumerator testar()
    {
        while (true)
        {
            verificarResultado();
            yield return new WaitForSeconds(1f);
        }
    }

    void verificarResultado()
    {

        bool funciona = true;
        for (int i = 0; i < switchs.Count; i++)
        {
            if (switchs[i].getEstado() != estadoDesejado)
            {
                funciona = false;
            }
        }

        if (funciona)
        {
            mudarDialogo();
            this.enabled = false;
        }
    }


    void mudarDialogo()
    {
        dialogar.MudarDialogo(novoDialogo);
    }
    
}
