using Coffee.UIExtensions;
using UnityEngine;
using SistemaDeDialogos;

public class NPCSprite : MonoBehaviour
{
    bool eSprite = false;
    GUI_Dialogar ui;
    [SerializeField]GameObject imagemAtual;
    [SerializeField] Color corEscura;
    UIEffect efeito;

    private void Start()
    {
        ui = GUI_Referenciador.instancia.GetComponent<GUI_Dialogar>();
        if(imagemAtual == null)
        {
            imagemAtual = gameObject;
            eSprite = true;
        }
        efeito = GetComponent<UIEffect>();
    }

    private void Update()
    {
        Observar();
    }

    void Observar()
    {
        if(ui.GetSpritePersonagemAtual() != imagemAtual)
        {
            efeito.effectColor = corEscura;
            ///efeito.effectFactor = 0.5f;
            if(eSprite)
            gameObject.transform.localScale = new Vector3(0.95f,0.95f,0.95f);
        }
        else
        {
            efeito.effectColor = Color.white;
            //efeito.effectFactor = 0f;
            if(eSprite)
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

        }
    }
}
