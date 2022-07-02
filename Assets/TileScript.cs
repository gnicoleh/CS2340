
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Vector3 targetPostition;
    private Vector3 correctPosition;
    private SpriteRenderer _sprite;
    public int number;
    // Start is called before the first frame update
    void Start()
    {
        targetPostition = transform.position;
        correctPosition = transform.position;
        _sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPostition, 0.05f);
        if (targetPostition == correctPosition)
        {
            _sprite.color = Color.green;
        }
        else
        {
            _sprite.color = Color.white;
        }
    }
}
