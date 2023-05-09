using System;
using System.Collections.Generic;
using System.IO;
using Scenes.scripts.CardGO;
using UnityEngine;

namespace Scenes.scripts.Database
{
    
    public class CardDatabaseManager
    {
        private static CardDatabaseManager _instance;
            
        private CardDatabase _cardDatabase;
        
        private CardDatabaseManager()
        {
            Debug.Log("CardDatabase manager start");
            _cardDatabase = new CardDatabase();
        }
        
        
        public List<CardModel> GetCardModels(Civilisation civilisation)
        {
            List<CardModel> cards = new List<CardModel>();

            _cardDatabase.GetCivilisation(civilisation).ForEach(data =>
                {
                    CardModel scriptableObjectCardModel = GenerateCardModel(data);
                    cards.Add(scriptableObjectCardModel);
                }
            );
            
            return cards;
        }

        
        
        public List<CardModel> GetTestDeck()
        {
            
            List<CardModel> cards = new List<CardModel>();
            
            _cardDatabase.GetCivilisation(Civilisation.Darkness).ForEach(data =>
                {
                    CardModel scriptableObjectCardModel = GenerateCardModel(data);
                    cards.Add(scriptableObjectCardModel);
                }
            );
            
            _cardDatabase.GetCivilisation(Civilisation.Nature).ForEach(data =>
                {
                    CardModel scriptableObjectCardModel = GenerateCardModel(data);
                    cards.Add(scriptableObjectCardModel);
                }
            );
            
            return cards;
        }


        private CardModel GenerateCardModel(CardData cardData)
        {
            
            CardModel scriptableObjectCardModel = ScriptableObject.CreateInstance<CardModel>();
                    
            scriptableObjectCardModel.name = cardData.cardName;
            scriptableObjectCardModel.description = cardData.cardText;
                    
            Texture2D imageTexture = new Texture2D(2, 2);

            string imagePath = "D:/unity/TheReturnOfTheRealDuelMaster/Assets/Scenes/scripts/Database/DM_01_base_set/"+cardData.civilisation+"/" + cardData.cardName + ".jpg";
                    
            if (File.Exists(imagePath))
            {
                byte[] fileData = File.ReadAllBytes(imagePath);
                imageTexture.LoadImage(fileData);
            }else{Debug.Log(imagePath);}
                    
            scriptableObjectCardModel.cardImage = imageTexture;
            scriptableObjectCardModel.manaCost = cardData.manaCost;
            scriptableObjectCardModel.civilization = Enum.Parse<Civilisation>(cardData.civilisation,true);
            scriptableObjectCardModel.power = cardData.power;
            scriptableObjectCardModel.type = cardData.cardType;
            scriptableObjectCardModel.cardAbilities = cardData.ability;
            
            return scriptableObjectCardModel;
        }
        

        public static CardDatabaseManager GetInstance()
        {
            return _instance ??= new CardDatabaseManager();
        }

       
    }
}
