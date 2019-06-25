using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narration
{
    public int act = 0;
    public int level = 0;

    public string text = "";
    public int initialIndex = 0;
    public int length = 0;
    public int lastIndex = 0;

    private struct data
    {
        public string text;
        public int initialIndex;
        public int length;
    }

    private data[][] dataMatrix = {
        // Act 1
        new data[] {
            new data
            {
                text = "acccbcdadbacbabdbabdcdedededbadbdcdedededbadbddcbdcdbbbad",
                initialIndex = 6,
                length = 12
            },
            new data
            {
                text = "babdddcbdcdbbbad",
                initialIndex = 0,
                length = 1
            }
        },
        // Act 2
        new data[] {
            new data
            {
                text = "dedededbadbddcbdcdbbbad",
                initialIndex = 0,
                length = 1
            },
            new data
            {
                text = "bbadbddcbdcabbadbddcbdcadbddcbdcbdddcbdcdbbbad",
                initialIndex = 0,
                length = 1
            }
        },
        // Act 3
        new data[] {
            new data
            {
                text = "dedeabbadbddcbdcadbddcbdcbdddcedbaabbadbddcbdcadbddcbdcbdddcdbddcbdcdbbbad",
                initialIndex = 0,
                length = 1
            },
            new data
            {
                text = "abbadbddabbadbddcbdcadbddcbdcbdddccbdcabbadbddcbdcadbddcbdcbdddcbdcdbbbad",
                initialIndex = 0,
                length = 1
            }
        }
    };

    public Narration(int act, int level)
    {
        this.act = act;
        this.level = level;
        text = dataMatrix[act][level].text;
        text = text.Replace(" ", "").ToUpper();

        initialIndex = dataMatrix[act][level].initialIndex;
        length = dataMatrix[act][level].length;
        lastIndex = initialIndex + length - 1;
    }

    public string GetCharacter(int absoluteIndex)
    {
        return text.Substring(absoluteIndex, 1);
    }

    public string GetTextFromRow(int rowIndex, int charactersPerLine)
    {
        int index = rowIndex * charactersPerLine;
        return text.Substring(index, charactersPerLine);
    }
}
