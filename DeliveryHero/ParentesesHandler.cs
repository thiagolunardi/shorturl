using System;
using System.Linq;

namespace DeliveryHero
{
    public class ParentesesHandler
    {
        public bool Validate(string input)
        {
            // shold only have parenteses
            var onlyParenteses = input
                .All(@char => @char == '(' || @char == ')');
            if (!onlyParenteses) return false;

            // just close opening ones
            var opening = 0;
            foreach (var @char in input)
            {
                if (@char == '(')
                    opening++;
                else
                {
                    if (opening <= 0) return false;
                    opening--;
                }
            }

            return opening == 0;
        }
    }
}
