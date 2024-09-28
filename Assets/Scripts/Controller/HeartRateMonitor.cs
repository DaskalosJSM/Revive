using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class HeartRateMonitor : MonoBehaviour
{
    // Referencia al componente TextMeshPro
    public TextMeshProUGUI heartRateText;

    // Variables para controlar el rango de pulsaciones
    public int minHeartRate = 60; // Límite inferior del rango
    public int maxHeartRate = 100; // Límite superior del rango

    // Duración del cambio de un valor a otro (en segundos)
    public float changeDuration = 5f; // Duración de la interpolación

    // Tiempo entre cada vez que se genera un nuevo valor (en segundos)
    public float changeFrequency = 10f; // Frecuencia de cambio de valor

    // Tiempo de espera antes de que comience la simulación, aleatorio entre 1 y 5 segundos
    public float minStartDelay = 1f;
    public float maxStartDelay = 5f;

    // Bandera para detener y arrancar la simulación desde afuera
    public bool isMonitoring = true;

    // Valor actual del ritmo cardíaco
    private float currentHeartRate;

    // Valor objetivo del ritmo cardíaco
    private float targetHeartRate;
    bool wasMonitoring = false;

    void Update()
{
    bool isMonitoring = MonitorController.Instance.IsMonitoring; // Estado actual del monitoreo

    if (!isMonitoring) // Si no está monitoreando
    {
        heartRateText.text = "--";
        StopAllCoroutines();
        wasMonitoring = false; // Reseteamos el flag cuando deja de monitorear
    }
    else if (isMonitoring && !wasMonitoring) // Solo entra si el estado ha cambiado de no-monitoreo a monitoreo
    {
        wasMonitoring = true; // Actualizamos el flag para reflejar que el monitoreo ha empezado
        Invoke("ReestartMonitor", 0f); // Invocamos la función una sola vez cuando cambia el estado
    }
}

// Reinicia la simulación
void ReestartMonitor()
{
    currentHeartRate = Random.Range(minHeartRate, maxHeartRate + 1);
    heartRateText.text = Mathf.RoundToInt(currentHeartRate).ToString(); // Mostrar este valor inicial
    StartCoroutine(StartMonitoringWithDelay());

}

    // Iniciar la simulación cuando el objeto se habilita
    void OnEnable()
    {
        if (MonitorController.Instance!=null)
        {
            isMonitoring = MonitorController.Instance.IsMonitoring;
        }
        // Inicializar currentHeartRate con un valor aleatorio dentro del rango para evitar la transición desde 0
        currentHeartRate = Random.Range(minHeartRate, maxHeartRate + 1);
        heartRateText.text = Mathf.RoundToInt(currentHeartRate).ToString(); // Mostrar este valor inicial

        StartCoroutine(StartMonitoringWithDelay());
    }

    // Corrutina que inicia el monitoreo con un retardo inicial aleatorio
    IEnumerator StartMonitoringWithDelay()
    {
        // Generar un tiempo de espera aleatorio entre minStartDelay y maxStartDelay
        float startDelay = Random.Range(minStartDelay, maxStartDelay);

        // Esperar el tiempo aleatorio antes de comenzar a monitorear
        yield return new WaitForSeconds(startDelay);

        // Comenzar la simulación solo si la bandera isMonitoring está activa
        if (isMonitoring)
        {
            StartCoroutine(UpdateHeartRate());
        }
    }

    // Corrutina que actualiza el valor del monitor cardíaco de manera aleatoria
    IEnumerator UpdateHeartRate()
    {
        while (isMonitoring) // Mientras la bandera esté en true
        {
            // Guardar el valor actual como punto de partida de la transición
            float startingHeartRate = currentHeartRate;

            // Generar un nuevo valor objetivo dentro del rango
            targetHeartRate = Random.Range(minHeartRate, maxHeartRate + 1);

            // Tiempo transcurrido durante la transición
            float elapsedTime = 0f;

            // Mientras el tiempo transcurrido sea menor a la duración del cambio, seguimos interpolando
            while (elapsedTime < changeDuration)
            {
                // Incrementar el tiempo transcurrido
                elapsedTime += Time.deltaTime;

                // Interpolar suavemente entre el valor inicial y el objetivo
                currentHeartRate = Mathf.Lerp(startingHeartRate, targetHeartRate, elapsedTime / changeDuration);

                // Actualizar el texto en pantalla con el nuevo valor redondeado
                heartRateText.text = Mathf.RoundToInt(currentHeartRate).ToString();

                // Esperar un frame
                yield return null;
            }

            // Asegurarse de que el valor final sea exactamente el objetivo
            currentHeartRate = targetHeartRate;
            heartRateText.text = Mathf.RoundToInt(currentHeartRate).ToString();

            // Esperar la cantidad de tiempo especificada en changeFrequency antes de iniciar un nuevo cambio
            yield return new WaitForSeconds(changeFrequency);
        }
    }

    // Método para ajustar el rango de frecuencia cardíaca dinámicamente (útil para cambiar el estado del paciente)
    public void SetHeartRateRange(int min, int max)
    {
        minHeartRate = min;
        maxHeartRate = max;
    }

    // Método para iniciar o detener la simulación desde afuera
    public void ToggleMonitoring(bool monitorState)
    {
        isMonitoring = monitorState;

        // Si se detiene el monitoreo, poner todos los valores a 0
        if (!isMonitoring)
        {
            // Resetear el valor del ritmo cardíaco a 0
            currentHeartRate = 0;
            targetHeartRate = 0;

            // Actualizar el texto del monitor a 0
            heartRateText.text = "0";
        }
        else
        {
            // Si se reinicia el monitoreo y no se está ejecutando, iniciamos la corrutina
            if (!IsInvoking("UpdateHeartRate"))
            {
                StartCoroutine(UpdateHeartRate());
            }
        }
    }
}
