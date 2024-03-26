using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New MiniGame", menuName ="Scriptable/MiniGame")]
public class MiniGameDATA : ScriptableObject
{
    public string minigameName; //Nome do minigame
    public Sprite minigameIcon; //Icone do minigame
}
