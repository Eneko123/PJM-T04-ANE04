
using CesiumForUnity;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Location : MonoBehaviour
{
    public TMP_Text LocationText;
    public GameObject player;

    void Start()
    {
        StartCoroutine(InitilicePosition());
    }

    private void Update()
    {
        
    }

    private IEnumerator InitilicePosition()
    {
        // Comprobar si el dispositivo tiene GPS habilitado
        //if (!Input.location.isEnabledByUser)
        //{
        //    LocationText.text = "La localización está desactivada";
        //    Debug.Log("El usuario no ha habilitado la localización");
        //    yield break;
        //}

        // Iniciar el servicio GPS
        Input.location.Start(5f, 5f);

        // Esperar a que se inicialice
        int maxWait = 20;

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Comprobar timeout
        if (maxWait <= 0)
        {
            LocationText.text = "Tiempo de espera agotado";
            Debug.Log("Fuera de tiempo");
            Input.location.Stop();
            yield break;
        }

        // Comprobar errores
        if (Input.location.status != LocationServiceStatus.Running)
        {
            LocationText.text = "No se pudo obtener la localización";
            Debug.Log("Error al iniciar el servicio GPS");
            yield break;
        }

        // Obtener localizacion
        while (Input.location.status == LocationServiceStatus.Running)
        {
            LocationInfo info = Input.location.lastData;

            LocationText.text =
                "Latitud: " + info.latitude +
                "\nLongitud: " + info.longitude +
                "\nAltitud: " + info.altitude +
                "\nPrecisión: " + info.horizontalAccuracy + " m";

            player.GetComponent<CesiumGlobeAnchor>().longitudeLatitudeHeight = new double3(info.latitude, info.longitude, info.altitude);
            yield return new WaitForSeconds(1);
        }
    }

    private void OnDestroy()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            Input.location.Stop();
        }
    }
}
