using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFitter : MonoBehaviour
{
    [SerializeField] private float margin = 0.5f;   // aire alrededor del campo

    private Camera cam;
    private int lastW, lastH;

    void Awake()
    {
        cam = GetComponent<Camera>();
        Fit();
    }

    void Update()
    {
        // Por si rota la pantalla o cambia la ventana del Simulator
        if (Screen.width != lastW || Screen.height != lastH) Fit();
    }

    private void Fit()
    {
        lastW = Screen.width;
        lastH = Screen.height;
    }
}