using System;
using System.Collections.Generic;
using UnityEngine;


namespace SistemaDeInventario
{
    public class Inventario : MonoBehaviour
    {
        [SerializeField]List<Item> itens = new List<Item>();
        Action<Item> ganhouItem;
        Action<Item> perdeuItem;

        public void AdicionarItem(Item item)
        {
            itens.Add(item);
            ganhouItem?.Invoke(item);
        }

        public void RetirarItem(Item item)
        {
            itens.Remove(item);
            perdeuItem?.Invoke(item);
        }

        public bool ChecarItem(string nome)
        {
            Debug.Log("Checando item");
            return itens.Exists(x => x.GetNome() == nome);
        }

        public void AdicionarINPegarItem(Action<Item> action)
        {
            ganhouItem += action;
        }

        public void AdicionarINPerderItem(Action<Item> action)
        {
            perdeuItem += action;
        }
    }

}
