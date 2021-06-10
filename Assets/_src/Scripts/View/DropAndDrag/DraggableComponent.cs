using UnityEngine;
using UnityEngine.EventSystems;

namespace _src.Scripts.View.DropAndDrag
{
    public abstract class DraggableComponent : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler
    {
        public virtual void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }
        
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
        }
        public virtual void OnEndDrag(PointerEventData eventData)
        {
        }
    }
}