using UnityEngine;
using UnityEngine.EventSystems;

namespace _src.Scripts.View
{
    public class DraggableWindow : DraggableComponent
    {
        public override void OnDrag(PointerEventData eventData)
        {
            transform.position += (Vector3) eventData.delta;
        }
    }
}