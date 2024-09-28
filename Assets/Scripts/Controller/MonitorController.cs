using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorController : Singleton<MonitorController>
{
    // Referencia al script HeartRateMonitor para activar/desactivar monitoreo
    public bool IsMonotoring;
    public List<ParticleSystemInfo> particleSystems;

    // Lista de sistemas de partículas que están asociados al estado de monitoreo
    [System.Serializable]
    public class ParticleSystemInfo
    {
        public ParticleSystem particleSystem; // El sistema de partículas en sí
        public bool isMonitoringParticle;     // Indica si el sistema de partículas es parte del monitoreo
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
  void Update()
{
    for (int i = 0; i < particleSystems.Count; i++)
    {
        if (particleSystems[i].particleSystem != null) // Verifica si no es null
        {
            particleSystems[i].particleSystem.gameObject.SetActive(IsMonotoring);
        }
        else
        {
            Debug.LogWarning($"El ParticleSystem en el índice {i} es null.");
        }
    }
}


    // Lista de todos los sistemas de p
}
