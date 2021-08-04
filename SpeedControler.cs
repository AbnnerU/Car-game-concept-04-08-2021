using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpeedControler : MonoBehaviour
{
    [SerializeField] private CarManeger carManeger;

    [SerializeField] private TrajetoryManeger trajetoryManeger;

    [SerializeField] private Slider speedSlider;

    [SerializeField] private float maxSpeedValue;

    [Header("Set Initial Speed")]
    [SerializeField] private float barChangeValuePerSecond;

    [Header("On game")]
    [SerializeField] private float increaseValuePerSecond;

    [SerializeField] private float decreaseValuePerSecond;

    private CarMovement carMovement;

    private bool chooseFirtAceleration;

    private bool chooseAceleration;

    private bool topToBottom;

    private bool speedingUp;

    private void Start()
    {
        carMovement =carManeger.GetPlayerCar().GetComponent<CarMovement>();

        speedSlider.maxValue = maxSpeedValue;

    }

    private void OnEnable()
    {
        chooseFirtAceleration = true;

        chooseAceleration = false;
    }

    private void Update()
    {
        if (chooseFirtAceleration)
        {
            if (topToBottom)
            {
                speedSlider.value -= barChangeValuePerSecond * Time.deltaTime;

                if (speedSlider.value == 0)
                    topToBottom = false;

            }
            else
            {
                speedSlider.value += barChangeValuePerSecond * Time.deltaTime;

                if (speedSlider.value == maxSpeedValue)
                    topToBottom = true;

            }
        }
        else if(chooseAceleration)
        {
            if (speedingUp)
            {
                speedSlider.value += increaseValuePerSecond * Time.deltaTime;

                carMovement.SetAcelaration(speedSlider.value);
            }
            else
            {
                speedSlider.value -= decreaseValuePerSecond * Time.deltaTime;

                carMovement.SetAcelaration(speedSlider.value);
            }
        }
    }

    public void OnPointerDown()
    {
        if (chooseFirtAceleration)
        {
            chooseFirtAceleration = false;

            chooseAceleration = true;

            //set speed

            carMovement.SetAcelaration(speedSlider.value);

            carManeger.StartCarMovement();

            //trajetory line
            trajetoryManeger.ClearTrajetory();

            trajetoryManeger.StartDrawTrajetory();

        }
        else if(chooseAceleration)
        {
            speedingUp = true;
        }
    }

    public void OnPointerUp()
    {
        if (chooseAceleration)
        {
            speedingUp = false;
        }
    }
}
