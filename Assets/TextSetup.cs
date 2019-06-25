using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSetup : MonoBehaviour
{
    GameObject gameController;
    public Transform row;
    private string narration = "";
    private int numberOfLines = 0;
    private int charactersPerLine = 0;

    public void StartSetup()
    {
        gameController = GameObject.Find("GameController").gameObject;
        narration = gameController.GetComponent<Config>().narration.Replace(" ", "");
        narration = narration.ToUpper();
        numberOfLines = gameController.GetComponent<Config>().numberOfLines;
        charactersPerLine = gameController.GetComponent<Config>().charactersPerLine;

        print(narration);
        (int restCount, string restText) = Count(narration);
        if (restCount > 0)
        {
            print("Text was too long, " + restCount + " characters exceeded. Rest of text: " + restText);
        }
        for (int i = 0; i < numberOfLines; i++)
        {
            string line = CreateLine(i, narration);
            float yPosition = 0f + (200f * -i);
            GameObject currentRow = Instantiate(row, gameObject.transform).gameObject;
            currentRow.transform.localPosition = new Vector3(0f, yPosition, 0f);
            currentRow.GetComponent<Row>().Setup(line, i);
        }
    }

    private (int, string) Count(string text)
    {
        int count = 0;
        string rest = "";
        int fullTextMaxLength = numberOfLines * charactersPerLine;

        if (text.Length > fullTextMaxLength)
        {
            rest = text.Substring(fullTextMaxLength);
            count = text.Length - fullTextMaxLength;
        }
        return (count, rest);
    }
    private string CreateLine(int index, string fullText)
    {
        int startIndex = index * charactersPerLine;

        return fullText.Substring(startIndex, charactersPerLine);
    }
}
