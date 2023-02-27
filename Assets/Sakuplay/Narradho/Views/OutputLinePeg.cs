using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sakuplay.Narradho.Views
{
    public class OutputLinePeg:MonoBehaviour,IPointerDownHandler,IDragHandler,IPointerUpHandler
    {
        [SerializeField] private Canvas canvasContainer;
        [SerializeField] private Line linePrefab;

        public List<InputLinePeg> Connections = new List<InputLinePeg>();
        private List<Line> draggingLines = new List<Line>();
        
        private Vector2 m_InitialMousePosition;
        private Line currentDraggingLine;


        public void OnPointerDown(PointerEventData eventData)
        {
            currentDraggingLine = Instantiate(linePrefab, this.transform);
            currentDraggingLine.gameObject.SetActive(true);
            currentDraggingLine.transform.localPosition = Vector3.zero;
        
        
            var mousePosition = EventSystem.current.currentInputModule.input.mousePosition;

            var scaleFactor = canvasContainer.scaleFactor;
            var transformedY = mousePosition.y / scaleFactor;
            var transformedX = mousePosition.x / scaleFactor;


            m_InitialMousePosition = new Vector2(transformedX, transformedY);
        }

        public void OnDrag(PointerEventData eventData)
        {
            var mousePosition = EventSystem.current.currentInputModule.input.mousePosition;

            var scaleFactor = canvasContainer.scaleFactor;
            var transformedY = mousePosition.y / scaleFactor;
            var transformedX = mousePosition.x / scaleFactor;
        
            var length = Vector2.Distance(m_InitialMousePosition, new Vector2(transformedX, transformedY));
            var rotation = Mathf.Atan2(transformedY - m_InitialMousePosition.y, transformedX - m_InitialMousePosition.x) * Mathf.Rad2Deg;
        
            currentDraggingLine.SetLength(length - 1f);
            currentDraggingLine.SetRotation(rotation);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            InputLinePeg inputLinePeg = null;
            var inputLine= eventData.hovered.FirstOrDefault(x => inputLinePeg = x.GetComponent<InputLinePeg>());
            if (inputLinePeg != null)
            {
                if (AllowToConnect(inputLinePeg) && inputLinePeg.AllowToConnect(this))
                {
                    Connect(inputLinePeg);
                    inputLinePeg.Connect(this);

                    currentDraggingLine.startingAnchor = this.transform;
                    currentDraggingLine.endingAnchor = inputLinePeg.transform;

                    draggingLines.Add(currentDraggingLine);
                }
            }
            else
            {
                Destroy(currentDraggingLine.gameObject);
            }
        }

        public bool AllowToConnect(InputLinePeg inputPeg)
        {
            return !Connections.Contains(inputPeg);
        }
        
        public void Connect(InputLinePeg inputPeg)
        {
            if(AllowToConnect(inputPeg)) Connections.Add(inputPeg);
        }

        public void UpdateLines()
        {
            for (int i = 0; i < draggingLines.Count; i++)
            {
                draggingLines[i].UpdateAnchoring(canvasContainer.scaleFactor);
            }
        }

    }
}