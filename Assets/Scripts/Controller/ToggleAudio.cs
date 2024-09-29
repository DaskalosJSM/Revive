using UnityEngine;

public class ToggleAudio : MonoBehaviour
{
    // Referencia al componente AudioSource
    private AudioSource audioSource;

    // Bandera para verificar si el sonido está activo o no
    private bool isPlaying = false;

    void Start()
    {
        // Obtenemos el componente AudioSource del GameObject
        audioSource = GetComponent<AudioSource>();
    }

    // Esta función activa o desactiva el sonido
    public void ToggleSound()
    {
        if (isPlaying)
        {
            // Si el audio está reproduciéndose, lo detenemos
            audioSource.Pause();
            isPlaying = false;
        }
        else
        {
            // Si el audio está pausado, lo reproducimos
            audioSource.Play();
            isPlaying = true;
        }
    }
}
