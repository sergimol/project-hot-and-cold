using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Maneja el movimiento en las 4 direcciones
//Tambien acciona las animaciones correspondiente a cada direccion
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 15;

    [SerializeField]
    float max_speed = 5;

    Vector2 direccion;
    Animator[] anim;
    Rigidbody2D rb;
    GameObject closest;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        closest = null;
        //anim[0] player //anim[1] sword
        anim = GetComponentsInChildren<Animator>();
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 direccionx;
        Vector2 direcciony;
        if (Input.GetJoystickNames().Length > 0) // MANDO
        {
            direccion = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            direccion.Normalize();
        }
        else // TECLADO Y RATÓN
        {
            if (Input.GetKey("w")) direcciony = new Vector2(0, 1);
            else if (Input.GetKey("s")) direcciony = new Vector2(0, -1);
            else direcciony = new Vector2(0, 0);

            if (Input.GetKey("d")) direccionx = new Vector2(1, 0);
            else if (Input.GetKey("a")) direccionx = new Vector2(-1, 0);
            else direccionx = new Vector2(0, 0);

            direccion = direccionx + direcciony;
            direccion.Normalize();
        }
        //anim[0].SetFloat("Speed", Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y));
        //anim[1].SetFloat("Speed", Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y));
        if(direccion.magnitude > 0){
            rb.drag = 0.01f;
        }else{
            rb.drag = 2.0f;
        }
    }
//uwu

    private void FixedUpdate()
    {
        if(rb.velocity.magnitude < max_speed){
            rb.AddForce(direccion * speed);
        }
    }

    //Metodos usados en los PowerUps Verde y Azul para manejar la velocidad del jugador
    public void MulSpeed(int x)
    {
        speed *= x;
    }
    public void DivSpeedReset(int x)
    {
        speed /= x;
    }
	
	private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprueba si el nuevo objeto está más cerca
        if (!closest || (Vector2.Distance(other.transform.position, rb.transform.position) < Vector2.Distance(closest.transform.position, rb.transform.position)))
        {
            if(closest)
                closest.GetComponentInChildren<Glow>().enabled = false;
            closest = other.gameObject;
            closest.GetComponentInChildren<Glow>().enabled = true;
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        GameObject g = other.gameObject;
        // Comprueba si el nuevo objeto está más cerca
        if (closest != g || (Vector2.Distance(other.transform.position, rb.transform.position) < Vector2.Distance(closest.transform.position, rb.transform.position)))
        {
            if (closest)
                closest.GetComponentInChildren<Glow>().enabled = false;
            closest = g;
            closest.GetComponentInChildren<Glow>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (closest == other.gameObject)
        {
            closest.GetComponentInChildren<Glow>().enabled = false;
            Debug.Log("danlles ya no brillo");
            closest = null;
        }
    }
}