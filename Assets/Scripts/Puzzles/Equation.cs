﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

public class Equation : MonoBehaviour
{
    private int num1;
    private int op;
    private int num2;
    private int res;
    private bool valid = false;
    private bool started = false;
    private bool finished = false;
    public int points = 0; 
    public float maxTime;
    private float timeRemaining;
    public bool timerIsRunning;
    public Text TimerText;
    public Text EquationText;
    public Material materialOff;
    public Material materialOn;
    public GameObject handLight1;
    public GameObject handLight2;
    public GameObject handLight3;
    public GameObject handLight4;
    public GameObject handLight5;
    private GameObject doorName;
    DoorOpener doorOpener;
    Door door1;
    Door door2;
    Door door3;
    Door door4;
    private string puzzleNumber;

    // Start is called before the first frame update
    void Start()
    {
        puzzleNumber = string.Concat(gameObject.name.Where(char.IsDigit));
        try
        {
            //doorOpener = GameObject.Find("sala." + puzzleNumber).GetComponent<DoorOpener>();
            //doorName = GameObject.Find("sala." + puzzleNumber).transform.GetChild(1).gameObject;
            door1 = GameObject.Find("door." + puzzleNumber + ".1").GetComponent<Door>();
            door2 = GameObject.Find("door." + puzzleNumber + ".2").GetComponent<Door>();
            door3 = GameObject.Find("door." + puzzleNumber + ".3").GetComponent<Door>();
            door4 = GameObject.Find("door." + puzzleNumber + ".4").GetComponent<Door>();
        }
        catch
        {
            doorOpener = GameObject.Find("sala").GetComponent<DoorOpener>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                TimerText.text = timeRemaining.ToString();
            }
            else
            {
                Debug.Log("Time has run out !");
                points = 0;
                UpdateLights(points);
                CreateEquation();
                timeRemaining = maxTime;
            }
        }
    }

    void UpdateLights(int points)
    {
        switch (points)
        {
            case 0:
                handLight1.GetComponent<MeshRenderer>().material = materialOff;
                handLight2.GetComponent<MeshRenderer>().material = materialOff;
                handLight3.GetComponent<MeshRenderer>().material = materialOff;
                handLight4.GetComponent<MeshRenderer>().material = materialOff;
                handLight5.GetComponent<MeshRenderer>().material = materialOff;
                break;
            case 1:
                handLight1.GetComponent<MeshRenderer>().material = materialOn;
                break;
            case 2:
                handLight2.GetComponent<MeshRenderer>().material = materialOn;
                break;
            case 3:
                handLight3.GetComponent<MeshRenderer>().material = materialOn;
                break;
            case 4:
                handLight4.GetComponent<MeshRenderer>().material = materialOn;
                break;
            case 5:
                handLight5.GetComponent<MeshRenderer>().material = materialOn;
                timerIsRunning = false;
                finished = true;
                door1.openDoor();
                door2.openDoor();
                door3.openDoor();
                door4.openDoor();
                break;
        }
    }

    public void StartPuzzle()
    {
        if (!finished) 
        {
            points = 0;
            UpdateLights(points);
            timeRemaining = maxTime;
            timerIsRunning = true;
            started = true;
            finished = false;
            CreateEquation(); 
        }
    }

    public void CreateEquation()
    {
        num1 = Random.Range(0, 10); 
        op = Random.Range(0, 2);
        num2 = Random.Range(0, 10);
        switch (op)
        {
            case 0:
                while (!valid)
                {
                    res = positiveRes(num1, num2);
                    if (res > 0 && res <=9)
                    {
                        valid = true;
                        EquationText.text = num1 + " + " + num2 + " = ?";
                    }
                    else
                    {
                        num1 = Random.Range(0, 9);
                        num2 = Random.Range(0, 9);
                    }
                }
                valid = false;
                break;
            case 1:
                while (!valid)
                {
                    res = negativeRes(num1, num2);
                    if (res > 0 && res <= 9)
                    {
                        valid = true;
                        EquationText.text = num1 + " - " + num2 + " = ?";
                    }
                    else
                    {
                        num1 = Random.Range(0, 9);
                        num2 = Random.Range(0, 9);
                    }
                }
                valid = false;
                break;
        }
    }

    private int positiveRes(int num1, int num2)
    {
        res = num1 + num2;
        return res;
    }

    private int negativeRes(int num1, int num2)
    {
        res = num1 - num2;
        return res;
    }

    private void verify(int num)
    {
        if (res == num && started && !finished)
        {
            points++;
            UpdateLights(points);
            CreateEquation();
        }
        else if (started && !finished)
        {
            points = 0;
            UpdateLights(points);
            CreateEquation();
            timeRemaining = maxTime;
        }
        if (finished)
        {
            EquationText.text = "Concluído!";
        }
    }

    public void Button1Press()
    {
        verify(1);
    }
    public void Button2Press()
    {
        verify(2);
    }
    public void Button3Press()
    {
        verify(3);
    }
    public void Button4Press()
    {
        verify(4);
    }
    public void Button5Press()
    {
        verify(5);
    }
    public void Button6Press()
    {
        verify(6);
    }
    public void Button7Press()
    {
        verify(7);
    }
    public void Button8Press()
    {
        verify(8);
    }
    public void Button9Press()
    {
        verify(9);
    }


}
