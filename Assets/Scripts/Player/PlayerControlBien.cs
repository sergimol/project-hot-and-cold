using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Maneja el movimiento en las 4 direcciones
//Tambien acciona las animaciones correspondiente a cada direccion
public class PlayerControlBien : MonoBehaviour
{
    [SerializeField]
    float speed = 15;

    [SerializeField]
    float max_speed = 5;

    [SerializeField]
    float maxDrag = 4.0f;

    [SerializeField]
    float minDrag = 0.01f;

    [SerializeField]
    SpriteRenderer renderer;

    Vector2 direccion;
    Rigidbody2D rb;
    GameObject closest;
    
    float rotationPos = 0;

    [SerializeField]
    float rotationMax = 10.0f;

    [SerializeField]
    Timer timer;

    float rotationKey = 0.5f;

    bool antiSpam = false;
    float startTime;
    Vector3 originalPos;
    float shakeAmount = 0.1f;


    SpriteRenderer sprite;
    Animator anim;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        closest = null;
        //anim[0] player //anim[1] sword
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 direccionx;
        Vector2 direcciony;
        //if (Input.GetJoystickNames().Length > 0) // MANDO
        //{
            direccion = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            direccion.Normalize();

            //if (!antiSpam && Input.GetButtonUp("Fire1") && closest)
            //{
            //    inputManagement();
            //}
        //}
        //else // TECLADO Y RATÓN
        //{
            //anim.SetBool("moving", true);
            bool move = true;

            //if (Input.GetKey("w")) direcciony = new Vector2(0, 1);
            //else if (Input.GetKey("s")) direcciony = new Vector2(0, -1);
            //else direcciony = new Vector2(0, 0);

            //if (Input.GetKey("d")) direccionx = new Vector2(1, 0);
            //else if (Input.GetKey("a")) direccionx = new Vector2(-1, 0);
            //else direccionx = new Vector2(0, 0);

            if (!antiSpam && (Input.GetKeyDown("space") || Input.GetButtonUp("Fire1")) && closest)
            {
                inputManagement();
            }

            //direccion = direccionx + direcciony;
            //direccion.Normalize();
            
            //Para la animacion
            if (direccion.x == 0 && direccion.y == 0)
                move = false;

            if (move)
            {
                anim.SetBool("moving", true);
                AudioManager.instance.Play(AudioManager.ESounds.footsteps);
            }
            else
                AudioManager.instance.Stop(AudioManager.ESounds.footsteps);

        //}
        //anim[0].SetFloat("Speed", Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y));
        //anim[1].SetFloat("Speed", Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y));
        if(direccion.magnitude > 0){
            rb.drag = minDrag;
        }else{
            rb.drag = maxDrag;
        }

        if (antiSpam)
        {
            startTime -= Time.deltaTime;
            originalPos = Camera.main.transform.position;
            Camera.main.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            if (startTime < 0)
            {
                antiSpam = false;
            }
        }

    }
    private void inputManagement()
    {
        if (closest.GetComponent<ObjectProperties>().searchingThis)
        {
            GameManager.instance.addPoints();
            gameObject.GetComponent<CameraZoom>().enabled = true;
            AudioManager.instance.Play(AudioManager.ESounds.acierto);
        }
        else
        {
            timer.reduceTime();
            antiSpam = true;
            startTime = 0.5f;
            AudioManager.instance.Play(AudioManager.ESounds.fallo);

            //Aceleramos la musica
            //no se si se puede hacer
            AudioManager.instance.increasePitch(AudioManager.ESounds.temon);
        }
    }
    private void FixedUpdate()
    {
        if(renderer != null){
            if(direccion.x > 0){
                renderer.flipX = false;
                rotationPos = Mathf.Lerp(rotationPos, rotationMax, rotationKey);
                renderer.transform.localRotation = Quaternion.Euler(0,0,rotationPos);
            }
            else if(direccion.x < 0){
                renderer.flipX = true;
                rotationPos = Mathf.Lerp(rotationPos, -rotationMax, rotationKey);
                renderer.transform.localRotation = Quaternion.Euler(0,0,rotationPos);
            }
            else{
                rotationPos = Mathf.Lerp(rotationPos, 0, rotationKey);
                renderer.transform.localRotation = Quaternion.Euler(0,0,rotationPos);
            }
        }

        if(rb.velocity.magnitude < max_speed){
            rb.AddForce(direccion * speed);
        }else{
            rb.drag = maxDrag;
        }



        //ANIMACION 
        if(rb.velocity.x < 1 && rb.velocity.x > -1 && rb.velocity.y < 1 && rb.velocity.y > -1)
            anim.SetBool("moving", false);

        if (rb.velocity.x > 0)
            sprite.flipX = true;
        else
            sprite.flipX = false;


        //rb.velocity = direccion*speed;
    }
	private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<ObjectProperties>())
            return;

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
        if (!other.GetComponent<ObjectProperties>())
            return;

        GameObject g = other.gameObject;
        // Comprueba si el nuevo objeto está más cerca
        if (closest != g || (Vector2.Distance(other.transform.position, rb.transform.position) < Vector2.Distance(closest.transform.position, rb.transform.position)))
        {
            if (closest != null)
                closest.GetComponentInChildren<Glow>().enabled = false;
            closest = g;
            closest.GetComponentInChildren<Glow>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.GetComponent<ObjectProperties>())
            return;

        if (closest == other.gameObject)
        {
            closest.GetComponentInChildren<Glow>().enabled = false;
            closest = null;
        }
    }
}