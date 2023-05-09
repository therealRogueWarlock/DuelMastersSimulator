using Scenes.scripts.CardGO;
using UnityEngine;

namespace Scenes.scripts.Animation
{
    public class AnimationHandler : MonoBehaviour
    {

        private Animator _animator;
        private static readonly int OnPlayCard = Animator.StringToHash("OnPlayCard");
        private static readonly int OnLose = Animator.StringToHash("OnLose");

        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();
        }
        
        public void OnCardPlayListener(Component sender, CardDisplay data)
        {
            if(sender.gameObject.layer != gameObject.layer) return;

            _animator.SetTrigger(OnPlayCard);
        }
    
        public void OnLoseGame(Component sender, object data)
        {
            if(sender.gameObject.layer != gameObject.layer) return;

            _animator.SetTrigger(OnLose);
        }
    
    }
}
