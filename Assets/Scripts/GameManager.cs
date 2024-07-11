using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;

    public int health;

    public static GameManager inst;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI healthText;


    public void incScore(int amount)
    {
        score=score+amount;
        scoreText.text = "SCORE : " + score;

    }
    public int getScore()
    {
        return score;
    }
    public void decHealth(int amount)
    {
        health = health - amount;
        healthText.text = "HEALTH : " + health;
    }

    private void Awake()
    {
        inst = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
