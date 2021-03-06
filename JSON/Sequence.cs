﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JSON
{
    public class Sequence : IPattern
    {

        //Clasa Sequance reprezintă o secvență de pattern-uri. 
        //Dar spre deosebire de Choice toate pattern-urile din secvență trebuie să se potrivească pentru ca secevența să fie validă.
        //Pattern-urile se aplică succesiv unul după altul peste textul care a rămas de la pattern-ul anterior.

        readonly IPattern[] patterns;

        public Sequence(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            string original = text;
            int counter = 0;
            int specialErrorCounter = 0;

            foreach (var pattern in patterns)
            {
                IMatch match = pattern.Match(text);


                if (match is SpecialError specialError )
                {
                    specialErrorCounter += specialError.Position();

                }
                else if (!match.Success())
                {

                    Error error = (Error)match;
                 
                    counter += error.Position();
                    counter += specialErrorCounter;

                    return new Error(counter, original);
                }
                else
                {
                    if (!String.IsNullOrEmpty(text))
                        counter += text.Length - match.RemainingText().Length;
                }

                text = match.RemainingText();
            }



            return new Match(true, text);
        }

    }
}
