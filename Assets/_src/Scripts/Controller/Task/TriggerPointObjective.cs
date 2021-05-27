using _src.Scripts.Controller.Interactables;
using UnityEngine;

namespace _src.Scripts.Controller.Task
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(Collider))]
    public class TriggerPointObjective : Objective
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Interactable>() == null) return;
            other.gameObject.SetActive(false);
            CompleteObjective(this);
        }

        public override void CompleteObjective(object param = default)
        {
            base.CompleteObjective(param);
            ChangeMaterial();
        }

        private void ChangeMaterial()
        {
            MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            _meshRenderer.GetPropertyBlock(materialPropertyBlock);
            materialPropertyBlock.SetColor("_TintColor",Color.green);
            _meshRenderer.SetPropertyBlock(materialPropertyBlock);
        }
    }
}
