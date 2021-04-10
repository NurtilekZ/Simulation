using UnityEngine;

public class AssignToObject : MonoBehaviour
{
    public Transform target;
    public Camera cameraCache;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraCache = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraCache.WorldToScreenPoint(target.position + new Vector3(0,1,0));
    }

    private void OnValidate()
    {
        if (target != null)
        {
            transform.position = cameraCache.WorldToScreenPoint(target.position + new Vector3(0,1,0));
        }
    }
}
