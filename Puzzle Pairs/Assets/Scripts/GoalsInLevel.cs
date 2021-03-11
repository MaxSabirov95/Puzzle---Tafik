using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Goals
{
    public string levelNumber;
    public int secondGoal;
    public int thirdGoal;
}

public class GoalsInLevel : MonoBehaviour
{
    public Goals[] allLevelsGoals;
    public int stars;

    private void Start()
    {
        BlackBoard.goalsInLevel = this;
    }

    //public void CalculateStars(int levelMoves, int level)
    //{
    //    //Debug.Log(levelNum);
    //    if (levelMoves <= allLevelsGoals[level].thirdGoal && levelMoves > 0)
    //    {
    //        stars+=3;
    //        //Debug.Log(levelMoves+" Moves"+" {+3}"+ "   "+ (level + 1)+" Level");
    //    }
    //    else if(levelMoves <= allLevelsGoals[level].secondGoal && levelMoves > 0)
    //    {
    //        stars+=2;
    //        //Debug.Log(levelMoves + " Moves" + " {+2}" + "   " + (level + 1) + " Level");
    //    }
    //    else if(levelMoves > 0)
    //    {
    //        stars++;
    //        //Debug.Log(levelMoves + " Moves" + " {+1}" + "   " + (level + 1) + " Level");
    //    }
    //}
}
