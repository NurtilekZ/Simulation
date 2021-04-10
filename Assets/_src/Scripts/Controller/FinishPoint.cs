using System.Windows.Input;
using UnityEngine;

namespace _src.Scripts.Controller
{
    public class FinishPoint : MonoBehaviour
    {
        public MeshRenderer meshRenderer;
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<ICommand>() != null)
            {
                MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
                meshRenderer.GetPropertyBlock(materialPropertyBlock);
                materialPropertyBlock.SetColor("_TintColor",Color.green);
                meshRenderer.SetPropertyBlock(materialPropertyBlock);
            }
        }
    }
}
