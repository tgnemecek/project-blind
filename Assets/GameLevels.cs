using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevels : MonoBehaviour
{
    public enum Act { Act1, Act2, Act3 }
    public Act currentAct = Act.Act1;

    public int currentLevel = 0;
    public string narration = "";

    void Awake()
    {
        GetNarration();
    }

    public void NextLevel()
    {
        currentLevel++;
        GetNarration();
    }


    void GetNarration()
    {
        switch(currentLevel)
        {
            case 0:
                narration = "abdeadbeadbeabdebadbeadbaababababdeddd";
                break;
        }
    }


}