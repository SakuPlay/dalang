using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sakuplay.Narradho.Views
{
    public class ObjectButton:MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {

        public Action<PointerEventData> MOnBeginDrag;
        public Action MOnEndDrag;
        public Action MOnDrag;
        
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            MOnBeginDrag?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            MOnEndDrag?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            MOnDrag?.Invoke();
        }
    }
}