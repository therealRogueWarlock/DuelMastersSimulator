using Scenes.scripts.Rounds;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Scenes.scripts.CardGO
{
    public class PlayCard : MonoBehaviour
    {
        // NOT USED 
        [SerializeField]
        private UnityEvent onPlayCard;

        
        
        [SerializeField]
        private GameObject dragEffect;
        

        [SerializeField] 
        private PhaseManager phaseManager;
    
        private bool _inSummonCollider;

    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        private void OnTriggerEnter(Collider other)
        {
            
            if (other.tag.Equals("Dropzone"))
            {
                _inSummonCollider = true;
                dragEffect.SetActive(true);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            
            if (other.tag.Equals("Dropzone"))
            {
                _inSummonCollider = false;
                dragEffect.SetActive(false);
            }
        }
        
        void OnMouseUp()
        {
            dragEffect.SetActive(false);
            if (_inSummonCollider)
            {
                onPlayCard.Invoke();
                SummonCard();
            }
        }


        
        private void SummonCard()
        {
           
            if(phaseManager.GetCurrentPhase().Equals(PhaseNames.Charge))
            {
                Debug.Log("play: " + GetComponent<CardDisplay>().CardModelData.name + " As a mana card");
            }
            

        }
        
    
    }
}
