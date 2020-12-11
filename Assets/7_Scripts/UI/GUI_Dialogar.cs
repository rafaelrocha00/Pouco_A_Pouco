using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SistemaDeDialogos
{
    public class GUI_Dialogar : MonoBehaviour
    {
        string mensagemAtual;
        [SerializeField] float velocidadeDeEscrita;
        [HideInInspector] public bool DialogoFoiEscrito = false;

        [SerializeField] [Tooltip("Onde escrever o dialogo?")] TextMeshProUGUI texto;
        [SerializeField] [Tooltip("Texto quendo não há mensageiro")] TextMeshProUGUI textoSemMensageiro;
        [SerializeField] [Tooltip("Quem está falando?")] TextMeshProUGUI Mensageiro;
        [SerializeField] [Tooltip("Caixa de texto")] GameObject painelDeTexto;
        [SerializeField] [Tooltip("O painel de escolhas, será o pai de todas elas.")] GameObject painelDeEscolhas;
        [SerializeField] [Tooltip("O botão de escolha, precisa ter um componente de botão.")] GameObject BotaoDeEscolhas;

        [SerializeField] List<GameObject> imagensPersonagensEsquerda = new List<GameObject>();
        [SerializeField] List<GameObject> imagensPersonagensDireita = new List<GameObject>();

        GameObject imagemEsquerdaAtual;
        GameObject imagemDireitaAtual;
        GameObject imagemAtual;

        List<Sprite> expressoes = new List<Sprite>();

        // O delegate que será passado ao botão.
        public delegate void BotaoDelegate(int id);
        public BotaoDelegate d;

        Dialogar dialogoAtual;

        // Random para sons.
        [SerializeField] public AudioClip[] Types;
        [SerializeField] public AudioSource Som;

        public int IndexSom;
        public int IndexAnterior;

        public float PitchMin = 0.8f;
        public float PitchMax = 1.2f;
        public float VolumeMin = 0.8f;
        public float VolumeMax = 1f;

        bool temMensageiro = true;

        /// <summary>
        /// Desenha uma string na tela. Letra por letra.
        /// </summary>
        /// <param name="mensagem"></param>
        public void MostrarMensagem(string mensagem)
        {
            // Apagando todos os mostrar mensagem.
            StopAllCoroutines();

            // Começando do inicio.
            DialogoFoiEscrito = false;

            // Encontrando Tags especiais na mensagem.
            mensagem = EncontrarTags(mensagem);


            // Limpa painel.
            texto.text = "";
            textoSemMensageiro.text = "";

            // A mensagem é formada por um mensageiro e a mensagem em si. aqui ele separa os dois. EX: Noah;É um dia quente hoje.
            // Isso permite que uma mesma array de strings tenha uma conversa inteira, mas sem escolhas.
            string[] mensagemSeparada = mensagem.Split(';');
            if (mensagemSeparada.Length > 1)
            {
                // Ativa o painel de dialogo.
                if (painelDeTexto.activeInHierarchy == false)
                {
                    painelDeTexto.SetActive(true);
                }

                Mensageiro.text = mensagemSeparada[0];
                int sprite = int.Parse(mensagemSeparada[1]);
                int posicao = int.Parse(mensagemSeparada[2]);
                int expressao = int.Parse(mensagemSeparada[3]);
                mensagemAtual = mensagemSeparada[4];

                // Atribuindo imagens especificas para cada NPC importante. Isso pode ser cortado.
                if (posicao == 0)
                {
                    if (imagemEsquerdaAtual != null && imagemEsquerdaAtual != imagensPersonagensEsquerda[sprite])
                    {
                        imagemEsquerdaAtual.SetActive(false);
                    }

                    imagensPersonagensEsquerda[sprite].SetActive(true);
                    imagemEsquerdaAtual = imagensPersonagensEsquerda[sprite];
                    imagemAtual = imagemEsquerdaAtual;
                }
                if (posicao == 1)
                {
                    if (imagemDireitaAtual != null && imagemDireitaAtual != imagensPersonagensDireita[sprite])
                    {
                        imagemDireitaAtual.SetActive(false);
                    }

                    imagensPersonagensDireita[sprite].SetActive(true);
                    imagemDireitaAtual = imagensPersonagensDireita[sprite];
                    imagemAtual = imagemDireitaAtual;
                }
                if (posicao == -1)
                {
                    Debug.LogWarning("Nenhuma posição estabelecida");
                    imagemDireitaAtual.SetActive(false);
                }

                if (imagemEsquerdaAtual == null)
                {
                    imagensPersonagensEsquerda[0].SetActive(true);
                    imagemEsquerdaAtual = imagensPersonagensEsquerda[0];
                }

                temMensageiro = true;
                StartCoroutine(DesenharMensagem(texto));


            }
            else
            {
                // Caso não tenha mensageiro, só mostra a mensagem.
                temMensageiro = false;
                mensagemAtual = mensagem;
                StartCoroutine(DesenharMensagem(textoSemMensageiro));

            }

            // Começa a coroutine.
        }

        public string EncontrarTags(string mensagem)
        {
            // Encontra tags e aplica efeitos. Isso tudo pode ser cortado e substituido para cada projeto diferente.
            string tag = GameManager.instancia.ProcurarTags(mensagem);
            if (tag != "")
                mensagem = mensagem.Replace(tag, "");
            return mensagem;
        }

        /// <summary>
        /// Fecha o painel de mensagem.
        /// </summary>
        public void FecharMensagem()
        {
            StopAllCoroutines();
            texto.text = "";
            textoSemMensageiro.text = "";
            if (Mensageiro != null)
            {
                Mensageiro.text = "";

            }
            if (imagemEsquerdaAtual != null)
                imagemEsquerdaAtual.SetActive(false);
            if (imagemDireitaAtual != null)
                imagemDireitaAtual.SetActive(false);
            imagemEsquerdaAtual = null;
            imagemDireitaAtual = null;
            mensagemAtual = null;
            painelDeTexto.SetActive(false);
            dialogoAtual = null;
        }


        /// <summary>
        /// Desenha a mensagem, letra por letra.
        /// </summary>
        /// <returns></returns>
        IEnumerator DesenharMensagem(TextMeshProUGUI texto)
        {
            foreach (char letra in mensagemAtual.ToCharArray())
            {
                texto.text += letra;
                MostrarSom();
                yield return new WaitForSeconds(velocidadeDeEscrita * Time.deltaTime);
            }
            DialogoFoiEscrito = true;
            yield break;
        }

        public void MostrarMensagemInstantaniamente()
        {

            StopAllCoroutines();
            if (temMensageiro)
            {
                texto.text = mensagemAtual;

            }
            else
            {
                textoSemMensageiro.text = mensagemAtual;
            }
            DialogoFoiEscrito = true;
        }

        /// <summary>
        /// Mostra um botão de escolha. O dialogar usa um for para cada escolha.
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="id"></param>
        public void MostrarBotao(string nome, int id)
        {
            if (painelDeEscolhas.activeInHierarchy == false)
            {
                painelDeEscolhas.SetActive(true);
            }
            GameObject botaoGrafico = Instantiate(BotaoDeEscolhas, painelDeEscolhas.transform);
            Button botao = botaoGrafico.GetComponent<Button>();
            botao.onClick.AddListener(delegate { d(id); });
            TextMeshProUGUI texto = botaoGrafico.GetComponentInChildren<TextMeshProUGUI>();
            texto.text = nome;
        }

        /// <summary>
        /// Fecha os botões de escolha.
        /// </summary>
        public void FecharBotoes()
        {
            painelDeEscolhas.SetActive(false);

            for (int i = 0; i < painelDeEscolhas.transform.childCount; i++)
            {
                Destroy(painelDeEscolhas.transform.GetChild(i).gameObject);
            }

        }

        /// <summary>
        /// Mostra os sons para cada letra escrita. Com um pouco de randomização.
        /// </summary>
        public void MostrarSom()
        {
            IndexSom = Random.Range(0, Types.Length);

            while (IndexSom == IndexAnterior)
            {
                IndexSom = Random.Range(0, Types.Length);

            }

            Som.PlayOneShot(Types[IndexSom]);
            IndexAnterior = IndexSom;
        }

        public GameObject GetSpritePersonagemAtual()
        {
            return imagemAtual;
        }

        public Dialogar GetDialogoAtual()
        {
            return dialogoAtual;

        }

        public void SetDialogoAtual(Dialogar dialogo)
        {
            dialogoAtual = dialogo;
        }


    }
}

