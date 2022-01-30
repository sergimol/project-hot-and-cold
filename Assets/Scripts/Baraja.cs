using System.Collections;
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

    public infocarta(bool activa, int puntaje, string info, bool dec)
    {
        active = activa;
        puntos = puntaje;
        descripcion = info;
        descubierta = dec;
    }
    public infocarta() { }
}

public class Baraja : MonoBehaviour
{

    public static Baraja instance;

 
    //template de las cartas
    [SerializeField]
    private GameObject cartaVoz, cartaGesto, cartaMenu;
    [SerializeField]
    private Sprite voz1, voz2, voz3, gestos1, gestos2, gestos3;
    private bool seleccionFacil = false, seleccionDificil = false;
    //una lista con todas las cartas faciles o dificiles que pueden haber
    //pila de cartas faciles que pueden sallir durante la partida
    //pila de cartas dificiles que pueden salir durante la partida
    List<infocarta> faciles, dificiles, facilesDefault, dificilesDefault;
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
            dificilesDefault = new List<infocarta>();
            cartasFaciles = new Stack<infocarta>();
            cartasDificiles = new Stack<infocarta>();


            facilesDefault = FileHandler.ReadListFromJSON<infocarta>("cartasFaciles.json");
            dificilesDefault = FileHandler.ReadListFromJSON<infocarta>("cartasDificiles.json");


        }
        else
        {
            Destroy(this.gameObject);
        }



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
        string savepath = path + "cartasDificiles.json";

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
        Debug.Log("se barajaron las faciles");
    }
    private void BarajarDificiles()
    {
        Shuffle(dificiles);
        for (int i = 0; i < dificiles.Count; i++)
        {
            //if (dificiles[i].active)
            cartasDificiles.Push(dificiles[i]);
        }
        Debug.Log("SE ABRAJARON ALS DIFICILES");
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


        Debug.Log("la primera carta de las faciles es" + cartasFaciles.Peek().descripcion);

        Debug.Log("la primera carta de las dificiles es" + cartasDificiles.Peek().descripcion);//se e dice cuales son las dos siguientes

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
        nextCard();
        AudioManager.instance.Play(AudioManager.ESounds.botonInicio);
    }

    //actualiza los Json de guardado de las cartas custom, ejemplo, se desactivaron unas cartas //todo
    public void ActualizarSave()
    {
        FileHandler.SaveToJSON<infocarta>(facilesDefault, "cartasFaciles.json");
        FileHandler.SaveToJSON<infocarta>(dificilesDefault, "cartasDificiles.json");
    }
    public void DesativarCarta(bool activar, int pos, bool facil, bool custom)
    {
        if (facil)
        {
            if (custom)
            {

            }
                //facilesCustom[pos].active = activar;
            else
                faciles[pos].active = activar;
        }
        else
        {
            if (custom)
            {

            }
                //dificilesCustom[pos].active = activar;
            else
                dificiles[pos].active = activar;
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
                        go.GetComponent<Image>().sprite = voz1;
                        break;
                    case 2:
                        go.GetComponent<Image>().sprite = voz2;
                        break;
                    case 3:
                        go.GetComponent<Image>().sprite = voz3;
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
       
    }

    // Start is called before the first frame update
    void Start()
    {
        //amontonar solo una vez cuando queramso volver a crear el mazo, ejemplo, hemos añadido u/y desactivado cartas //todo
        amontonar();
        Barajar();


        //crearData();

    }

    // Update is called once per frame
    void Update()
    {

        //debug
        if (Input.GetKeyUp("k"))
        {


            Debug.Log("siguientes cartas");
            nextCard();
        }


    }
}
