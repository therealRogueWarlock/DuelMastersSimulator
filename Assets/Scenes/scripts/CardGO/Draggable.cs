using Scenes.Events.CardEvents;
using UnityEngine;

namespace Scenes.scripts.CardGO
{
    public class Draggable : MonoBehaviour
    {

        private Transform _faceThis;
        
        [SerializeField] 
        private CardGameEvent onCardDrop;
        
        // target when stop dragging
        public Quaternion targetRotation;
        public Vector3 targetPosition;

        private Transform _transformStateBeforeDrag;
        
        // dragging 
        [SerializeField]
        private float dragTiltAmount = 150;
        
        [SerializeField]
        public float distanceToCard = 0.5f;

        public bool isVertical;
        
        // Select and state bull shit
        public bool isDisableDrag;
        public bool beingDragged;
        
        private void Awake()
        {
            //isVertical = true;
            targetRotation = Quaternion.Euler(0f, 0f, 0f);
        }

        // Start is called before the first frame update
        void Start()
        {
            _faceThis = Camera.main.transform;
        }

        private void Update()
        {
            if (!beingDragged)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 8.0f);
                transform.position = Vector3.Slerp(transform.position, targetPosition, Time.deltaTime * 8.0f);
            }
        }
        
        
        private Vector3 GetMouseWorldPosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
        
        private void OnMouseDrag()
        {
            if (isDisableDrag) return;
            
            
            beingDragged = true;
            DragAnimation();
        }


        private void OnMouseDown()
        {
            //_distanceToCard = Vector3.Distance(Camera.main.transform.position, transform.position);
        }
        

        private void DragAnimation()
        {
            
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            
            Vector3 draggablePositioning = mouseRay.origin + (mouseRay.direction * distanceToCard);
            
            Debug.DrawLine(mouseRay.origin, draggablePositioning,Color.blue,2);
            
            
            Quaternion toRotation = Quaternion.Euler(((draggablePositioning.z-transform.position.z)*dragTiltAmount),0, -((draggablePositioning.x-transform.position.x)*dragTiltAmount));
            if (isVertical) toRotation = Quaternion.Euler(((draggablePositioning.y-transform.position.y)*dragTiltAmount),-(draggablePositioning.x-transform.position.x)*dragTiltAmount,0);

            toRotation *= targetRotation;
           
            //transform.rotation = Quaternion.LookRotation(_faceThis.transform.position - transform.position, new Vector3(1, 0, 0)) * toRotation;
            transform.rotation = Quaternion.Lerp(transform.rotation,toRotation,Time.deltaTime*15.0f);
            transform.position = Vector3.Lerp(transform.position, draggablePositioning, Time.deltaTime * 15.0f);

        }
        
        
        void OnMouseUp()
        {
            if(isDisableDrag) return;
            beingDragged = false;
            onCardDrop.InvokeGameEvent(this,GetComponent<CardDisplay>());
        }
    }
}
