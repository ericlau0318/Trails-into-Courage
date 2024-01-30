using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue 
{
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;

    [TextArea(3, 10)]
    public string[] choice1Sentences;

    [TextArea(3, 10)]
    public string[] choice2Sentences;

    [TextArea(3, 10)]
    public string[] finishSentences;
}
