using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandyPickUp : MonoBehaviour
{
    float candyStartAmount = 0;
    float candyValue = 100;
    float candyCounter;
    float candyScore;

    public Text candyCounterText;
    public Text scoreCounter;

    private void Start()
    {
        candyCounter = candyStartAmount;
        candyScore = candyStartAmount;
        candyCounterText.text = candyCounter.ToString();
        scoreCounter.text = candyScore.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Candy")
        {
            candyCounter++;
            candyScore += candyValue;
            candyCounterText.text = candyCounter.ToString();
            scoreCounter.text = candyScore.ToString();
            Destroy(other.gameObject);
        }
    }
}
