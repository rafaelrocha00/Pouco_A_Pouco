using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;


namespace Save
{
    public class C_Save : MonoBehaviour
    {
        [SerializeField] string pastaSave;
        [SerializeField] string nomeSave;
        Action jogoSalvo;
        Action DocumentoCarregado;
        [SerializeField] SaveWrapper save;

        public void Finalizar()
        {
            Salvar(save);
        }

        public void Inicializar()
        {
            save = Carregar<SaveWrapper>();
        }

        public float Carregar(string nome)
        {
            if(save == null)
            {
                Inicializar();
                Debug.Log("Save é nulo");
            }
            return save.GetInformacao(nome);
        }

        public void Salvar(string nome, float valor)
        {
            if(save == null)
            {
                save = new SaveWrapper();
            }
            Debug.Log("Salvando info" + nome);
            save.SetInformacao(nome, valor);
        }

        public void Salvar(string nome, Vector3 vetor)
        {
            Salvar(nome + "X", vetor.x);
            Salvar(nome + "Y", vetor.y);
            Salvar(nome + "Z", vetor.z);
        }

        public void Salvar(string nome,Quaternion quaternion)
        {
            Salvar(nome + "X", quaternion.x);
            Salvar(nome + "Y", quaternion.y);
            Salvar(nome + "Z", quaternion.z);
            Salvar(nome + "W", quaternion.w);
        }

        public Vector3 Carregar(string nome, Vector3 vetor)
        {
           vetor.x = Carregar(nome + "X");
           vetor.y = Carregar(nome + "Y");
           vetor.z = Carregar(nome + "Z");
            return vetor;
        }

        public Quaternion Carregar(string nome, Quaternion quaternion)
        {
          quaternion.x =  Carregar(nome + "X");
          quaternion.y = Carregar(nome + "Y");
          quaternion.z = Carregar(nome + "Z");
          quaternion.w = Carregar(nome + "W");

            return quaternion;
        }


        void Salvar<T>(T objeto)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            if (!Directory.Exists(Application.dataPath + "/" + pastaSave))
            {
                Directory.CreateDirectory(Application.dataPath + "/" + pastaSave);
            }
            FileStream stream = new FileStream(Application.dataPath + "/" + pastaSave + "/" + nomeSave + ".xml", FileMode.Create);
            serializer.Serialize(stream, objeto);
            stream.Close();

            Debug.Log("Save completo em: " + Application.dataPath + "/" + pastaSave + "/" + nomeSave + ".xml");
            jogoSalvo?.Invoke();
        }

        T Carregar<T>()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            FileStream stream = new FileStream(Application.dataPath + "/" + pastaSave + "/" + nomeSave + ".xml", FileMode.Open);
            return (T)serializer.Deserialize(stream);
          
        }





    }

  
}


