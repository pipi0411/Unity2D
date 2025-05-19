using UnityEngine;


public class Random : MonoBehaviour
{
    [SerializeField] int width, le;
    [SerializeField] GameObject dirt;
    void Start()
    {
        Generation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Generation(){
        for (int x = 0; x < width; x++)
        {
            Instantiate(dirt, new Vector2(x, 0), Quaternion.identity);
        }
    }
}
