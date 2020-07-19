using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EngineDangerFlash : MonoBehaviour
{
   public  TMP_Text dangerText;

    void Start()
    {
        //get the Text component
        
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
            dangerText.text = "";
            //display blank text for 0.5 seconds
            yield return new WaitForSeconds(.4f);
            //display “INSERT COIN[S]” for the next 0.5 seconds
            dangerText.text = "ENGINE DANGER!";
            yield return new WaitForSeconds(.4f);
        }
    }
}
