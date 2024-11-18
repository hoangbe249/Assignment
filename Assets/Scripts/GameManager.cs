using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject start;

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void ButtonStart()
    {
        Time.timeScale = 1;
        start.SetActive(false);
    }
}
