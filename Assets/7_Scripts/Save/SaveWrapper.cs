using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;


namespace Save
{
  
    [System.Serializable]
    public class SaveWrapper
    {
        [XmlArray("AtributosSalvaveis"), XmlArrayItem(typeof(SaveAtribute), ElementName = "SaveAtribute")]
        public List<SaveAtribute> AtributosSalvaveis = new List<SaveAtribute>();

        public float GetInformacao(string nome)
        {
            if(AtributosSalvaveis == null)
            {
                Debug.Log("Atributos salvaveis é nulo");
            }
            SaveAtribute sa = AtributosSalvaveis.Find(x => x.nome == nome);
            if (sa != null)
            {
                return sa.valor;
            }

            return -1;
        }

        public void SetInformacao(string nome, float valor)
        {
            SaveAtribute s = new SaveAtribute();
            s.nome = nome;
            s.valor = valor;
            AtributosSalvaveis.Add(s);
        }

        public SaveWrapper()
        {
        }


    }
}

