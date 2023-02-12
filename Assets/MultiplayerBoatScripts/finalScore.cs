using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class finalScore : MonoBehaviour
{
    [SerializeField]
    TMP_Text _playername;
    [SerializeField]
    TMP_Text _playerscore;
    
    public void ShowFinalScore(string name,string score) //stores a player name and score to display in the final panel.
    {
        _playername.text = name;
        _playerscore.text = score;
    }
}
