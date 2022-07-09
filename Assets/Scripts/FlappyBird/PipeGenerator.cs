using UnityEngine;

public class PipeGenerator : MonoBehaviour
{
    [SerializeField] GameObject pipes;  // v1
    // int timer = 0;  // v1

    public float maxTime = 1;
    public GameObject pipe;
    public float height;

    private float timer = 0;

    void Start()
    {
        // Creates a new pipe at a random height
        GameObject newpipe = Instantiate(pipe);
        newpipe.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
    }

    void Update()
    {
        if (timer > maxTime)
        {
            GameObject newpipe = Instantiate(pipe);
            newpipe.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
            Destroy(newpipe, 15);
            timer = 0;
        }

        timer += Time.deltaTime;
    }

    // v1 method
    //void FixedUpdate()
    //{
    //    timer++;
    //    if (timer >= 100)
    //    {
    //        timer = 0;
    //        GameObject pipe = Instantiate(pipes, new Vector2(pipes.transform.position.x, pipes.transform.position.y + Random.Range(-4, 4)), pipes.transform.rotation);
    //        Destroy(pipe, 5);
    //    }
    //}
}
