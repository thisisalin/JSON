﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JSON
{

    public class Character : IPattern
    {

        readonly char pattern;

        public Character(char pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return new Match(false, text);
            }

            return new Match(text[0] == pattern, text.Substring(1));

        }

    }
}
