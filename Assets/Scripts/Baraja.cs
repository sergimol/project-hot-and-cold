using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

[Serializable]
public class infocarta
{
    public bool active;
    public int puntos;
    public string descripcion;

    public infocarta (bool activa, int puntaje, string info){
        active = activa;
        puntos = puntaje;
        descripcion = info;
    }
    public infocarta(){}
}

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

    //template de las cartas
    public GameObject carta;
    private GameObject cartaFacil, cartaDificil;
    private bool seleccionFacil = false, seleccionDificil = false;
    //una lista con todas las cartas faciles o dificiles que pueden haber
    //pila de cartas faciles que pueden sallir durante la partida
    //pila de cartas dificiles que pueden salir durante la partida
    List<infocarta> faciles, dificiles, facilesDefault, facilesCustom, dificilesDefault, dificilesCustom;
    Stack<infocarta>cartasFaciles, cartasDificiles;

    infocarta cartaSeleccion;

    string path = ""; //pos pa ir probando
    string persistentPath = ""; //el que ahi que poner en releasse o no funciona //TODO


    //metódo para setear las direcciones de guardado
    private void setpaths(){
        path = Application.dataPath + Path.AltDirectorySeparatorChar;
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar;
    }

    //creamos informacion para saber donde leñes esta nada personal
    private void crearData(){
        string savepath = path + "cartasDificiles.json";

        //string json = JsonUtility.ToJson(faciles[0]);
        //Debug.Log(json);

        //StreamWriter writer = new StreamWriter(savepath);
        //writer.Write(json);

        FileHandler.SaveToJSON<infocarta>(dificiles, "cartasDificiles.json");

    }

    //baraja tood el cotarro
    private void Barajar()
    {


        BarajarFaciles();
        BarajarDificiles();
        //copiar las cartas a las que van a aparecerr durante el juego
        //chimpóm
        Debug.Log("se barajaron todas las cartas");
    }

    private void BarajarFaciles(){

        Shuffle(faciles);

        for (int i = 0; i < faciles.Count; i++){
            //if (faciles[i].active)
                cartasFaciles.Push(faciles[i]);
        }
        Debug.Log("se barajaron las faciles");
    }
    private void BarajarDificiles(){
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


    private void nextCard(){
        //saca las dos cartas y muestra las dos siguientes, si las pials estan vacias las rellena
        cartasFaciles.Pop();
        cartasDificiles.Pop();

        if (cartasDificiles.Count == 0){
            //esta vacio
            BarajarDificiles();
        }
        if (cartasFaciles.Count == 0){
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

    public void setSelección(bool esfacil){
        cartaSeleccion = esfacil ? cartasFaciles.Peek() : cartasDificiles.Peek();
        nextCard();
        AudioManager.instance.Play(AudioManager.ESounds.botonInicio);
    }

    //actualiza los Json de guardado de las cartas custom, ejemplo, se desactivaron unas cartas //todo
    public void ActualizarCustomSave(){

        FileHandler.SaveToJSON<infocarta>(facilesCustom, "cartasFacilesCustom.json");
        FileHandler.SaveToJSON<infocarta>(dificilesCustom, "cartasDificilesCustom.json");
    }

    //elimian una carta del mazo permanentemente segun la posicion en el indice //todo
    public void EliminarCarta(int pos, bool facil)
    {
        if (facil)
        {
            facilesCustom.RemoveAt(pos);
        }
        else
        {
            dificilesCustom.RemoveAt(pos);
        }
    }

        //añadde una carta a los amzos Custom y guarda la información
        public void GuardarCarta(infocarta carta, bool facil)
    {
        if (facil)
        {
            facilesCustom.Add(carta);
        }
        else{
            dificilesCustom.Add(carta);
        }
        //seguramente es mas sencillo y seguro que esot solo ocurra una vez en vez de con cada carta, //TODO ejemplo al salir del menu de ccrear crear o descativar cartas custom
        ActualizarCustomSave();
    }

    //pone el mazo con todas las cartas activas que se van a utilizar durante esta partida
    public void amontonar(){
        //limpiar las listas para evitar repeticiones

        dificiles.Clear();
        faciles.Clear();



        for (int i = 0; i < dificilesDefault.Count; i++)
        {
            if (dificilesDefault[i].active)
            dificiles.Add(dificilesDefault[i]);
        }
        for (int i = 0; i < dificilesCustom.Count; i++)
        {
            if (dificilesCustom[i].active)
            dificiles.Add(dificilesCustom[i]);
        }

        for (int i = 0; i < facilesDefault.Count; i++)
        {
            if (facilesDefault[i].active)
            faciles.Add(facilesDefault[i]);
        }
        for (int i = 0; i < facilesCustom.Count; i++)
        {
            if (facilesCustom[i].active)
            faciles.Add(dificilesCustom[i]);
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
        setpaths();



        faciles = new List<infocarta>();
        dificiles = new List<infocarta>(); 
        facilesDefault = new List<infocarta>();
        dificilesDefault = new List<infocarta>(); 
        facilesCustom = new List<infocarta>();
        dificilesCustom = new List<infocarta>();
        cartasFaciles = new Stack<infocarta>();
        cartasDificiles = new Stack<infocarta>();

 
        facilesDefault = FileHandler.ReadListFromJSON<infocarta>("cartasFaciles.json");
        facilesCustom = (FileHandler.ReadListFromJSON<infocarta>("cartasFacilesCustom.json"));
        dificilesDefault = FileHandler.ReadListFromJSON<infocarta>("cartasDificiles.json");
        dificilesCustom = (FileHandler.ReadListFromJSON<infocarta>("cartasDificilesCustom.json"));





        //amontonar solo una vez cuando queramso volver a crear el mazo, ejemplo, hemos añadido u/y desactivado cartas
        amontonar();
        Barajar();



        Debug.Log("la primera carta de las faciles es" + cartasFaciles.Peek().descripcion);

        Debug.Log("la primera carta de las faciles es" + cartasDificiles.Peek().descripcion);


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
