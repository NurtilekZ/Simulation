using UnityEngine;
using UnityEngine.EventSystems;

namespace _src.Scripts.View
{
    public class DropArea : MonoBehaviour, IDropHandler
    {
        [SerializeField] private bool propertySensitive = true;
        [SerializeField] public VariableView variableView;
        [SerializeField] private  UnitView unitView;

        public void OnDrop(PointerEventData eventData)
        {
            if (propertySensitive)
            {
                variableView = eventData.pointerDrag.GetComponent<VariableView>();
                if (unitView.Property != variableView.Property)
                {
                    variableView = null;
                    return;
                }
            }

            AttachItem(eventData.pointerDrag.transform);
        }

        public void AttachItem(Transform eventData)
        {
            eventData.transform.SetParent(transform);
            eventData.position = transform.position;
        }
    }
}
