using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class C_Animacao : MonoBehaviour, ITagable
{
    GameObject npcAtual;

    public void SetNpcAtual(GameObject novoNPC)
    {
        npcAtual = novoNPC;
    }

    public void SetAnimacaoBool(int nome)
    {
        npcAtual.GetComponent<Animator>().SetBool(nome, true);
    }

    public string AcharTags(string mensagem)
    {
        string PadraoDarSkill = "<SetAnim<[0-9]>>";
        Match match = Regex.Match(mensagem, PadraoDarSkill);
        if (match.Success)
        {
            SetAnimacaoBool(int.Parse(Regex.Match(match.Value, @"\d+").Value));
            return match.Value;
        }
        return "";
    }
}

