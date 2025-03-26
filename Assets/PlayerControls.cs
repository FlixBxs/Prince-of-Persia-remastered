using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Bewegungsvariablen
    [SerializeField] private float bewegungsGeschwindigkeit = 5f;
    [SerializeField] private float sprungKraft = 12f;

    // Komponenten
    private Rigidbody2D rb;
    private float horizontaleBewegung;
    private bool links;
    private bool rechts;
    private bool idle;
    private Animator animator;
    private bool Sprung = true;

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
        if (Input.GetButtonDown("Jump") && Sprung)
        {
            StartCoroutine(Jumpreset());
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, sprungKraft);
            Sprung = false;
            Debug.Log("Sprung");
        }
    }

    // FixedUpdate wird für physikbasierte Berechnungen verwendet
    private void FixedUpdate()
    {
        // Horizontale Bewegung anwenden
        rb.linearVelocity = new Vector2(horizontaleBewegung * bewegungsGeschwindigkeit, rb.linearVelocity.y);

         if (horizontaleBewegung > 0)
        {
            animator.SetBool("rechts", true);
            animator.SetBool("links", false);
            animator.SetBool("idle", false);
            StopCoroutine(SetIdleAfterDelay());
        }
        else if (horizontaleBewegung < 0)
        {
            animator.SetBool("links", true);
            animator.SetBool("rechts", false);
            animator.SetBool("idle", false);
            StopCoroutine(SetIdleAfterDelay());
        }
        else if (horizontaleBewegung == 0)
        {
            StartCoroutine(SetIdleAfterDelay());
        }
    }

    private IEnumerator SetIdleAfterDelay()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("idle", true);
    }
    private IEnumerator Jumpreset()
    {
        yield return new WaitForSeconds(1f);
        Sprung = true;
    }
}

