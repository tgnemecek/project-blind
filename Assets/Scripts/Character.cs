using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
  public string Name;
  public bool[][] Nodes;
  public Character(string name)
    {
        Name = name;
        GetNodesArray();
    }

  void GetNodesArray()
  {
    if (Name == "A") {
      Nodes = new bool[][] {
        new bool[] {true, false},
        new bool[] {false, false},
        new bool[] {false, false},
      };
    } else if (Name == "B") {
      Nodes = new bool[][] {
        new bool[] {true, true},
        new bool[] {false, false},
        new bool[] {false, false},
      };
    } else if (Name == "C") {
      Nodes = new bool[][] {
        new bool[] {true, true},
        new bool[] {true, false},
        new bool[] {true, false},
      };
    } else if (Name == "D") {
      Nodes = new bool[][] {
        new bool[] {false, false},
        new bool[] {false, true},
        new bool[] {true, true},
      };
    } else if (Name == "E") {
      Nodes = new bool[][] {
        new bool[] {false, true},
        new bool[] {false, true},
        new bool[] {false, true},
      };
    } else {
      Nodes = new bool[][] {
        new bool[] {false, false},
        new bool[] {false, false},
        new bool[] {false, false},
      };
    }
  }
}