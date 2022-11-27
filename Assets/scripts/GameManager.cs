using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject startpage;
    public GameObject endpage;
    public GameObject coutdownpage;
    public Text scoretext;
    public int score = 0;
    bool gameover=false;
    public Text finalscore;
    public Text highscore;
    public GameObject flappybird;
    public GameObject pipegenerator;
    public int pipecount;
    public int pipegap;
    public Vector3 startpipeposition;
    public float pipeymax;
    public float pipeymin;
    public GameObject gamehandler;
    Vector3 gamehandlerstartpos;
    //public Vector3 flappypositionbegin;
    List<GameObject> pipelist=new List<GameObject>();
    public AudioSource diemusic;
    public AudioSource pointmusic;
    public AudioSource hitmusic;
    public List<BoxCollider2D> colliderarray = new List<BoxCollider2D>();


    public bool GameOver()
    {
        return gameover;
    }

    void Start()
    {
        instance = this;
        Time.timeScale = 0;
        highscore.text = "high score : " + PlayerPrefs.GetInt("higherscore");
        gamehandlerstartpos = gamehandler.transform.position;
       // flappypositionbegin = flappybird.transform.position;
        
    }

   
    void Update()
    {
        if (!gameover)
        {
            movegamehandler();
        } 
        
    }

    public void playbtnpressed()    
    {

        flappybird.GetComponent<PolygonCollider2D>().enabled = true;
        retaincollidersrest();
        distroypipelist();
        pipegeneration();
        gameover = false;
        startpage.SetActive(false);
        coutdownpage.SetActive(true);
        endpage.SetActive(false);
        score = 0;
        gamehandler.transform.position = gamehandlerstartpos;
        setflappybird();
        scoretext.text = ""+0;
      
    }

    void retaincollidersrest()
    {
        for (int i = 0; i < colliderarray.Count; i++)
        {
            if (colliderarray[i] != null)
            {
                colliderarray[i].GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    void distroypipelist()
    {
        for (int i = 0; i < pipelist.Count; i++)
        {
            DestroyImmediate(pipelist[i].gameObject);
        }
        pipelist.Clear();

    }

    public void gameoverfn()
    {
        gameover = true;
        endpage.SetActive(true);
        finalscore.text = "score : "+ score ;
        if (score > PlayerPrefs.GetInt("higherscore"))
        {
            PlayerPrefs.SetInt("higherscore", score);
        }
        pipecount = 0;
    }

    public void setflappybird()
    {
        flappybird.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        flappybird.transform.position = tapcontroller._startposition;
        //flappybird.transform.position = flappypositionbegin;
       // flappybird.transform.position = new Vector3(0, tapcontroller._startposition.y, 0);
        flappybird.transform.rotation = quaternion.Euler(0, 0, 0);
    }

    public void pipegeneration()
    {
        pipecount++;
        float yvalue = Random.Range(pipeymin, pipeymax);
        GameObject currentpipegenerated = Instantiate(pipegenerator);
        currentpipegenerated.transform.position = new Vector3((startpipeposition.x + pipecount * pipegap), yvalue, 0);
        pipelist.Add(currentpipegenerated);
        if(pipecount > 4)
        {
            Destroy(pipelist[pipecount-5]);
        }

    }

    public void movegamehandler()
    {
        gamehandler.transform.position += new Vector3(Time.deltaTime, 0, 0);
    }
    
}
