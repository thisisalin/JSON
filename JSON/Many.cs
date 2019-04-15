﻿using System;

using System.Collections.Generic;

using System.Text;


namespace JSON

{
    public class Many : IPattern
    {
        readonly IPattern pattern;
        public Many(IPattern pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            string original = text;
            if (!String.IsNullOrEmpty(text))
            {
                IMatch match;
                foreach (char c in text)
                {
                    match = pattern.Match(text);
                    if (match.Success())
                        text = match.RemainingText();
                }
                return new Match(true, text);
            }
            return new Match(true, original);

        }

    }
}