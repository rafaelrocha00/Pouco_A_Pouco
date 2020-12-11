using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coffee.UIExtensions;
using System;
using UnityEngine.UI;

public class Transicao : MonoBehaviour
{
    UITransitionEffect efeito;
    bool tocando = false;
    bool fadeOff = false;
    public Action TerminoudeMostrar;
    Image fade;

    //Setagem
    private void Start()
    {
        if(fade == false)
        {
            SetarParametros();
        }
    }
    public void SetarParametros()
    {
        fade = gameObject.GetComponent<Image>();
        efeito = fade.GetComponent<UITransitionEffect>();
        fade.enabled = false;
    }

    private void Update()
    {
        QuestionarTocando();
    }

    void QuestionarTocando()
    {     
        if(tocando && efeito.effectFactor == efeito.duration)
        {
            tocando = false;
            TerminoudeMostrar?.Invoke();
        }
    }
    
   public void TocarTransicao()
   {
        if(fade == null)
        {
            SetarParametros();
        }
        
        tocando = true;
        fade.enabled = true;
        efeito.Show();
   }

    public void FadeOff()
    {
        if(fade == null)
        {
            SetarParametros();
        }
        fade.enabled = true;
        efeito.effectFactor = 1f;
        StopAllCoroutines();
        StartCoroutine(FadeOffAsync());
    }

    IEnumerator FadeOffAsync()
    {
        while(efeito.effectFactor > 0)
        {
            Debug.Log("FadeOff funcionando");
            efeito.effectFactor -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        fade.enabled = false;
        TerminoudeMostrar?.Invoke();
    }
}
