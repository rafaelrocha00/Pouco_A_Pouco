using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SistemaDeDialogos;

public class IntroducaoSalaDeEntrada : MonoBehaviour
{
    [SerializeField]float tempoDeEspera;
    [SerializeField] Transform porta;
    [SerializeField] Dialogar dialogar;
    [SerializeField] Dialogo dialogoSegundo;
    [SerializeField] Companheiro companheiro;

    private void Start()
    {
        Iniciar();
        dialogar = GetComponent<Dialogar>();
    }

    public void Iniciar()
    {
        GameManager.instancia.PararJogador();
        StartCoroutine(esperar());
    }

    IEnumerator esperar()
    {
        yield return new WaitForSeconds(tempoDeEspera);
        dialogar.AdicionarAcaoAoFimDeDialogo(AndarAtePorta);
        dialogar.MostrarDialogo();
    }


    public void AndarAtePorta()
    {
        dialogar.RetirarINDialogo(AndarAtePorta);
        companheiro.PararDeSeguir();
        companheiro.Seguir(porta.position, 0.1f);
        companheiro.AdicionarINChegar(MostrarDialogoSegundo);
    }

    public void MostrarDialogoSegundo()
    {
        companheiro.RetirarINChegar(MostrarDialogoSegundo);
        dialogar.MudarDialogo(dialogoSegundo);
        StartCoroutine(esperarSegundo());
    }

    IEnumerator esperarSegundo()
    {
        yield return new WaitForSeconds(2f);
        dialogar.AdicionarAcaoAoFimDeDialogo(Liberar);
        dialogar.MostrarDialogo();
    }

    public void Liberar()
    {
        dialogar.RetirarINDialogo(Liberar);
        GameManager.instancia.LiberarJogador();
    }
}
