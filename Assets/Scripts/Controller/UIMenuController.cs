using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuController : MonoBehaviour
{
    // Arreglo de GameObjects (los paneles)
    public GameObject[] panels;

    // Variable que indica el índice del panel que debe activarse
    public int currentPanelIndex;

    // Índice del panel de carga dentro del arreglo de panels
    public int loadingPanelIndex;

    void Start()
    {
        // Llama a la función para activar el panel inicial
        ChangePanel(currentPanelIndex);
    }

    // Función que activa el panel correspondiente y desactiva los demás
    public void ChangePanel(int panelIndex)
    {
        // Asegura que el índice esté dentro de los límites del arreglo
        if (panelIndex < 0 || panelIndex >= panels.Length)
        {
            Debug.Log("Índice de panel fuera de rango");
            return;
        }

        // Activa y desactiva los paneles según el índice
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == panelIndex);
        }
    }

    // Función para salir de la aplicación
    public void QuitApplication()
    {
        Application.Quit();
    }

    // Función para cargar una escena asíncronamente usando el índice del SceneManager
    public void LoadSceneAsync(int sceneIndex)
    {
        // Asegúrate de que el índice de la escena esté dentro del rango de las escenas del Build Settings
        if (sceneIndex < 0 || sceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("Índice de escena fuera de rango");
            return;
        }

        // Activar el panel de carga
        ChangePanel(loadingPanelIndex);

        // Iniciar la carga de la escena de manera asíncrona
        StartCoroutine(LoadSceneCoroutine(sceneIndex));
    }

    // Corrutina que carga la escena asíncronamente y cambia a ella una vez que se complete
    private IEnumerator LoadSceneCoroutine(int sceneIndex)
    {
        // Comienza la carga de la escena asíncronamente
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        // Evita que la nueva escena se active inmediatamente
        asyncLoad.allowSceneActivation = false;

        // Espera hasta que la escena haya terminado de cargarse
        while (!asyncLoad.isDone)
        {
            // Si la carga está completa (progreso llega a 0.9), activamos la escena
            if (asyncLoad.progress >= 0.9f)
            {
                // Aquí podrías mostrar algún mensaje de "presionar botón para continuar" si deseas
                // Pero en este caso, activamos la escena automáticamente.
                asyncLoad.allowSceneActivation = true;
            }

            // Continuar esperando hasta que la escena se haya activado
            yield return null;
        }
    }
}
