using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    [SerializeField] private GameObject Star;
    [SerializeField] private int MaxStars;
    [SerializeField] private Vector2 initOffset = new Vector2(7, 7);
    [SerializeField] private Color[] starColors={
        new Color (0.5f,0.5f,1f),
        new Color (0,1f,1f),
        new Color (1f,1f,0),
        new Color (1f,0,0)
    };
    // Start is called before the first frame update
    void Start()
    {
        Vector2 min=Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        Vector2 max=Camera.main.ViewportToWorldPoint(new Vector2(1,1));
        min -= initOffset;
        max += initOffset;
        
        for (int i = 0; i < MaxStars; ++i)
        {
            GameObject star = (GameObject)Instantiate(Star);

            star.GetComponent<SpriteRenderer>().color = starColors[i % starColors.Length]; //setting star colours

            star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));

            star.GetComponent<Star>().speed = -(1f * Random.value + 0.5f);

            star.transform.parent = transform; //making star a child of StarGenerator
        }
    }  
}
