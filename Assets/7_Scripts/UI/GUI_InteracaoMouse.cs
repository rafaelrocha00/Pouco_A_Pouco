using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_InteracaoMouse : MonoBehaviour
{

    [SerializeField]GameObject BotaoDialogo;
    [SerializeField]GameObject Canvas;
    [SerializeField] Vector3 BotaoDialogoOffset  = new Vector3(-100f, 0, 0);
    Interagivel interagivelAtual;

    public void MostrarBotoesDeInteracao(Interagivel objeto)
    {
        BotaoDialogo.SetActive(true);
        interagivelAtual = objeto;
    }

    public void MoverBotoes()
    {
        BotaoDialogo.transform.position = Camera.main.WorldToScreenPoint(interagivelAtual.gameObject.transform.position) + BotaoDialogoOffset;
    }

    public void Update()
    {
        if(interagivelAtual != null)
        MoverBotoes();
    }
}
