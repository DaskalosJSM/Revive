using UnityEngine;
using UnityEngine.Video; // Necesario para manejar el VideoPlayer
using System;
using Unity.VisualScripting;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Asigna el VideoPlayer desde el inspector
    public double triggerTime = 10.0;
    public double triggerTime2 = 10.0;
    public double triggerTime3 = 10.0;
    public double triggerTime4 = 10.0;

    private bool eventTriggered = false;
    private bool eventTriggered2 = false;
    private bool eventTriggered3 = false;
    private bool eventTriggered4 = false;

    public GameObject[] checklist;

    void Start()
    {
        // Asigna el método al evento de bucle del video
        videoPlayer.loopPointReached += OnVideoLoop;
    }

    void Update()
    {
        // Lógica de control de tiempo y eventos
        if (videoPlayer.time >= triggerTime && !eventTriggered)
        {
            TriggerEvent1();
        }
        if (videoPlayer.time <= triggerTime)
        {
            checklist[0].GetComponent<ChecklistController>().isChecklistActive = false;
            eventTriggered = false;
        }

        if (videoPlayer.time >= triggerTime2 && !eventTriggered2)
        {
            TriggerEvent2();
        }
        if (videoPlayer.time <= triggerTime2)
        {
            checklist[1].GetComponent<ChecklistController>().isChecklistActive = false;
            eventTriggered2 = false;
        }

        if (videoPlayer.time >= triggerTime3 && !eventTriggered3)
        {
            TriggerEvent3();
        }
        if (videoPlayer.time <= triggerTime3)
        {
            checklist[2].GetComponent<ChecklistController>().isChecklistActive = false;
             eventTriggered3 = false;
        }

        if (videoPlayer.time >= triggerTime4 && !eventTriggered4)
        {
            TriggerEvent4();
        }
        if (videoPlayer.time <= triggerTime4)
        {
            checklist[3].GetComponent<ChecklistController>().isChecklistActive = false;
             eventTriggered4 = false;
        }
    }

    void TriggerEvent1()
    {
        eventTriggered = true; // Marca como disparado para que no se repita
        activarItem(checklist[0]);  // Dispara el evento
        Debug.Log("Evento disparado en tiempo: " + triggerTime + " segundos.");
    }

    void TriggerEvent2()
    {
        eventTriggered2 = true; // Marca como disparado para que no se repita
        activarItem(checklist[1]);  // Dispara el evento
        Debug.Log("Evento disparado en tiempo: " + triggerTime2 + " segundos.");
    }

    void TriggerEvent3()
    {
        eventTriggered3 = true; // Marca como disparado para que no se repita
        activarItem(checklist[2]);  // Dispara el evento
        Debug.Log("Evento disparado en tiempo: " + triggerTime3 + " segundos.");
    }

    void TriggerEvent4()
    {
        eventTriggered4 = true; // Marca como disparado para que no se repita
        activarItem(checklist[3]);  // Dispara el evento
        Debug.Log("Evento disparado en tiempo: " + triggerTime4 + " segundos.");
    }

    void activarItem(GameObject item)
    {
        item.GetComponent<ChecklistController>().ToggleChecklist();
    }

    // Método que se llama cuando el video entra en bucle
    void OnVideoLoop(VideoPlayer vp)
    {
        Debug.Log("El video ha entrado en bucle, reiniciando checklist y eventos.");

        // Restablecer los eventos
        eventTriggered = false;
        eventTriggered2 = false;
        eventTriggered3 = false;
        eventTriggered4 = false;

        // Reiniciar el checklist
        ReestartChecklist();
    }

    void ReestartChecklist()
    {
        for (int i = 0; i < checklist.Length; i++)
        {
            checklist[i].GetComponent<ChecklistController>().ToggleChecklist();
        }
    }
}
