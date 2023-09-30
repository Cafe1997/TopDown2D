using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoDisparador : MonoBehaviour
{
    public Transform jugador; // El objeto del jugador al que apuntaremos.

    public GameObject balaPrefab; // El prefab de la bala que disparará el enemigo.
    public float velocidadBala = 10f; // La velocidad de la bala.
    public float frecuenciaDisparo = 3f; // La frecuencia de disparo en segundos.
    private float tiempoUltimoDisparo; // El tiempo en el que se realizó el último disparo.

    public int puntosDeVida = 100; // Puntos de vida del enemigo.
    public int puntosDeVidaPorNuevoPunto = 25; // Cantidad de puntos de vida para agregar un nuevo punto de disparo.

    private Transform[] puntosDeDisparo; // Los puntos de disparo del enemigo.
    private int puntoActual = 0; // El punto de disparo actual.(Contador de puntos de disparo).

    void Start()
    {
        tiempoUltimoDisparo = Time.time; // Inicializamos el tiempo del último disparo.

        // Crear un punto de disparo inicial.
        puntosDeDisparo = new Transform[1];
        puntosDeDisparo[0] = transform; // Punto de disparo original (la posición del enemigo).
    }

    void Update()
    {
        // Comprobamos si ha pasado suficiente tiempo desde el último disparo.
        if (Time.time - tiempoUltimoDisparo >= frecuenciaDisparo)
        {
            // Creamos balas en todos los puntos de disparo.
            for (int i = 0; i < puntosDeDisparo.Length; i++)
            {
                // Calculamos la dirección hacia el jugador desde cada punto de disparo.
                Vector3 direccionAlJugador = (jugador.position - puntosDeDisparo[i].position).normalized;

                // Creamos la bala en el punto de disparo actual.
                GameObject bala = Instantiate(balaPrefab, puntosDeDisparo[i].position, Quaternion.identity);

                // Aplicamos velocidad a la bala.
                Rigidbody2D rb = bala.GetComponent<Rigidbody2D>();
                rb.velocity = direccionAlJugador * velocidadBala;
            }

            // Actualizamos el tiempo del último disparo.
            tiempoUltimoDisparo = Time.time;
        }
    }

    // Este método permite restar puntos de vida al enemigo.
    public void RecibirDanio(int cantidadDanio)
    {
        puntosDeVida -= cantidadDanio;

        // Si los puntos de vida llegan a cero o menos, puedes destruir al enemigo o realizar otras acciones.
        if (puntosDeVida <= 0)
        {
            // Por ejemplo, puedes destruir al enemigo aquí.
            Destroy(gameObject);
        }
    }

    // Agrega un nuevo punto de disparo al enemigo.
    private void AgregarNuevoPuntoDeDisparo()
    {
        Transform[] nuevosPuntos = new Transform[puntosDeDisparo.Length + 1];
        for (int i = 0; i < puntosDeDisparo.Length; i++)
        {
            nuevosPuntos[i] = puntosDeDisparo[i];
        }

        // Crea un nuevo punto de disparo en una posición aleatoria alrededor del enemigo.
        nuevosPuntos[puntosDeDisparo.Length] = new GameObject("PuntoDeDisparo" + (puntosDeDisparo.Length + 1)).transform;
        nuevosPuntos[puntosDeDisparo.Length].parent = transform;
        Vector2 offset = Random.insideUnitCircle.normalized * 2; // Genera una posición aleatoria en un radio de 2 unidades.
        nuevosPuntos[puntosDeDisparo.Length].localPosition = new Vector3(offset.x, offset.y, 0f);

        puntosDeDisparo = nuevosPuntos;
    }
}
