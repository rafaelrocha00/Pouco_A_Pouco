using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SistemaDeInventario
{
    public class GUI_Butao_Inventario : MonoBehaviour
    {
        [HideInInspector] public GUI_Inventario gui_inven;
        [HideInInspector] public Item itemRelacionado;
        public TextMeshProUGUI nome;
        public Button butao;

        private void Start()
        {
            butao.onClick.AddListener(Selecionar);
        }

        public void Selecionar()
        {
            Debug.Log("Selecionar foi chamado " + itemRelacionado.name);
            gui_inven.MudarDescricao(itemRelacionado.GetDescricao());
        }
    }

}
