using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    public enum GameState { Loading, Idle, DrawingLine }
    public GameState currentState;

    public GameObject paragraph;

    public string narration = "";
    public int numberOfLines = 4;
    public int charactersPerLine = 5;

    // Start is called before the first frame update
    void Awake()
    {
        currentState = GameState.Idle;
        paragraph.GetComponent<TextSetup>().StartSetup();
    }

    // Update is called once per frame
    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }
}