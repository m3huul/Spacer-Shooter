using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planets : MonoBehaviour
{
    [SerializeField] private float speed=-2f;
    [SerializeField] private Sprite[] PlanetSprites;
    [SerializeField] private float offset = -10f;
    Vector2 min;
    Vector2 max;

    private void Start()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));//bottom-left
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));//top-right    
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;

#if UNITY_ANDROID
        transform.position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        if (transform.position.y < min.y + offset)
        {
            transform.position = new Vector2(Random.Range(min.x, max.x), max.y - offset);
            GetComponent<SpriteRenderer>().sprite = PlanetSprites[Random.Range(0, PlanetSprites.Length)];
        }
#else
        transform.position = new Vector2(position.x + speed * Time.deltaTime, position.y);

        if (transform.position.x < min.x + offset)
        {
            transform.position = new Vector3(max.x - offset, Random.Range(min.y, max.y));
            GetComponent<SpriteRenderer>().sprite = PlanetSprites[Random.Range(0, PlanetSprites.Length)];
        }
#endif   
    }
}
