using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float groundedDistance = 3f;
    [SerializeField] private LayerMask groundedMask;
    [SerializeField] private GameObject blue;
    [SerializeField] private GameObject yellow;
    [SerializeField] private GameObject red;

    [SerializeField] private GameObject blueSplash;
    [SerializeField] private GameObject yellowSplash;
    [SerializeField] private GameObject redSplash;
    [SerializeField] private GameObject halo;
    private Renderer haloRenderer;
    private Color haloColor;
    


    private int ResModulo;
    private int indexObject;

    [SerializeField] private string sceneNameToLoad;


    public List<string> Inventory = new List<string>();

    Rigidbody2D rb2D;

    public AudioClip getTableau;
    public AudioClip getColor;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        haloRenderer = halo.GetComponent < Renderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        indexObject = 0;
        Inventory.Add("Default");
        blue.SetActive(false);
        yellow.SetActive(false);
        red.SetActive(false);
        blueSplash.SetActive(false);
        yellowSplash.SetActive(false);
        redSplash.SetActive(false);
    }
    void Update()
    {

        Move();
        Jump();

        if (Inventory.Count == 2)
        {
            blueSplash.SetActive(true);
        }
        if (Inventory.Count == 3)
        {
            yellowSplash.SetActive(true);
        }
        if (Inventory.Count == 4)
        {
            redSplash.SetActive(true);
        }

    }


    private void Move()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += speed * Time.deltaTime * Vector3.right;
            Vector3 theScale = transform.localScale;
            theScale.x = 1;
            transform.localScale = theScale;
        };
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += speed * Time.deltaTime * Vector3.left;
            Vector3 theScale = transform.localScale;
            theScale.x = -1;
            transform.localScale = theScale;
        };
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb2D.velocity = jumpSpeed * Vector2.up;

        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundedDistance, groundedMask);
        if (hit.collider == null)
        {
            return false;

        }
        else
        {
            return true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "ladder" && Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += speed * Time.deltaTime * Vector3.down;
        }


    }


    void OnTriggerStay2D(Collider2D col)
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            var goCollided = col.gameObject;

            if (goCollided.tag == "yellow" || goCollided.tag == "blue" || goCollided.tag == "red")
            {
                Inventory.Add(goCollided.tag);
                Destroy(goCollided);
                GetComponent<AudioSource>().PlayOneShot(getTableau);
            }

            if (goCollided.tag == "mirror")
            {
                indexObject++;
                ResModulo = indexObject % (Inventory.Count);
                GetComponent<AudioSource>().PlayOneShot(getColor);



                if (Inventory[ResModulo] == "blue")
                {
                    blue.SetActive(true);
                    yellow.SetActive(false);
                    red.SetActive(false);
                    haloColor = new Color(0.1882353f, 0.3686275f, 0.7058824f, 1f);
                    haloRenderer.material.SetColor("_Color", haloColor);
                }

                if (Inventory[ResModulo] == "yellow")
                {
                    yellow.SetActive(true);
                    red.SetActive(false);
                    blue.SetActive(false);
                    haloColor = new Color(0.7058824f, 0.6352941f, 0.1882353f, 1f);
                    haloRenderer.material.SetColor("_Color", haloColor);

                }

                if (Inventory[ResModulo] == "red")
                {
                    red.SetActive(true);
                    yellow.SetActive(false);
                    blue.SetActive(false);
                    haloColor = new Color(0.6666667f, 0.1921569f, 0.03921569f, 1f);
                    haloRenderer.material.SetColor("_Color", haloColor);
                }

                if(Inventory[ResModulo] == "Default")
                {
                    blue.SetActive(false);
                    yellow.SetActive(false);
                    red.SetActive(false);
                    haloColor = new Color(1f, 1f, 1f, 1f);
                    haloRenderer.material.SetColor("_Color", haloColor);
                }
            }
        }

        if (col.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene(sceneNameToLoad);
        }
    }


}

// Update is called once per frame






