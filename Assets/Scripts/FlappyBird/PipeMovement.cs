using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Moves pipes to the left
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
