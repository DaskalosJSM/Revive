using UnityEngine;

public class ChecklistController : MonoBehaviour
{
    // Variable booleana para activar o desactivar los objetos hijos
    public bool isChecklistActive = false;

    // Variable para almacenar el estado anterior del booleano
    private bool previousState = false;

    // Referencias a los objetos hijos que se alternarán
    private GameObject[] children;

    private void Start()
    {
        // Buscar todos los hijos del objeto padre y almacenarlos en el array
        Transform[] childTransforms = GetComponentsInChildren<Transform>();

        // Filtrar solo los objetos hijos, excluyendo el objeto padre
        children = new GameObject[childTransforms.Length - 1];
        for (int i = 1; i < childTransforms.Length; i++)
        {
            children[i - 1] = childTransforms[i].gameObject; // Obtener el GameObject del Transform
        }

        // Asegurarse de que haya al menos dos hijos
        if (children.Length < 2)
        {
            Debug.LogWarning("El objeto padre debe tener al menos dos hijos.");
        }

        // Actualizar la visibilidad inicial
        UpdateChecklistVisibility();
    }

    private void Update()
    {
        // Comprobar si el estado del booleano ha cambiado
        if (isChecklistActive != previousState)
        {
            UpdateChecklistVisibility();
            previousState = isChecklistActive; // Actualizar el estado anterior
        }
    }

    // Método para alternar los objetos hijos
    public void ToggleChecklist()
    {
        // Cambiar el valor booleano
        isChecklistActive = !isChecklistActive;

        // No llamamos aquí a UpdateChecklistVisibility, ya que se maneja en Update
    }

    // Método que actualiza la visibilidad de los objetos hijos
    private void UpdateChecklistVisibility()
    {
        if (children.Length >= 2) // Asegurarse de que hay al menos dos hijos
        {
            // Activar o desactivar el primer y segundo hijo
            children[0].SetActive(!isChecklistActive); // Desactivar si el booleano es true
            children[1].SetActive(isChecklistActive);  // Activar si el booleano es true
        }
    }

    // Método que puede ser llamado por eventos (ej. desde un botón)
    public void OnToggleButtonPressed()
    {
        ToggleChecklist();
    }
}
