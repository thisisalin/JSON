﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JSON
{
    public class ListFacts
    {
        List a = new List(new OneOrMore(new Range('0', '9')), new Character(','));

        [Theory]
        [InlineData("10111,1,2,3", "")]
        [InlineData("1,2,3,", ",")]
        [InlineData("1a", "a")]
        [InlineData("", "")]
        [InlineData(null, null)]

        public void ReturnsTrueWhenElementIsRangeAndSeparatorIsChar(string pattern, string remainingText)
        {
            Assert.True(a.Match(pattern).Success());
            Assert.Equal(remainingText, a.Match(pattern).RemainingText());
        }

       static OneOrMore digits = new OneOrMore(new Range('0', '9'));
       static Many whitespace = new Many(new Any(" \r\n\t"));
       static Sequence separator = new Sequence(whitespace, new Character(';'), whitespace);
       static List list = new List(digits, separator);

        [Theory]
        [InlineData("1; 22  ;\n 333 \t; 22", "")]
        [InlineData("1 \n;", " \n;")]
        [InlineData("abc", "abc")]
       
        public void ReturnsTrueIfListIsCorrectAndRemainingTest(string pattern, string remainingText)
        {
            Assert.True(list.Match(pattern).Success());
            Assert.Equal(remainingText, list.Match(pattern).RemainingText());
        }
    }
}
