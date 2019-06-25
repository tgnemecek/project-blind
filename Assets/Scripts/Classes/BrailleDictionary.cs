using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrailleDictionary
{
    public BrailleDictionary()
    {
    }
    public bool[][] GetMatrix(string character)
    {
        switch(character)
        {
            case "A":
                return new bool[][] {
                    new bool[] {true, false},
                    new bool[] {false, false},
                    new bool[] {false, false}
                };
            case "B":
                return new bool[][] {
                    new bool[] {false, true},
                    new bool[] {false, true},
                    new bool[] {false, false}
                };
            case "C":
                return new bool[][] {
                    new bool[] {false, false},
                    new bool[] {false, false},
                    new bool[] {true, true}
                };
            case "D":
                return new bool[][] {
                    new bool[] {true, false},
                    new bool[] {true, false},
                    new bool[] {true, false}
                };
            case "E":
                return new bool[][] {
                    new bool[] {false, false},
                    new bool[] {false, true},
                    new bool[] {false, false}
                };
            default:
                return new bool[][] {
                    new bool[] {false, false},
                    new bool[] {false, false},
                    new bool[] {false, false}
                };
        }
    }
}
