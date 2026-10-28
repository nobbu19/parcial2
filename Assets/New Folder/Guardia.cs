using UnityEngine;
using UnityEngine.SceneManagement;

public class Guardia : MonoBehaviour
{
    public Transform[] puntos;          
    public Transform jugador;           
    public float velocidad = 2f;       
    public float rangoVision = 5f;      
    public float anguloVision = 60f;    

    int indice = 0;                     

    void Update()
    {
      
        Patrullar();

        if (VeAlJugador())
        {
          
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void Patrullar()
    {
        
        Transform destino = puntos[indice];
        Vector2 direccion = destino.position - transform.position;

       
        
        if (direccion.magnitude < 0.1f)
        {
            indice = (indice + 1) % puntos.Length;
            return;
        }

        
        transform.up = direccion;

        // se mueve hacia el destino
        transform.position += (Vector3)(direccion.normalized * velocidad * Time.deltaTime);
    }

    bool VeAlJugador()
    {
        //   el jugador debe estar dentro del rango de visión
        Vector2 haciaJugador = jugador.position - transform.position;


        //   el jugador debe estar dentro del cono
         
        return Vector2.Angle(transform.up, haciaJugador) < anguloVision / 2f;
    }

    // Solo sirve para ver el cono  
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        // Calcula los dos bordes del cono rotando la dirección "arriba" a cada lado
        Vector3 izq = Quaternion.Euler(0, 0, anguloVision / 2f) * transform.up;
        Vector3 der = Quaternion.Euler(0, 0, -anguloVision / 2f) * transform.up;

        // Dibuja las dos líneas
        Gizmos.DrawLine(transform.position, transform.position + izq * rangoVision);
        Gizmos.DrawLine(transform.position, transform.position + der * rangoVision);
    }
}