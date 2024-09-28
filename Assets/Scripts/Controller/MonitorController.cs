using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorController : Singleton<MonitorController>
{
      public bool IsMonitoring;
    // GameObjects que se activarán
    public GameObject[] objectsToActivate;

    void Update()
    {
        // Usamos switch para manejar el estado de IsMonitoring
        switch (IsMonitoring)
        {
            case true:
                ActivateObjects();
                break;
            case false:
                DeactivateObjects();
                break;
        }
    }

    // Activar objetos
    public void ActivateObjects()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)  // Comprobación para evitar errores si alguno es nulo
            {
                obj.SetActive(true);
            }
        }
    }

    // Desactivar objetos
    public void DeactivateObjects()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)  // Comprobación para evitar errores si alguno es nulo
            {
                obj.SetActive(false);
            }
        }
    }

}
