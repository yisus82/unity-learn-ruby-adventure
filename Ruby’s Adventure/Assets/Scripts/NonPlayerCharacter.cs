﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
  public float displayTime = 4.0f;
  public GameObject dialogBox;

  float timerDisplay;

  void Start()
  {
    dialogBox.SetActive(false);
  }

  void Update()
  {
    if (timerDisplay > 0)
    {
      timerDisplay -= Time.deltaTime;
    }
    else
    {
      dialogBox.SetActive(false);
    }
  }

  public void DisplayDialog()
  {
    timerDisplay = displayTime;
    dialogBox.SetActive(true);
  }
}
