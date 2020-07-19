using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EngineCriticalFlash : MonoBehaviour
{

    public TMP_Text criticalText;

    // Start is called before the first frame update
    void Start()
    {
        //Call coroutine BlinkText on Start
        StartCoroutine(BlinkText());
    }

    //function to blink the text 
    public IEnumerator BlinkText()
    {
        //blink it forever. You can set a terminating condition depending upon your requirement
        while (true)
        {
            //set the Text's text to blank
            criticalText.text = "";
            //display blank text for 0.5 seconds
            yield return new WaitForSeconds(.2f);
            //display “INSERT COIN[S]” for the next 0.5 seconds
            criticalText.text = "ENGINE CRITICAL!!!";
            yield return new WaitForSeconds(.2f);
        }
    }
}
