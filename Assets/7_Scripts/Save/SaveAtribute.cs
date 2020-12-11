using System.Xml.Serialization;

namespace Save
{
    [XmlRoot("SaveWrapper")]
    [System.Serializable]
    public class SaveAtribute
    {
        [XmlElement]
        public string nome;
        [XmlElement]
        public float valor;



        public SaveAtribute()
        {

        }
    }
}


