using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_NPC : MonoBehaviour
{
    NPC npc;
    [SerializeField] GameObject PrefabHud_NPC;
    [SerializeField] GameObject canvas;
    [SerializeField] float offsetVertical;
    [SerializeField] Vector3 offsetVerticalOpacidade;
    List<Image> imagens;
    GameObject Hud_NPC;
    Slider medoSlider;
    [SerializeField] float distanciaMetadeOpacidade;
    [SerializeField] float distanciaZeroOpacidade;
    [SerializeField] Color distanciaFull;
    [SerializeField] Color distanciaMetade;
    [SerializeField] Color distanciaZero;
    [SerializeField] Color corVitoria;
    [SerializeField] bool jogadorVenceu = false;
    float valorAnterior;


    private void Start()
    {
        SetarHUD();
        npc.AdicionarAvitoria(SetarVitoria);
    }

    private void Update()
    {
        if(Hud_NPC != null  && !jogadorVenceu)
        {
            SetarValoresHud();
        }

        SetarPosicaoHUD();

        if(npc.gameObject == null)
        {
            Destroy(this.gameObject);
        }

    }

    public void SetarHUD()
    {
        npc = GetComponent<NPC>();
        canvas = GUI_Referenciador.instancia.gameObject;
        Hud_NPC = Instantiate(PrefabHud_NPC, canvas.transform);
        HUD_NPCreferenciador refe = Hud_NPC.GetComponent<HUD_NPCreferenciador>();
        imagens = refe.imagens;
        medoSlider = refe.slider;
        medoSlider.maxValue = npc.GetMedoMaximo();
      
    }

    public void SetarPosicaoHUD()
    {
        Hud_NPC.transform.position = Camera.main.WorldToScreenPoint(npc.gameObject.transform.position + new Vector3(0, offsetVertical, 0));
    }

    public void SetarValoresHud()
    {
        medoSlider.value = npc.GetMedo();
        float distancia = Vector3.Distance(Input.mousePosition, Camera.main.WorldToScreenPoint(npc.gameObject.transform.position + offsetVerticalOpacidade));
        if (distancia <= distanciaMetadeOpacidade)
        {
            for (int i = 0; i < imagens.Count; i++)
            {
                imagens[i].color = distanciaFull;
            }
        }

        if (distancia >= distanciaMetadeOpacidade)
        {
            for (int i = 0; i < imagens.Count; i++)
            {
                imagens[i].color = distanciaMetade;
            }
        }

        if (distancia >= distanciaZeroOpacidade)
        {
            for (int i = 0; i < imagens.Count; i++)
            {
                imagens[i].color = distanciaZero;
            }
        }
    }

    public void SetarVitoria()
    {
        medoSlider.value = medoSlider.maxValue;
        for (int i = 0; i < imagens.Count; i++)
        {
            imagens[i].color = corVitoria;
        }

        jogadorVenceu = true;
    }


}
