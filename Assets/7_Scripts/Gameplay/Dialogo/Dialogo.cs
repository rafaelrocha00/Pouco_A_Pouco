using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SistemaDeDialogos
{
    [SerializeField]
    [CreateAssetMenu(fileName = "Novo Dialogo", menuName = "Dialogo/Dialogo")]
    public class Dialogo : ScriptableObject
    {
        [SerializeField] [Tooltip("A mensagem a ser dita.")] string[] mensagem;
        [SerializeField] [Tooltip("Caso haja uma escolha que levou até aqui.")] string nomeEscolha;
        [SerializeField] [Tooltip("Os filhos desse dialogo")] List<Dialogo> filhos;


        /// <summary>
        /// Pode mostrar dialogo?
        /// </summary>
        /// <returns></returns>
        public virtual bool checarRequisito()
        {
            return true;
        }

        /// <summary>
        /// Age no NPC.
        /// </summary>
        public virtual void Agir(GameObject NPC)
        {

        }



        public string GetMensagem(int index)
        {
            return mensagem[index];
        }

        public string[] GetMensagem()
        {
            return mensagem;
        }


        public int GetTamanhoMensagem()
        {
            return mensagem.Length;
        }

        public List<Dialogo> GetFilhos()
        {
            return filhos;
        }

        public Dialogo GetFilho(int index)
        {
            return filhos[index];
        }

        public string GetNomeEscolha()
        {
            return nomeEscolha;
        }
    }
}


