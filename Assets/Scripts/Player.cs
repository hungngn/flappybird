using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] public Sprite[] sprites;
    private int index = 0;
    private Vector3 direction;
    public float gravity = -10f;
    public float strength = 5f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprites), 0.1f, 0.1f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)  || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    private void AnimateSprites()
    {
        index++;
        if(index > 2) index = 0;
        spriteRenderer.sprite = sprites[index];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Pipes")
        {
            FindObjectOfType<GameManager>().GameOver();
        } else if(other.gameObject.tag == "Scoring")
        {
            FindObjectOfType<GameManager>().PlusScore();
        }
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction =Vector3.zero;
    }
}
