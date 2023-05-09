using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;


namespace Scenes.scripts.Database
{
    
    public class CardDatabase
    {

        private Dictionary<Civilisation, List<CardData>> _civilisations;


        public CardDatabase()
        {
            
            Debug.Log("CardDatabase constructor");
            _civilisations = new Dictionary<Civilisation, List<CardData>>();
            
            LoadCivilisation(Civilisation.Darkness);
            LoadCivilisation(Civilisation.Nature);
            LoadCivilisation(Civilisation.Light);
            LoadCivilisation(Civilisation.Fire);
        }


        public List<CardData> GetCivilisation(Civilisation civilisation)
        {
            return _civilisations[civilisation];
        }
        
        private void LoadCivilisation(Civilisation civilisation)
        {
            using (StreamReader r =
                   new StreamReader("D:/unity/TheReturnOfTheRealDuelMaster/Assets/Scenes/scripts/Database/" +
                                    civilisation + ".txt"))
            {
                string json = r.ReadToEnd();
                SerializableList<CardData> cardsSerializable =
                    JsonUtility.FromJson<SerializableList<CardData>>(json);

                _civilisations.Add(civilisation, cardsSerializable.list);
            }
            Debug.Log(civilisation + " Loaded");
        }
    }
    
    [Serializable]
    public class CardData
    {
        public string cardName;
        public string cardType;
        public string cardText;
        public string[] ability;
        public string illustrator;
        public int mana;
        public int manaCost;
        public int power;
        public string race;
        public string civilisation;
        public string rarity;
    }
    
    // used to deserialize json list
    [Serializable]
    public class SerializableList<T> {
        public List<T> list;

        public SerializableList()
        {
            list = new List<T>();
        }

    }

}
