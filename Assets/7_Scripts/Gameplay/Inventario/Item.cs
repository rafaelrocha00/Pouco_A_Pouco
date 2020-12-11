using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SistemaDeInventario
{
    [CreateAssetMenu(fileName = "Novo Item", menuName = "Item")]
    public class Item: ScriptableObject
    {
        [SerializeField]string nome;
        [SerializeField][TextArea]string descricao;


        public string GetNome()
        {
            return nome;
        }

        public string GetDescricao()
        {
            return descricao;
        }
    }
}

