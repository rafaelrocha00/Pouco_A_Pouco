using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SistemaDeEventos
{
    public class ListaDeEventos : MonoBehaviour
    {
        [SerializeField] int id;
        [SerializeField] List<Evento> eventos = new List<Evento>();
        [SerializeField] bool tocarAoAcordar;
        [SerializeField] bool liberarJogador = false;
        int indexAtual = 0;
        bool funcionando = true;
        bool estaEmAndamento = false;
        bool pararJogador = false;      
        

        private void Start()
        {
            Carregar();

            if (tocarAoAcordar)
            {
                Interagir(true);
            }
        }

        public void Interagir(bool pararInput, bool reset = true)
        {
            if (!funcionando)
            {
                return;
            }
            estaEmAndamento = true;

            if (pararInput)
            {
                GameManager.instancia.PararJogador();
                pararJogador = true;
            }

            if (reset)
            {
                indexAtual = 0;
            }
            Salvar();
            Avancar();
        }

        public void Avancar()
        {
            Debug.Log("Avancando evento");
            if (indexAtual == eventos.Count)
            {
                Debug.Log("Evento acabou");
                if (pararJogador)
                {
                    if(liberarJogador)
                        GameManager.instancia.LiberarJogador();
                }

                estaEmAndamento = false;
                return;
            }

            eventos[indexAtual].AdicionarOuvinte(Avancar);
            eventos[indexAtual].Trigger();
            indexAtual++;


        }

        public void Salvar()
        {
            GameManager.instancia.GetSave().Salvar("LE" + id.ToString(), 1);
        }

        public void Carregar()
        {
          float valor = GameManager.instancia.GetSave().Carregar("LE" + id.ToString());
       
        }

    }
}


