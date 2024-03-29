using UnityEngine;

public class PipeGenerator : MonoBehaviour
{
    [SerializeField] GameObject pipes;

    public float maxTime = 1;
    public GameObject pipe;
    public float height;

    private float timer = 0;

    void Start()
    {
        // Creates a new pipe at a random height
        GameObject newpipe = Instantiate(pipe);
        newpipe.transform.position = transform.position + new Vector3(2, Random.Range(-height, height), 0);
    }

    void Update()
    {
        if (timer > maxTime)
        {
            GameObject newpipe = Instantiate(pipe);
            newpipe.transform.position = transform.position + new Vector3(2, Random.Range(-height, height), 0);
            Destroy(newpipe, 15);
            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
