using UnityEngine;
using UnityEngine.UI;

public class CrosshairUI : MonoBehaviour
{
    [SerializeField] private Image crosshair; // referencia a la imagen de la mira
    [SerializeField] private thirdPersonShooting aimingScript; // referencia al script de zoom

    void Update()
    {
        if (aimingScript != null && crosshair != null)
        {
            crosshair.enabled = aimingScript.IsAiming;
        }
    }
}
