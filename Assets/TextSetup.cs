using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSetup : MonoBehaviour
{
    public Transform row;

    public void StartSetup()
    {
        Config config = GameObject.Find("GameController").GetComponent<Config>();
        Narration narration = new Narration(config.currentAct, config.currentLevel);

        int charactersPerLine = config.charactersPerLine;
        int numberOfTotalLines = narration.text.Length / charactersPerLine;

        for (int i = 0; i < numberOfTotalLines; i++)
        {
            float zPosition = -18f * i;
            GameObject currentRow = Instantiate(row, gameObject.transform).gameObject;
            currentRow.transform.localPosition = new Vector3(0f, 0f, zPosition);
            currentRow.GetComponent<Row>().Setup(i);
        }
    }
}
