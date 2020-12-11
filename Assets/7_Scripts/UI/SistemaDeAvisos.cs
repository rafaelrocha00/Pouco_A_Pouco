using UnityEngine;
using TMPro;
using System;
using SistemaDeDialogos;

namespace Avisos
{
    public class SistemaDeAvisos : MonoBehaviour
    {
        [SerializeField]GameObject painel;
        [SerializeField]TextMeshProUGUI texto;
        Dialogo dialogoAtual;
        bool mostrandoAviso;
        int index;

        Action Finalizado;

        private void Update()
        {
            if (mostrandoAviso)
            {
                GameManager.instancia.PararJogador();
            }
        }

        public void MostrarAviso(Dialogo dialogo)
        {
            dialogoAtual = dialogo;
            index = 0;
            mostrandoAviso = true;
            Avancar();
        }

        void MostrarAviso()
        {
            texto.text = dialogoAtual.GetMensagem(index);
        }

        public void Avancar()
        {
            if (index == dialogoAtual.GetTamanhoMensagem())
            {
                Fechar();
                return;
            }

            MostrarAviso();
            painel.SetActive(true);

            index++;
        }

        public void Fechar()
        {
            mostrandoAviso = false;
            painel.SetActive(false);
            texto.text = "";
            GameManager.instancia.LiberarJogador();
            Finalizado?.Invoke();
        }

        public void AdicionarAoTerminar(Action action)
        {
            Finalizado += action;
        }
    }
}


