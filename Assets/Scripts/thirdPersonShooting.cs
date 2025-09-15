using Unity.Cinemachine;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class thirdPersonShooting : MonoBehaviour
{

    [SerializeField] private CinemachineCamera vCam;
    [SerializeField] private float normalVision = 60f;
    [SerializeField] private float zoomVision = 40f;
    [SerializeField] private float zoomLerp = 10f;
    [SerializeField] private float shootRange = 100f;
    private bool isAiming = false;
    public bool IsAiming => isAiming; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) isAiming = true;
        if (Input.GetMouseButtonUp(1)) isAiming = false;
        float targetFOV = isAiming ? zoomVision : normalVision;
        vCam.Lens.FieldOfView = Mathf.Lerp(vCam.Lens.FieldOfView,targetFOV,Time.deltaTime * zoomLerp);

    }
}
