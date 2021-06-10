using _src.Scripts.Controller.Services;
using _src.Scripts.Controller.Systems;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace _src.Scripts.View.DropAndDrag
{
    [RequireComponent(typeof(LayoutElement))]
    [RequireComponent(typeof(RectTransform))]
    public class DraggableItem : DraggableComponent, IPointerClickHandler
    {
        private LayoutElement _layoutElement;
        private RectTransform _rectTransform;
        private Image _image;
        private TextMeshProUGUI _text;
        
        private void Awake()
        {
            _layoutElement = GetComponent<LayoutElement>();
            _rectTransform = GetComponent<RectTransform>();
            _image = GetComponent<Image>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            SetLayoutProperties(true);
            transform.rotation = new Quaternion(0,0,1,10);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            SetLayoutProperties(false);
            SetAnchorPosition();
        }
        
        private void SetLayoutProperties(bool isDragging)
        {
            _layoutElement.ignoreLayout = isDragging;
            _image.raycastTarget = !isDragging;
            _image.maskable = !isDragging;
            _text.maskable = !isDragging;
        }

        private void SetAnchorPosition()
        {
            _rectTransform.anchorMax = Vector2.one;
            _rectTransform.anchorMin = Vector2.zero;
            _rectTransform.pivot = new Vector2(0.5f, 0.5f);
            _rectTransform.anchoredPosition = Vector2.zero;
            _rectTransform.offsetMax = Vector2.zero;
            _rectTransform.offsetMin = Vector2.zero;
            transform.rotation = Quaternion.identity;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                ServiceLocator.Current
                    .GetService<EventManager>()
                    .Publish(PublisherType.RETURN_TO_VARIABLES,this, this.GetComponent<VariableView>());
            }
        }
    }
}
