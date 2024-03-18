﻿using System;

namespace ikatic.StringMetrics
{
    public class JaroWinkler : Algorithm, IAlgorithm
    {
        public override double Compare(string pattern, string target)
        {
            //base.Init will initilize the base class 
            //and it will also validate both strings
            //if not valid it will set the distance and score appropriatly
            if (!base.Init(pattern, target)) { Stop(); return Score; };

            //valid length pattern and target - compare bsm Jaro-Winkler method:
            Jaro jaro = new Jaro();
            double jaroScore = jaro.Compare(pattern, target);
            Score = jaroScore + (GetCommonPrefix(pattern, target, 4) * 0.1d * (1 - jaroScore));

            //Don't let the score drop below the min score:
            if (Score < base.MinScore)
                Score = base.MinScore;

            Distance = base.MaxScore - Score;

            Stop();
            return Score;
        }
    }
}