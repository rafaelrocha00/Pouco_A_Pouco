using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace SistemaDeDialogos
{
    public class Dialogar : MonoBehaviour
    {
        GameObject mensageiro;
        [SerializeField] Dialogo dialogoAtual;
        Dialogo dialogoPadrao;
        int indexDialogo;
        bool estaDialogando;
        bool fazendoEscolha;
        [SerializeField] UnityEvent INDialogoFinalizado;

        GUI_Dialogar DialogarGUI;
        GUI_Referenciador referencia;

        bool EncontrandoNodePorFilho;

        private void Start()
        {
            if (DialogarGUI == null)
            {
                referencia = GUI_Referenciador.instancia;
                DialogarGUI = referencia.GetComponent<GUI_Dialogar>();

            }
        }

        private void Update()
        {
            ObservarInput();
        }

        void ObservarInput()
        {
            if (((estaDialogando) && (!fazendoEscolha)))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (DialogarGUI.DialogoFoiEscrito)
                    {
                        //Se ainda tem mensagem para mostrar, mudar o index e vá para o próximo dialogo.

                        if (indexDialogo < dialogoAtual.GetTamanhoMensagem() - 1)
                        {
                            indexDialogo++;
                            MostrarDialogo();
                        }
                        else
                        {
                            // Resertar index e encontrar próximo filho.
                            indexDialogo = 0;
                            EncontrarProximoNode();
                        }

                    }
                    else
                    {
   
                      DialogarGUI.MostrarMensagemInstantaniamente();
                            
                    }
                }
            }
        }

        /// <summary>
        /// Mostra o dialogo na tela.
        /// </summary>
        public void MostrarDialogo()
        {
            if (DialogarGUI.GetDialogoAtual() == null)
            {
                DialogarGUI.SetDialogoAtual(this);
            }
            if (DialogarGUI.GetDialogoAtual() != this)
            {
                return;
            }
            estaDialogando = true;

            // Para qualquer dialogo que esteja sendo mostrado.
            DialogarGUI.StopAllCoroutines();

            Debug.Log("Mostrando Dialogo");
            if ((dialogoAtual.GetMensagem().Length != 0) && (indexDialogo <= dialogoAtual.GetMensagem().Length))
            {
                // Mostra o dialogo. GUI_Dialogar só lida com strings.
                DialogarGUI.MostrarMensagem(dialogoAtual.GetMensagem(indexDialogo));
            }
            else
            {
                if(dialogoAtual.checarRequisito())
                dialogoAtual.Agir(gameObject);
                FecharDialogo();
            }
        }

        public void MostrarDialogo(Dialogo dialogo)
        {
            estaDialogando = true;
            indexDialogo = 0;
            // Para qualquer dialogo que esteja sendo mostrado.
            DialogarGUI.StopAllCoroutines();
            dialogoPadrao = dialogoAtual;
            dialogoAtual = dialogo;
            INDialogoFinalizado.AddListener(ResetarDialogo);

            Debug.Log("Mostrando Dialogo");
            if ((dialogoAtual.GetMensagem().Length != 0) && (indexDialogo <= dialogoAtual.GetMensagem().Length))
            {
                // Mostra o dialogo. GUI_Dialogar só lida com strings.
                DialogarGUI.MostrarMensagem(dialogoAtual.GetMensagem(indexDialogo));
            }
            else
            {
                FecharDialogo();
            }
        }

        virtual protected void EncontrarProximoNode()
        {
            // Mostra as opções de escolha para o jogador e espera que ele clique em alguma.
            if (mensageiro == null)
            {
                mensageiro = gameObject;
            }
            // Caso não tenha filho, saia do dialógo.
            if ((dialogoAtual.GetFilhos().Count == 0) || (dialogoAtual.GetFilhos() == null))
            {
                Debug.Log("Nenhum filho encontrado, saindo do dialogo");
                FecharDialogo();
                return;
            }

            // Caso o proximo filho não tenha mensagens, apenas aja.
            if((dialogoAtual.GetFilhos().Count == 1) && (dialogoAtual.GetFilhos()[0].GetNomeEscolha() == "") && (dialogoAtual.GetFilho(0).GetMensagem().Length == 0))
            {
                FecharDialogo();
                dialogoAtual.GetFilho(0).Agir(gameObject);
                return;
            }

            // Se houver apenas um filho e ele não possua uma mensagem de escolha, mostre ele.
            if ((dialogoAtual.GetFilhos().Count == 1) && (dialogoAtual.GetFilhos()[0].GetNomeEscolha() == ""))
            {
                Debug.Log("Só tem um filho sem nome");
                dialogoAtual = dialogoAtual.GetFilhos()[0];
                return;
            }

            if ((dialogoAtual.GetFilhos().Count == 1) && (!dialogoAtual.GetFilhos()[0].checarRequisito()))
            {
                Debug.Log("Só tem um filho, mas seu requisito não foi cumprido");
                FecharDialogo();
                return;
            }


            // Caso nenhuma dessas exceções, mostre os botões e espere.
            Debug.Log("Mostrando botoes");
            fazendoEscolha = true;
            DialogarGUI.d += MudarNodePorFilho;
            for (int i = 0; i < dialogoAtual.GetFilhos().Count; i++)
            {
                Debug.Log("Spawnando botoes");
                // Checa se o requisito desse dialogo foi cumprido, caso não, não o mostre.
                if (dialogoAtual.GetFilho(i).checarRequisito())
                {
                    DialogarGUI.MostrarBotao(dialogoAtual.GetFilho(i).GetNomeEscolha(), i);
                }
            }

            EncontrandoNodePorFilho = true;
        }

        /// <summary>
        /// Muda o atual pelo filho escolhido pelo jogador.
        /// </summary>
        /// <param name="id"></param>
        void MudarNodePorFilho(int id)
        {
            // Agora que o jogador clicou em um butão. Olhe o id do butão em que ele clicou  e qual a mensagem associada.
            EncontrandoNodePorFilho = false;

            if (mensageiro == null)
            {
                mensageiro = gameObject;
            }
            // Tire o delegate para não se repetir quando for o chamar de novo.
            DialogarGUI.d -= MudarNodePorFilho;

            fazendoEscolha = false;
            // Mude o dialogo atual pelo filho com o id do botão.
            dialogoAtual = dialogoAtual.GetFilho(id);

            // Fecha todos os botões.
            DialogarGUI.FecharBotoes();
            // Se não tiver mensagem no dialogo atual, fecha tudo.
            if ((dialogoAtual.GetMensagem() == null) || (dialogoAtual.GetMensagem().Length == 0))
            {
                Debug.Log("Mensagem é vazia, saindo do dialogo");
                FecharDialogo();
                dialogoAtual.Agir(mensageiro);
                return;
            }

            // Chama o  método de ação do dialogo atual, passando o NPC.
            dialogoAtual.Agir(mensageiro);

            // Repete o processo.
            MostrarDialogo();
        }

        /// <summary>
        /// Termina a interação de dialogo.
        /// </summary>
        void FecharDialogo()
        {
            estaDialogando = false;
            DialogarGUI.DialogoFoiEscrito = false;
            DialogarGUI.FecharMensagem();
            Debug.Log("Dialogo fechado");

            INDialogoFinalizado?.Invoke();

        }

        public void AdicionarAcaoAoFimDeDialogo(UnityAction action)
        {
            INDialogoFinalizado.AddListener(action);
        }

        public void MudarDialogo(Dialogo dialogo)
        {
            dialogoAtual = dialogo;
        }

        public void ResetarDialogo()
        {
            dialogoAtual = dialogoPadrao;
            INDialogoFinalizado.RemoveListener(ResetarDialogo);
        }

        public void RetirarINDialogo(UnityAction action)
        {
            INDialogoFinalizado.RemoveListener(action);
        }
    }
}


