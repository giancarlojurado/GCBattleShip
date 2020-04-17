using System.Collections.Generic;

namespace GCBattleShip.Domain
{
    public static class Common
    {
        public static Dictionary<string, int> LetterLocationTranslator = new Dictionary<string, int>()
        {
            {"A",1},
            {"B",2},
            {"C",3},
            {"D",4},
            {"E",5},
            {"F",6},
            {"G",7},
            {"H",8},
            {"I",9},
            {"J",10}
        };
        
        //The pattern of location, this is that the first character is a letter between a-j
        // and then numbers between 1 and 10
        public const string LocationPattern  = @"^[A-Ja-j]{1}(10|[1-9][0]?)$";
    }
}