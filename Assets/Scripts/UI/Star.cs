using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] internal float speed;
    private Vector2 min;
    private Vector2 max;
    private float offset = -10;
    // Start is called before the first frame update
    void Start()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));//bottom-left
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));//top-right
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position=transform.position;

#if UNITY_ANDROID
        transform.position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        if (transform.position.y < min.y)
        {
            transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        }
#else
        transform.position = new Vector2(position.x + speed * Time.deltaTime, position.y);

        if (transform.position.x < min.x + offset)
        {
            transform.position = new Vector2(max.x - offset, Random.Range(min.y, max.y));
        }
#endif
    }
}
