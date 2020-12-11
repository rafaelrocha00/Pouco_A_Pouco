using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SistemaDeInventario
{
    public class GUI_Inventario : MonoBehaviour, ISelecionavel
    {
        [SerializeField] TextMeshProUGUI descricao;
        [SerializeField] GameObject botaoBase;
        [SerializeField] GameObject inventarioObjeto;
        [SerializeField] GameObject painel;

        GUI_Butao_Inventario ButaoSelecionado;
        List<GUI_Butao_Inventario> Butoes = new List<GUI_Butao_Inventario>();
        Inventario inventario;

        private void Start()
        {
            inventario = GameManager.instancia.GetInventario();
            inventario.AdicionarINPegarItem(InstanciarItem);
            inventario.AdicionarINPerderItem(RemoverItem);
        }


        public void MudarDescricao(string novaDescricao)
        {
            Debug.Log("Descricao foi chamado");
            descricao.text = novaDescricao;
        }

        public void Selecionar()
        {
            inventarioObjeto.SetActive(true);
           
        }

        public bool Deselecionar()
        {
            inventarioObjeto.SetActive(false);
            return true;
        }

        public void InstanciarItem(Item item)
        {
            GameObject objeto = Instantiate(botaoBase, painel.transform);
            GUI_Butao_Inventario butao = objeto.GetComponent<GUI_Butao_Inventario>();
            butao.nome.text = item.GetNome();
            butao.gui_inven = this;
            butao.itemRelacionado = item;
            Butoes.Add(butao);
        }

        public void RemoverItem(Item item)
        {
            Butoes.Remove(Butoes.Find(x => x.itemRelacionado = item));
        }

    }
}

