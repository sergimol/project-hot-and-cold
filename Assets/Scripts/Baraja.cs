﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class infocarta
{
    public bool active;
    public int puntos;
    public string descripcion;
    public bool descubierta;
    public bool eliminar;

    public infocarta(bool activa, int puntaje, string info, bool dec)
    {
        active = activa;
        puntos = puntaje;
        descripcion = info;
        descubierta = dec;
        eliminar = false;
    }
    public infocarta() {
        eliminar = false;
    }
}

public class Baraja : MonoBehaviour
{

    public static Baraja instance;

    string idioma = "Spain";
    //template de las cartas
    [SerializeField]
    private GameObject cartaVoz, cartaGesto, cartaMenu;
    [SerializeField]
    private Sprite voz1, voz2, voz3, gestos1, gestos2, gestos3;
    private bool seleccionFacil = false, seleccionDificil = false;
    //una lista con todas las cartas faciles o dificiles que pueden haber
    //pila de cartas faciles que pueden sallir durante la partida
    //pila de cartas dificiles que pueden salir durante la partida
    List<infocarta> faciles, dificiles, facilesDefault, dificilesDefault, facilesCustom, dificilesCustom;
    Stack<infocarta> cartasFaciles, cartasDificiles;

    infocarta cartaSeleccion;



    string path = ""; //pos pa ir probando
    string persistentPath = ""; //el que ahi que poner en releasse o no funciona //TODO

    void Awake()
    {
        if (instance == null)
        {



            instance = this;
            DontDestroyOnLoad(this.gameObject);

            setpaths();

            faciles = new List<infocarta>();
            dificiles = new List<infocarta>();
            facilesDefault = new List<infocarta>();
            facilesCustom = new List<infocarta>();
            dificilesCustom = new List<infocarta>();
            dificilesDefault = new List<infocarta>();
            cartasFaciles = new Stack<infocarta>();
            cartasDificiles = new Stack<infocarta>();
                

            //Debug.Log(persistentPath);

            facilesDefault = FileHandler.ReadListFromJSON<infocarta>("cartasFaciles" + idioma + ".json");
            dificilesDefault = FileHandler.ReadListFromJSON<infocarta>("cartasDificiles" + idioma + ".json");


            facilesCustom = FileHandler.ReadListFromJSON<infocarta>("cartasFacilesCustom" + idioma + ".json");
            dificilesCustom = FileHandler.ReadListFromJSON<infocarta>("cartasDificilesCustom" + idioma + ".json");

            if (facilesDefault.Count == 0)
                crearCartas();

        }
        else
        {
            Destroy(this.gameObject);
        }



    }

    private infocarta creadnoCarta(int p, string d)
    {
        infocarta aux = new infocarta();

        aux.active = true;
        aux.puntos = p;
        aux.descripcion = d;
        aux.descubierta = false;

        return aux;
    }

    private void crearCartas()
    {


        dificilesDefault.Add(creadnoCarta(3,
            "¡Levantate cuando se acerque, sientate cuando se aleje, NO HABLES!"));
        dificilesDefault.Add(creadnoCarta(1, "¡Da palmadas, mas rapido cuanto mas cerca esta!"));
        dificilesDefault.Add(creadnoCarta(2, "Alejate de tu amigo si esta lejos y acercate si esta cerca"));
        dificilesDefault.Add(creadnoCarta(3, "Indica solo con tus ojos donde tiene que ir"));
        dificilesDefault.Add(creadnoCarta(2, "Inclinate a la derecha si esta cerca, a la izquierda si se aleja"));
        dificilesDefault.Add(creadnoCarta(2, "Actua como un controlador aereo, el de los palitos"));
        dificilesDefault.Add(creadnoCarta(3, "Aguanta la respiracion"));
        dificilesDefault.Add(creadnoCarta(1, "Frota algo!"));
        dificilesDefault.Add(creadnoCarta(1, "Te estas quedando dormido"));
        dificilesDefault.Add(creadnoCarta(2, "Haz cosquillas"));
        dificilesDefault.Add(creadnoCarta(1, "Si se acerca muevete inquieto en la silla"));
        dificilesDefault.Add(creadnoCarta(2, "Cuanto mas cerca mirale mas fijamente, mas intenso!"));
        dificilesDefault.Add(creadnoCarta(3, "Ponte muy triste"));


        //las faciles

        facilesDefault.Add(creadnoCarta(1, "Di frio frio cuando se aleje y caliente caliente al acercarse"));
        facilesDefault.Add(creadnoCarta(2, "Imita a un animal cuando se acerque y otro cuando se aleje!"));
        facilesDefault.Add(creadnoCarta(2, "Solo onomatopeyas!"));
        facilesDefault.Add(creadnoCarta(3, "Di colores contrarios al objeto"));
        facilesDefault.Add(creadnoCarta(1, "SEMAFORO! Verde cuando este cerca, rojo cuando se aleje"));
        facilesDefault.Add(creadnoCarta(1, "Pon acento gallego!"));
        facilesDefault.Add(creadnoCarta(2, "Di nombres de objetos cercanos al objetivo"));
        facilesDefault.Add(creadnoCarta(3, "A las seis en punto! da la direccion como las manillas de un reloj!"));

        facilesDefault.Add(creadnoCarta(3, "Di la receta de unas lentejas de super puta madre"));
        facilesDefault.Add(creadnoCarta(3, "solo puedes decir la letra \"A\" "));

        facilesDefault.Add(creadnoCarta(1, "Habla en Brasileiro!"));
        facilesDefault.Add(creadnoCarta(1, "Encara Messi! Encara Messi! Se un comentarista de futbol!"));

        facilesDefault.Add(creadnoCarta(2, "Canta let it go! (Copyright disclaimer)"));
        facilesDefault.Add(creadnoCarta(2, "Canta algo del conejo malo! (Copyright disclaimer)"));
        facilesDefault.Add(creadnoCarta(2, "El vecino con el taladro"));
        facilesDefault.Add(creadnoCarta(1, "Grita!"));
        facilesDefault.Add(creadnoCarta(3, "Riete segun se aleje"));
        facilesDefault.Add(creadnoCarta(3, "Di frutas al alejarse, di postres al acercarse"));
        facilesDefault.Add(creadnoCarta(2, "habla como Mickey al acercarse, como goofy al alejarse"));
        facilesDefault.Add(creadnoCarta(2, "habla como el pato Donald"));
        facilesDefault.Add(creadnoCarta(1, "Habla lento"));
        facilesDefault.Add(creadnoCarta(2, "Beep Boop! convierte en robot habla con 0 y 1"));

        facilesDefault.Add(creadnoCarta(3, "Lanza piropos"));
        facilesDefault.Add(creadnoCarta(2, "Silba, si no sabes pos te las arreglas"));
        facilesDefault.Add(creadnoCarta(3, "Finge que te has olvidado"));
        facilesDefault.Add(creadnoCarta(3, "insulta, no temas por tu amistad"));
        facilesDefault.Add(creadnoCarta(2, "Tirate pedos"));
        facilesDefault.Add(creadnoCarta(3, "Barras, Barras, Rapea"));


        ActualizarSave();

    }

   
    //metódo para setear las direcciones de guardado
    private void setpaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar;
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar;
    }

    //creamos informacion para saber donde leñes esta nada personal
    private void crearData()
    {
        string savepath = persistentPath + "cartasDificiles.json";

        //string json = JsonUtility.ToJson(faciles[0]);
        //Debug.Log(json);

        //StreamWriter writer = new StreamWriter(savepath);
        //writer.Write(json);
        
        //FileHandler.SaveToJSON<infocarta>(dificiles, "cartasDificiles.json");

    }

    //baraja tood el cotarro
    private void Barajar()
    {


        BarajarFaciles();
        BarajarDificiles();
        //copiar las cartas a las que van a aparecerr durante el juego
        //chimpóm
    }

    private void BarajarFaciles()
    {

        Shuffle(faciles);

        for (int i = 0; i < faciles.Count; i++)
        {
            //if (faciles[i].active)
            cartasFaciles.Push(faciles[i]);
        }
    }
    private void BarajarDificiles()
    {
        Shuffle(dificiles);
        for (int i = 0; i < dificiles.Count; i++)
        {
            //if (dificiles[i].active)
            cartasDificiles.Push(dificiles[i]);
        }
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




    private void nextCard()
    {
        //saca las dos cartas y muestra las dos siguientes, si las pials estan vacias las rellena
        infocarta auxFacil, auxDificil;
        auxFacil = cartasFaciles.Pop();
        auxDificil = cartasDificiles.Pop();

        //buscarlo en lcartasFacilesas default y decir que han sido descubiertas
        dificilesDefault[dificilesDefault.IndexOf(auxDificil)].descubierta = true;
        facilesDefault[facilesDefault.IndexOf(auxFacil)].descubierta = true;

        if (cartasDificiles.Count == 0)
        {
            //esta vacio
            BarajarDificiles();
        }
        if (cartasFaciles.Count == 0)
        {
            BarajarFaciles();
        }

    }


    public infocarta GiveCartaFacil()
    {
        return cartasFaciles.Peek();
    }
    public infocarta GiveCartaDificil()
    {
        return cartasDificiles.Peek();
    }

    public infocarta GiveSeleccion()
    {
        return cartaSeleccion;
    }

    public void setSeleccion(bool esfacil)
    {
        cartaSeleccion = esfacil ? cartasFaciles.Peek() : cartasDificiles.Peek();
        GameManager.instance.actualCardPoints = esfacil ? cartasFaciles.Peek().puntos : cartasDificiles.Peek().puntos;
        nextCard();
        AudioManager.instance.Play(AudioManager.ESounds.botonInicio);
    }

    //actualiza los Json de guardado de las cartas custom, ejemplo, se desactivaron unas cartas //todo
    public void ActualizarSave()
    {
        FileHandler.SaveToJSON<infocarta>(facilesDefault, "cartasFaciles" + idioma + ".json");
        FileHandler.SaveToJSON<infocarta>(dificilesDefault, "cartasDificiles" + idioma + ".json");

        //eliminar las cartas custom en caso de que esten marcadas para eliminarse
        for (int i = 0; i < facilesCustom.Count; i++)
        {
            if (facilesCustom[i].eliminar)
            {
                facilesCustom.RemoveAt(i);
                i--;
            }
        } 
        
        for (int i = 0; i < dificilesCustom.Count; i++)
        {
            if (dificilesCustom[i].eliminar)
            {
                dificilesCustom.RemoveAt(i);
                i--;
            }
        }

        FileHandler.SaveToJSON<infocarta>(facilesCustom, "cartasFacilesCustom" + idioma + ".json");
        FileHandler.SaveToJSON<infocarta>(dificilesCustom, "cartasDificilesCustom" + idioma + ".json");
    }
    public void DesativarCarta(bool activar, int pos, bool facil, bool custom)
    {
        if (facil)
        {
            if (custom)
            {
                facilesCustom[pos].active = activar;
            }
            else
                facilesDefault[pos].active = activar;
        }
        else
        {
            if (custom)
            {
                dificilesCustom[pos].active = activar;
            }
            else
                dificilesDefault[pos].active = activar;
        }
    }

    public void addCartaCustom(bool esFacil, int points, string descripccion)
    {
        if (esFacil)
        {
            infocarta car = creadnoCarta(points, descripccion);
            car.descubierta = true;
            facilesCustom.Add(car);
        }
        else
        {
            infocarta car = creadnoCarta(points, descripccion);
            car.descubierta = true;
            dificilesCustom.Add(car);
        }
    }

    public void EliminarCartaCustom(bool eliminar, int pos, bool facil)
    {
        if (facil)
        {
                facilesCustom[pos].eliminar = eliminar;
        }
        else
        {
                dificilesCustom[pos].eliminar = eliminar;
        }
    }


    //añade todas las cartas al panel
    public void startPanel(GameObject panel)
    {
        for (int i = 0; i < facilesDefault.Count; i++)
        {
            //if (facilesDefault[i].active)
            GameObject go = Instantiate(cartaMenu, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            go.transform.SetParent(panel.transform, false);// = panel.transform;
                                                           //go.transform.parent = panel.transform;
            if (facilesDefault[i].descubierta)
            {
                GameObject text;
                text = go.transform.GetChild(0).gameObject;
                text.active = true;
                text.GetComponent<Text>().text = facilesDefault[i].descripcion;
                GameObject toggle;// = go.GetComponentsInChildren<Transform>()[1].gameObject;
                toggle = go.transform.GetChild(1).gameObject;
                toggle.active = true;
                toggle.GetComponent<Toggle>().isOn = facilesDefault[i].active;

                go.GetComponent<CardToggler>().setValues(i, true, false);
                switch (facilesDefault[i].puntos)
                {
                    case 1:
                        go.GetComponentInChildren<Image>().sprite = voz1;
                        break;
                    case 2:
                        go.GetComponentInChildren<Image>().sprite = voz2;
                        break;
                    case 3:
                        go.GetComponentInChildren<Image>().sprite = voz3;
                        break;
                    default:
                        break;
                }
            }
        }
        
        for (int i = 0; i < dificilesDefault.Count; i++)
        {
            GameObject go = Instantiate(cartaMenu, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            go.transform.SetParent(panel.transform, false);// = panel.transform;
            if (dificilesDefault[i].descubierta)
            {
                GameObject text;
                text = go.transform.GetChild(0).gameObject;
                text.active = true;
                text.GetComponent<Text>().text = dificilesDefault[i].descripcion;
                GameObject toggle;// = go.GetComponentsInChildren<Transform>()[1].gameObject;
                toggle = go.transform.GetChild(1).gameObject;
                toggle.active = true;
                toggle.GetComponent<Toggle>().isOn = dificilesDefault[i].active;
                go.GetComponent<CardToggler>().setValues(i, false, false);
                switch (dificilesDefault[i].puntos)
                {
                    case 1:
                        go.GetComponentInChildren<Image>().sprite = gestos1;
                        break;
                    case 2:
                        go.GetComponentInChildren<Image>().sprite = gestos2;
                        break;
                    case 3:
                        go.GetComponentInChildren<Image>().sprite = gestos3;
                        break;
                    default:
                        break;
                }
            }
        }

        for (int i = 0; i < facilesCustom.Count; i++)
        {
            //if (facilesCustom[i].active)
            GameObject go = Instantiate(cartaMenu, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            go.transform.SetParent(panel.transform, false);// = panel.transform;
                                                           //go.transform.parent = panel.transform;
            if (facilesCustom[i].descubierta)
            {
                GameObject text;
                text = go.transform.GetChild(0).gameObject;
                text.active = true;
                text.GetComponent<Text>().text = facilesCustom[i].descripcion;
                GameObject toggle;// = go.GetComponentsInChildren<Transform>()[1].gameObject;
                toggle = go.transform.GetChild(1).gameObject;
                toggle.active = true;
                toggle.GetComponent<Toggle>().isOn = facilesCustom[i].active;

                toggle = go.transform.GetChild(2).gameObject;
                toggle.active = true;



                go.GetComponent<CardToggler>().setValues(i, true, true);
                switch (facilesCustom[i].puntos)
                {
                    case 1:
                        go.GetComponentInChildren<Image>().sprite = voz1;
                        break;
                    case 2:
                        go.GetComponentInChildren<Image>().sprite = voz2;
                        break;
                    case 3:
                        go.GetComponentInChildren<Image>().sprite = voz3;
                        break;
                    default:
                        break;
                }
            }
        }

        for (int i = 0; i < dificilesCustom.Count; i++)
        {
            GameObject go = Instantiate(cartaMenu, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            go.transform.SetParent(panel.transform, false);// = panel.transform;
            if (dificilesCustom[i].descubierta)
            {
                GameObject text;
                text = go.transform.GetChild(0).gameObject;
                text.active = true;
                text.GetComponent<Text>().text = dificilesCustom[i].descripcion;
                GameObject toggle;// = go.GetComponentsInChildren<Transform>()[1].gameObject;
                toggle = go.transform.GetChild(1).gameObject;
                toggle.active = true;
                toggle.GetComponent<Toggle>().isOn = dificilesCustom[i].active;

                toggle = go.transform.GetChild(2).gameObject;
                toggle.active = true;
                go.GetComponent<CardToggler>().setValues(i, false, true);
                switch (dificilesCustom[i].puntos)
                {
                    case 1:
                        go.GetComponent<Image>().sprite = gestos1;
                        break;
                    case 2:
                        go.GetComponent<Image>().sprite = gestos2;
                        break;
                    case 3:
                        go.GetComponent<Image>().sprite = gestos3;
                        break;
                    default:
                        break;
                }
            }
        }


    }



    //pone el mazo con todas las cartas activas que se van a utilizar durante esta partida
    public void amontonar()
    {
        //limpiar las listas para evitar repeticiones

        dificiles.Clear();
        faciles.Clear();



        for (int i = 0; i < dificilesDefault.Count; i++)
        {
            if (dificilesDefault[i].active)
                dificiles.Add(dificilesDefault[i]);
        }


        for (int i = 0; i < facilesDefault.Count; i++)
        {
            if (facilesDefault[i].active)
                faciles.Add(facilesDefault[i]);
        }

        //custom
        for (int i = 0; i < dificilesCustom.Count; i++)
        {
            if (dificilesCustom[i].active)
                dificiles.Add(dificilesCustom[i]);
        }


        for (int i = 0; i < facilesCustom.Count; i++)
        {
            if (facilesCustom[i].active)
                faciles.Add(facilesCustom[i]);
        }

    

        //en caso de que algun idiota haya elegido jugar sin cartas, pos juegas con una nen
        if (dificiles.Count == 0)
            dificiles.Add(dificilesDefault[0]);
        if (faciles.Count == 0)
            faciles.Add(facilesDefault[0]);
       
    }

    // Start is called before the first frame update
    void Start()
    {
        //amontonar solo una vez cuando queramso volver a crear el mazo, ejemplo, hemos añadido u/y desactivado cartas //todo
        amontonar();
        Barajar();
        //crearData();
    }
}
