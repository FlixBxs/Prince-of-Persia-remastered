using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Spielt Sounds bei Collision ab
    public AudioSource audioPlayer;
    public AudioSource audioPlayer2;

    // Bewegungsvariablen
    [SerializeField] private float bewegungsGeschwindigkeit = 5f;
    [SerializeField] private float sprungKraft = 12f;

    // Komponenten
    private Rigidbody2D rb;
    private float horizontaleBewegung;
    private bool links;
    private bool rechts;
    private bool idle;
    private bool jumplinks;
    private bool jumprechts;
    private Animator animator;
    private bool Sprung = false;
    private bool Sprung2 = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Horizontale Eingabe erfassen (links/rechts)
        horizontaleBewegung = Input.GetAxisRaw("Horizontal");

        // Springen, wenn Leertaste gedrückt wird und der Spieler am Boden ist
        if (Input.GetKeyDown(KeyCode.Space) && Sprung == false)
        {
            StartCoroutine(Jumpreset());
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, sprungKraft);
            Sprung = true;
            Sprung2 = true;
            Debug.Log("Sprung");
            Debug.Log("Sprung2");
        }
    }

    // FixedUpdate wird für physikbasierte Berechnungen verwendet
    private void FixedUpdate()
    {
        // Horizontale Bewegung anwenden
        rb.linearVelocity = new Vector2(horizontaleBewegung * bewegungsGeschwindigkeit, rb.linearVelocity.y);

        if (horizontaleBewegung > 0 && Sprung == false)
        {
            animator.SetBool("rechts", true);
            animator.SetBool("links", false);
            animator.SetBool("idle", false);
        }
        else if (horizontaleBewegung < 0 && Sprung == false)
        {
            animator.SetBool("links", true);
            animator.SetBool("rechts", false);
            animator.SetBool("idle", false);
        }
        else if (horizontaleBewegung == 0)
        {
            animator.SetBool("idle", true);
            animator.SetBool("links", false);
            animator.SetBool("rechts", false);
        }
        else if (horizontaleBewegung > 0 && Sprung2 == true)
        {
            animator.SetBool("idle", false);
            animator.SetBool("links", false);
            animator.SetBool("rechts", false);
            animator.SetTrigger("jumprechts");
            Sprung2 = false;
        }
        else if (horizontaleBewegung < 0 && Sprung2 == true)
        {
            animator.SetBool("links", false);
            animator.SetBool("rechts", false);
            animator.SetBool("idle", false);
            animator.SetTrigger("jumplinks");
            Sprung2 = false;
        }
        else if (horizontaleBewegung == 0 && Sprung2 == true)
        {
            animator.SetBool("idle", false);
            animator.SetTrigger("jumplinks");
            animator.SetBool("links", false);
            animator.SetBool("rechts", false);
            animator.SetTrigger("jumprechts");
            Debug.Log("Sprung2");
            Sprung2 = false;
        }

    }
    private IEnumerator Jumpreset()
    {
        yield return new WaitForSeconds(1f);
        Sprung = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bosssound")
        {
            audioPlayer.Play();
        }
        if (collision.gameObject.tag == "Heilig")
        {
            audioPlayer2.Play();
            Invoke("zweiteScene", 3f);
        }

    }
    private void zweiteScene()
    {
        transform.position = new Vector2(270f, 0f);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Tod")
        {
            transform.position = new Vector2(-16f, -3.6f);
        }
    }
}

