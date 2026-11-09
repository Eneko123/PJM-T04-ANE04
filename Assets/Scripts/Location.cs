using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;
using System.Collections;

public class Location : MonoBehaviour
{
    public Vector3 position;

    public TMP_Text LocationText;

    private void Start()
    {
        Input.location.Start(5f, 5f);
    }

    void Update()
    {
        StartCoroutine(UpdateLocation());
    }

    private IEnumerator UpdateLocation()
    {
        int maxWait = 10;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;  
        }

        if (maxWait < 1)
        {
            Debug.Log("Fuera de tiempo");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("No se pudo determinar la localizacion");
            yield break;
        }

        LocationInfo info = Input.location.lastData;

        LocationText.text = info.ToString();
    }
}
