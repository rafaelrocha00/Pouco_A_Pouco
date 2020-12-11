using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(fileName = "Nova habilidade", menuName = "Habilidade")]
public class Habilidade : ScriptableObject
{
    [SerializeField] int index;
    [SerializeField] Texture2D CursorAssociado;
    [SerializeField] string nome;
    [SerializeField] string descricao;
    [SerializeField] int custo;


    public void agir()
    {

    }

    public string GetNome()
    {
        return nome;
    }

    public string GetDescricao()
    {
        return descricao;
    }

    public int GetCusto()
    {
        return custo;
    }

    public int GetIndex()
    {
        return index;
    }

    public Texture2D GetCursorAssociado()
    {
        return CursorAssociado;
    }
}
