using UnityEngine;

namespace Scenes.scripts.CardGO
{
    [CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
    public class CardModel : ScriptableObject
    {
        public new string name;
        public string description;

        public Texture cardImage;
        public int manaCost;
        public Civilisation civilization;
        public int power;
        public string type;
        public bool summoningSickness = true;
        public bool isUsed;
        public bool isShield;
        public string[] cardAbilities;
        
    }
}
