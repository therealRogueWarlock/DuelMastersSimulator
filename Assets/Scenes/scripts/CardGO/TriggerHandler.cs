using UnityEngine;

namespace Scenes.scripts.CardGO
{
    public class TriggerHandler : MonoBehaviour
    {


        private CardModel _cardModelData;

        // Start is called before the first frame update
    
        void Start()
        {
            _cardModelData = GetComponent<CardDisplay>().CardModelData;
        }

        // Update is called once per frame
        void Update()
        {
        
        }



        public void OnSummon()
        {
            print("On summon "+ _cardModelData.name);
            print("cost "+ _cardModelData.manaCost);

        }


        public void OnEnterBattleZone()
        {
            print("On enter battle zone");
        }
    
    
    }
}
