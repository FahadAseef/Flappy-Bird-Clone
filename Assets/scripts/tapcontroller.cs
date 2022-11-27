using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]

public class tapcontroller : MonoBehaviour
{
    public float tapforce;
    public float tiltsmooth;
    public static Vector3 _startposition;
    Rigidbody2D _rigidbody2D;
    quaternion downrotation;
    Quaternion forwardrotation;


    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _startposition = this.transform.position;
        downrotation= quaternion.Euler(0,0,30);
        forwardrotation= quaternion.Euler(0,0,33);
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0 && !GameManager.instance.GameOver() )
        {

            transform.rotation = forwardrotation;
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.AddForce(Vector2.up * tapforce, ForceMode2D.Force);
    
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, downrotation, Time.deltaTime * tiltsmooth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "deadzone")
        {
            GameManager.instance.gameoverfn();
            GameManager.instance.diemusic.Play();
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameManager.instance.colliderarray.Add(collision.gameObject.GetComponent<BoxCollider2D>());
            this.GetComponent<PolygonCollider2D>().enabled = false;
        }
        else if (collision.gameObject.tag == "scorezone")
        {
            GameManager.instance.pointmusic.Play();
            GameManager.instance.score++;
            GameManager.instance.scoretext.text ="" + GameManager.instance.score;
            GameManager.instance.pipegeneration();
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameManager.instance.colliderarray.Add(collision.gameObject.GetComponent<BoxCollider2D>());
        }
        else if (collision.gameObject.tag == "pipedead")
        {
            GameManager.instance.hitmusic.Play();     
            GameManager.instance.gameoverfn();
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameManager.instance.colliderarray.Add(collision.gameObject.GetComponent<BoxCollider2D>());
            this.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
}
