using System.Collections.Generic;
using Scenes.scripts.CardGO;
using UnityEngine;

namespace Scenes.scripts.Database
{
    public class DeckBuilder : MonoBehaviour
    {
    
        [SerializeField] private GameObject cardPrefab;
        private DraggablePositionManager _draggablePositionManager;
        private CardDatabaseManager _cardDatabaseManager;
        // Start is called before the first frame update
        void Start()
        {
            _cardDatabaseManager= CardDatabaseManager.GetInstance();
            _draggablePositionManager = GetComponent<DraggablePositionManager>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    

        public void DisplayDarkness(Transform spawnPoint)
        {
            DisplayCivilisation(spawnPoint,Civilisation.Darkness);
        }
    
        public void DisplayLight(Transform spawnPoint)
        {
            DisplayCivilisation(spawnPoint,Civilisation.Light);
        }

        public void DisplayFire(Transform spawnPoint)
        {
            DisplayCivilisation(spawnPoint,Civilisation.Fire);
        }
        public void DisplayNature(Transform spawnPoint)
        {
            DisplayCivilisation(spawnPoint,Civilisation.Nature);
        }
        public void DisplayWater(Transform spawnPoint)
        {
            Debug.Log("Display Water");
        
        }
    
        public void DisplayCivilisation(Transform spawnPoint, Civilisation civilisation)
        {
            _draggablePositionManager.ResetAndDestroy();
            var cardModelDataList = _cardDatabaseManager.GetCardModels(civilisation);

            DeployCardPrefabs(cardModelDataList,spawnPoint);
        }
    


        public void DeployCardPrefabs(List<CardModel> cardModelDataList, Transform spawnPoint)
        {
            foreach (var cardModelData in cardModelDataList)
            {
                GameObject cardObject = Instantiate(cardPrefab,
                    spawnPoint.position, spawnPoint.rotation);

                CardDisplay cardDisplay = cardObject.GetComponent<CardDisplay>();
                cardDisplay.CardModelData = cardModelData;

                Draggable draggable = cardDisplay.gameObject.GetComponent<Draggable>();
                draggable.isVertical = true;
                _draggablePositionManager.AddDraggable(draggable);
            }
        }

    }
}
