using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public bool isActivatedDoor = false;
    public float startTimer;
    public float chargedTimer = 0;
    public float chargeOverTime = 1.0f;
    public float fullCharge;
    private float winCount = 0;
    private Slider chargeSlider;
   
    // Start is called before the first frame update
    void Start()
    {
        chargedTimer = startTimer;
        chargeSlider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isActivatedDoor)
        {
            chargedTimer += chargeOverTime * Time.deltaTime;
            chargeSlider.value = chargedTimer / fullCharge;
            if (chargedTimer >= fullCharge)
            { 
                winCount++;
                if(winCount == 1)
                {
                    GameManager.Instance.Win();
                }
                
            }
        }
        isActivatedDoor = false;

       

    }

    public void ActivateDoor()
    {
        isActivatedDoor = true;
    }
}
