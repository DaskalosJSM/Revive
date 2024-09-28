using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float velocidad = 5f;
    public float desplazamientoMaximo = 10f;
    [SerializeField] private float limite;
    private float posicionInicialY;
    public bool invertirMovimiento = false;
    private bool enContactoConSuction = false;

    void Start()
    {
        posicionInicialY = transform.position.y;

        if (limite == 0)
        {
            limite = desplazamientoMaximo;
        }
    }

    void Update()
    {    
        MoverObjeto();
    }

    void MoverObjeto()
    {
        float desplazamiento = velocidad * Time.deltaTime;

        if (invertirMovimiento || enContactoConSuction)
        {
            desplazamiento = -desplazamiento;
        }

        float nuevaPosicionY = transform.position.y + desplazamiento;

        if (Mathf.Abs(nuevaPosicionY - posicionInicialY) <= limite)
        {
            transform.Translate(0, desplazamiento, 0);
        }
        else
        {
            float limitePosicion = posicionInicialY + Mathf.Sign(desplazamiento) * limite;
            transform.position = new Vector3(transform.position.x, limitePosicion, transform.position.z);
        }
    }

    // Detectar la colisión con el objeto que tiene el tag "suction"
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colisión detectada con un objeto" + other.name); // Log para la detección de colisión

        if (other.CompareTag("suction"))
        {
            Debug.Log("Colisión detectada con un objeto con el tag 'suction'."); // Log para la detección de colisión
            CambiarDireccion(true);
        }
    }

    // Cuando salga del área de colisión con el objeto "suction"
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Colisión detectada con un objeto" + other.name);
        if (other.CompareTag("suction"))
        {
            Debug.Log("El objeto ha dejado de colisionar con el objeto con el tag 'suction'."); // Log para detectar cuándo sale de colisión
            CambiarDireccion(false);
        }
    }

    void CambiarDireccion(bool enContacto)
    {
        enContactoConSuction = enContacto;
    }
}
