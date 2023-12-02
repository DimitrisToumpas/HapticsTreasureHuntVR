    using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    float currentTime = 0f;
    public float startingTime = 10f;

    [SerializeField] TextMeshProUGUI countdownText;
    //[SerializeField] Text gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        Outline outlineComponent = countdownText.GetComponent<Outline>();
        //gameOverText.enabled = false;
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime >= 0) {
            if (currentTime < 0.5)
            {
                countdownText.color = Color.red;
                
            }
            countdownText.text = "Time : "+ currentTime.ToString("0");
        }
        //GameOverScreen();
    }

    /*public void GameOverScreen()
    {
        if (currentTime <= 0.5)
        {
            gameOverText.enabled=true;

        }
    }
    */
}
