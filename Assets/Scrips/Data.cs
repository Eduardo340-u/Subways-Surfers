using UnityEngine;
 
[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Data")]
public class Data : ScriptableObject
{
    public string jumpAnimationName = "Jump";
    public string moveAnimationName = "Move";
    public string rollAnimationName = "Roll";
    public string loseAnimationName = "Lose";
    public string runAnimationName = "Run";
    public string flyAnimationName = "Fly";
}
 