using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countdowntextscript : MonoBehaviour
{
     Text countountext;
    int counttimer;

    // Start is called before the first frame update
    void OnEnable()
    {
        counttimer = 3;
        countountext = GetComponent<Text>();
        StartCoroutine(countownfn());
        
    }

    IEnumerator countownfn()
    {
        if (counttimer > 0)
        {
            Time.timeScale = 0;
            countountext.text = "" + counttimer;
            yield return new WaitForSecondsRealtime(1);
            counttimer--;
            StartCoroutine(countownfn());
           // Time.timeScale = 0;
        }
        else
        {
            GameManager.instance.coutdownpage.SetActive(false);
            Time.timeScale = 1;
            
        }
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
