using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BeenHitTypeA : MonoBehaviour
{
    public Image damage_Image;
    public Color flash_Color;
    public float flash_Speed = 5;
    bool damaged = false;
    // Update is called once per frame  
    void Update()
    { 
        /*if (Input.GetMouseButtonDown(0))
        {
            TakeDamage();
        }*/
        PlayDamagedEffect();
    }

    void PlayDamagedEffect()
    {
        if (damaged)
        {

            damage_Image.color = flash_Color;
        }
        else
        {
            damage_Image.color = Color.Lerp(damage_Image.color, Color.clear, flash_Speed * Time.deltaTime);
        }
        damaged = false;
    }

    public void TakeDamage()
    {
        damaged = true;
    }

    public void DontTakeDamage()
    {
        damaged = false;
    }
} 
