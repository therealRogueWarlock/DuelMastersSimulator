using System.Collections.Generic;
using Scenes.scripts.CardGO;
using UnityEngine;

namespace Scenes.scripts
{
    public class DraggablePositionManager : MonoBehaviour
    {
        [SerializeField] private GameObject draggablePrefab; 
    
        private float _draggableWidth;
        private float _draggableHeight;
        private float _draggableLength;
        private float _transformWidth;
        private Vector3 _startPosition;
    
        private List<Draggable> _draggablesList;
        [SerializeField] private Vector3 rotationOffSet;
        [SerializeField] private int draggablePerRow = 10;
        [SerializeField] private bool vertical;

        [SerializeField] private float overlapTopDecimal = 1;
        [SerializeField] private float overlapSideDecimal = 1;
        [SerializeField] private float hoverDecimal = 1;

        [SerializeField] private bool stack;
        //[SerializeField] private Transform targetTransform = null;
        private Transform targetTransform;

        // some testing
        private Vector3 _targetTransformCenter;
        void Start()
        { 
            Renderer prefabRender = draggablePrefab.GetComponent<Renderer>();
        
            _draggableWidth = prefabRender.bounds.size.x;
            _draggableLength = prefabRender.bounds.size.z;
            _draggableHeight = prefabRender.bounds.size.y;
        
            _draggablesList = new List<Draggable>();

            targetTransform = transform.transform;

            _targetTransformCenter = targetTransform.position;
        
            _transformWidth = GetComponent<Renderer>().bounds.size.x;

            _startPosition = _targetTransformCenter - new Vector3((_transformWidth / 2) - (_draggableWidth / 2),0,0);;

        }

        // Update is called once per frameasfdasd
        void Update()
        {
       
        }

        private Vector3 GetHorizontalPosition(int colNumber, int rowNumber, int heightNumber)
        {
            return new Vector3(_draggableWidth*overlapSideDecimal*colNumber,_draggableHeight*hoverDecimal*heightNumber,-(_draggableLength*overlapTopDecimal)*rowNumber);
        }
    
        private Vector3 GetVerticalPosition(int colNumber, int rowNumber, int heightNumber)
        {
            return new Vector3(_draggableWidth*overlapSideDecimal*colNumber,-(_draggableLength*overlapTopDecimal)*rowNumber,_draggableHeight*hoverDecimal*heightNumber);
        }
    
    

    
        private Vector3 SpaceAroundPosition()
        {
            float rowNumber = 10 % _draggablesList.Count;
            return transform.position - new Vector3((_transformWidth / _draggablesList.Count)-(_draggableWidth/2), _draggableHeight*rowNumber,-(_draggableLength*0.2f)*rowNumber);
        }


        public void AddDraggable(Draggable newDraggable)
        {
            _targetTransformCenter = targetTransform.position;
            _startPosition = _targetTransformCenter - new Vector3((_transformWidth / 2) - (_draggableWidth / 2), 0, 0);
            ;
        
            _draggablesList.Add(newDraggable);
            int rowNumber = (_draggablesList.Count - 1) / draggablePerRow;
            int colNumber = (_draggablesList.Count - 1) % draggablePerRow;
            int heightNumber = rowNumber;
            if (stack)
            {
                rowNumber = 0;
                colNumber = 0;
                heightNumber = _draggablesList.Count;
            }
        
        
        
            Vector3 cardOffSet = GetHorizontalPosition(colNumber, rowNumber, heightNumber);

            float xRotation = targetTransform.rotation.eulerAngles.x;
            float yRotation = targetTransform.rotation.eulerAngles.y;
            float zRotation = targetTransform.rotation.eulerAngles.z;

            Quaternion newRotation = Quaternion.Euler(xRotation, yRotation, zRotation);

            if (vertical)
            {
                cardOffSet = GetVerticalPosition(colNumber, rowNumber, heightNumber);
                //newRotation = Quaternion.Euler(spawnPoint.rotation.eulerAngles.x,draggable.targetRotation.y,draggable.targetRotation.z);
            }

            newDraggable.targetRotation = newRotation;
            newDraggable.targetPosition = _startPosition + cardOffSet;
        }

        public void ResetAndDestroy()
        {
            foreach (var draggable in _draggablesList)
            {
                Destroy(draggable.gameObject);
            }
        
            _draggablesList = new List<Draggable>();
        }

    
    
        public void RemoveDraggable(Draggable draggableToRemove)
        {
            _draggablesList.Remove(draggableToRemove);
            RepositionAll();
        }



        public void RepositionAll()
        {
            int rowNumber = 0;
            int colNumber = 0;
            int heightNumber = 0;
        
            // realign all the left
            foreach (var draggable in _draggablesList)
            {
            
                Vector3 cardOffSet = GetHorizontalPosition(colNumber, rowNumber, heightNumber );

                float xRotation = targetTransform.rotation.eulerAngles.x;
                float yRotation = targetTransform.rotation.eulerAngles.y;
                float zRotation = targetTransform.rotation.eulerAngles.z;

                Quaternion newRotation = Quaternion.Euler(xRotation,yRotation, zRotation);
            
                if (vertical)
                {
                    cardOffSet = GetVerticalPosition(colNumber,rowNumber, heightNumber);
                    //newRotation = Quaternion.Euler(spawnPoint.rotation.eulerAngles.x,draggable.targetRotation.y,draggable.targetRotation.z);
                }

                draggable.targetRotation = newRotation;
                draggable.targetPosition = _startPosition + cardOffSet;
            
                if(stack)
                {
                    heightNumber++;continue;
                }
            
                colNumber++;
                if (colNumber >= draggablePerRow)
                {
                    rowNumber++;
                    heightNumber++;
                    colNumber = 0;
                }
            }
        }
    
        public void MoveAllToTransform(Transform target)
        {
            foreach (var draggable in _draggablesList)
            {
                draggable.targetPosition = target.position;
                draggable.targetRotation = target.rotation;
            }
        }

        public List<Draggable> GetDraggables()
        {
            return _draggablesList;
        }


    }
}
