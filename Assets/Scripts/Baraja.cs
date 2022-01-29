﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baraja : MonoBehaviour
{

    public static Baraja instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private struct infocarta
    {
        public int puntos;
        public string descripcion;
    }
    //template de las cartas
    public GameObject carta;
    private GameObject cartaFacil, cartaDificil;
    private bool seleccionFacil = false, seleccionDificil = false;
    //una lista con todas las cartas faciles o dificiles que pueden haber
    //pila de cartas faciles que pueden sallir durante la partida
    //pila de cartas dificiles que pueden salir durante la partida
    List<infocarta> faciles, dificiles;
    Stack<infocarta>cartasFaciles, cartasDificiles;


    //baraja tood el cotarro
    private void Barajar()
    {
        Shuffle(faciles);
        Shuffle(dificiles);
        //copiar las cartas a las que van a aparecerr durante el juego
        cartasFaciles = new Stack<infocarta>(faciles);
        cartasDificiles = new Stack<infocarta>(dificiles);
        //chimpón
    }

    private void Shuffle(List<infocarta> cards)
    {
        System.Random random = new System.Random();

        for (int i = 0; i < cards.Count; i++)
        {
            int j = random.Next(i, cards.Count);
            infocarta temporary = cards[i];
            cards[i] = cards[j];
            cards[j] = temporary;
        }
    }



    //This method returns the game object that was clicked using Raycast 2D
    private void mouseHovering()
    {
        //Converting $$anonymous$$ouse Pos to 2D (vector2) World Pos
        Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

        if (hit)
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.gameObject.name == cartaFacil.name)
            {
                Debug.Log("estoy en la carta facil");
                seleccionFacil = true;
                seleccionDificil = false;
            }
            else if (hit.transform.gameObject.name == cartaDificil.name)
            {
                Debug.Log("estoy en la carta dificil");
                seleccionDificil = true;
                seleccionFacil = false;
            }
            else
            {
                seleccionFacil = seleccionDificil = false;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        faciles = new List<infocarta>();
        dificiles = new List<infocarta>();
        cartasFaciles = new Stack<infocarta>();
        cartasDificiles = new Stack<infocarta>();


        //  1 ---> cuando empiza necesito leer las cartas que estan activas de algun lado
        // de momento ni puta idea asi que me voya  inventar 3 caartas de cada tipo para dar un poco variedad
        //yo que se imaginenese el txt mas bonico del mundo

        //las estoy poniendo aqui a piñonazo, me estoy imaginando el formato obviamente susceptible a cambios
        
        infocarta provisional;
        provisional = new infocarta();
        provisional.puntos = 2;
        provisional.descripcion = "esta es la carta facil numero 1";

        //push la carta
        faciles.Add(provisional);

        provisional = new infocarta();
        provisional.puntos = 3;
        provisional.descripcion = "esta es la carta facil numero 2";

        //push la carta
        faciles.Add(provisional);

        provisional = new infocarta();
        provisional.puntos = 4;
        provisional.descripcion = "esta es la carta facil numero 3";

        //Add la carta
        faciles.Add(provisional);


        //ahora las dificiles
        provisional = new infocarta();
        provisional.puntos = 6;
        provisional.descripcion = "esta es la carta dificil numero 1";

        //Add la carta
        dificiles.Add(provisional);

        provisional = new infocarta();
        provisional.puntos = 12;
        provisional.descripcion = "esta es la carta dificil numero 2";

        //Add la carta
        dificiles.Add(provisional);




        // 2 ---> las cartas pueden ser de dos tipos, onomatopeya o gestos, facil o dificil

        // 3 ---> las cartas pueden estar activas o desactivadas
        //SI ESTAN DESACCTIVADAS NUNCA SE METEN ES INFORMACION DE LEER!

        // 4 ---> idem con la custom


        //scramble la baraja como lo hacemos
        //funcion que baraja todas las carta leidas
        Barajar();


        //copiar las cartas a las que se van a mostrar, la pila
        //al final de leer debera de aparecer primera carta de todas de la carta de frio caliente



        //mostrar dos cartas
        //cartaFacil =  //cartasFaciles.Peek();

        Debug.Log("la primera carta de las faciles es" + cartasFaciles.Peek().descripcion);

        Debug.Log("la primera carta de las faciles es" + cartasDificiles.Peek().descripcion);

    }

    // Update is called once per frame
    void Update()
    {
        //comprobar si elige la opcion facil o dificil
        //no tengo ni idea de seleecion de esto con mando lo ahre solo apra raton luego me checkean

        //si se esta usando raton, si no pos se selecciona con izquierda o derecha en el mando, o de igual modo con las teclas en teclado
        //a lo mejor no queremos usar el raton ni pa pipas
        //mouseHovering();

        //confirmar eleccion
        //click, o espacio


        //actualizar la pila de las cartas que ya han salido
        //si se vacia volver a barajar


        //devolverr la eleccion

    }
}
