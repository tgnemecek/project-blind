using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    public enum GameState { Loading, Idle, DrawingLine }
    public GameState currentState;

    public int currentAct = 0;
    public int currentLevel = 0;

    public GameObject paragraph;
    
    public int numberOfActiveLines = 4;
    public int charactersPerLine = 5;
    
    void Awake()
    {
        currentState = GameState.Idle;
        paragraph.GetComponent<TextSetup>().StartSetup();
    }
    
    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }
    public void NextLevel()
    {
        currentLevel++;
    }
}