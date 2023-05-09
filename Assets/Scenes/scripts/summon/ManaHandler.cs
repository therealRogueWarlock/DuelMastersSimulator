using System;
using System.Collections.Generic;
using Scenes.scripts.CardGO;
using UnityEngine;

namespace Scenes.scripts.summon
{
    public class ManaHandler : MonoBehaviour
    {
        private Dictionary<Civilisation, int> _floatingMana;
        private List<Civilisation> _keys;

        private List<CardModel> _tappedCard;


        // Start is called before the first frame update
        void Start()
        {
            _floatingMana = new();
        
            foreach (Civilisation civilisation in Enum.GetValues(typeof(Civilisation)))
            {
                _floatingMana.Add(civilisation,0);
            }
        
            _keys = new List<Civilisation>(_floatingMana.Keys);
            _tappedCard = new();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void HandleCardTap(Tabbable tabbable)
        {
        
            CardModel cardModel = tabbable.gameObject.GetComponent<CardDisplay>().CardModelData;
        
            if (tabbable.isTapped) AddMana(cardModel);
            else RemoveMana(cardModel);

        }
    
    

        private void AddMana(CardModel cardModel)
        {
        
            _tappedCard.Add(cardModel);
            _floatingMana[cardModel.civilization]++;
        
        }

        private void RemoveMana(CardModel cardModel)
        {
            _tappedCard.Remove(cardModel);
            _floatingMana[cardModel.civilization]--;
        
        }
    
        public bool CanPlay(CardModel cardModel)
        {
        
            Debug.Log("Trying to play" + cardModel.name +" " + cardModel.manaCost);
            Debug.Log(string.Join(Environment.NewLine, _floatingMana));
            // if not the right kind of mana floating
            if (_floatingMana[cardModel.civilization] == 0) return false;
            // if not enough mana floatin
            if (SumFloatingMana() < cardModel.manaCost) return false;
            UseMana(cardModel);
            return true;
        }

        private int SumFloatingMana()
        {
            int manaSum = 0;
            foreach (var civilisation in _floatingMana.Keys)
            {
                manaSum += _floatingMana[civilisation];
            }
            return manaSum;
        }
    
        // TODO: needs a fixer
        private void UseMana(CardModel card)
        {
            int manaToRemove = card.manaCost;
        
            while (manaToRemove > 0)
            {
                foreach (var cardModel in _tappedCard.ToArray())
                {
                    if (_floatingMana[cardModel.civilization]==0) continue;
                    _floatingMana[cardModel.civilization]--;
                    cardModel.isUsed = true;
                    manaToRemove--;
                    _tappedCard.Remove(cardModel);
                    if(manaToRemove == 0 ) break;
                }
            }
            Debug.Log(string.Join(Environment.NewLine, _floatingMana));
        }
    }
}
