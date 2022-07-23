using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText; // v1
    public float velocity = 1;
    public GameManagerFB gameManagerFB;
    private Rigidbody2D bird;
    int score = 0;  // v1
    bool dead = false;  // v1

    // Start is called before the first frame update
    void Start()
    {
        bird = GetComponent<Rigidbody2D>();
        score = 0;
        scoreText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)) && !dead)
        {
            bird.velocity = Vector2.up * velocity;
        }
    }

    // v1 method
    private void OnCollisionEnter2D()
    {
        dead = true;
        gameManagerFB.GameOver();
    }

    // v1 method
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "pointTrigger")
        {
            score++;
            scoreText.text = score.ToString();
        }
    }
}
