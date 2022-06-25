using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar1 : MonoBehaviour
{
    public int health;
    public int numberOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite empetyHeart;
 
    // Update is called once per frame
    void Update()
    {
        if(health > numberOfHearts) { 
        health = numberOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health){
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = empetyHeart;
            }
             if(i< numberOfHearts)
            {
                hearts[i].enabled = true;
            }else
            {
                hearts[i].enabled = false;
            }
        }
        
    }
}
