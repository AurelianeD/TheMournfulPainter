using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBlock : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private Vector3 pos1;
    [SerializeField] private Vector3 pos2;

    [SerializeField] private GameObject plateform;






    // Start is called before the first frame update
    void Start()
    {

    }
  

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f));   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(plateform.tag == "movableBlock")

        {
            Debug.Log("colission");
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);

    }
}
