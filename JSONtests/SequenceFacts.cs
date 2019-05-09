﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JSON
{
    public class SequenceFacts
    {

        static Sequence ab = new Sequence(new Character('a'), new Character('b'));
        static Sequence abcde = new Sequence(ab, new Character('c'), new Character('d'), new Character('e'), new Character('e'));

        [Theory]
        [InlineData("abcd", "cd")]
       
        public void ReturnsFalseWhenABiscorrect(string pattern, string remainingText)
        {
            Assert.True(ab.Match(pattern).Success());
            Assert.Equal(remainingText, ab.Match(pattern).RemainingText());

        }

        [Theory]
        [InlineData("def", "def")]
        [InlineData("", "")]
        [InlineData(null, null)]

        public void ReturnsFalseWhenABisIncorrect(string pattern, string remainingText)
        {
            Assert.False (ab.Match(pattern).Success());
            Assert.Equal(remainingText, ab.Match(pattern).RemainingText());

        }

       

       static Choice hex = new Choice(
    new Range('0', '9'),
    new Range('a', 'f'),
    new Range('A', 'F')
    );

       static Sequence hexSeq = new Sequence(
    new Character('u'),
    new Sequence(
        hex,
        hex,
        hex,
        hex));

        [Theory]
        [InlineData("u1234", "")]
        [InlineData("uabcdef", "ef")]
        [InlineData("uB005 ab", " ab")]

        public void ReturnsFalseWhenHexSeqIscorrect(string pattern, string remainingText)
        {
            Assert.True(hexSeq.Match(pattern).Success());
            Assert.Equal(remainingText, hexSeq.Match(pattern).RemainingText());

        }

        [Theory]
        [InlineData("abc", "abc")]
        [InlineData(null, null)]
       

        public void ReturnsFalseWhenHexSeqIsIncorrect(string pattern, string remainingText)
        {
            Assert.False(hexSeq.Match(pattern).Success());
            Assert.Equal(remainingText, hexSeq.Match(pattern).RemainingText());

        }

        [Theory]
        [InlineData("abcdfff", "abcdfff")]

        public void ReturnsFalseWhenABCisInCorrect(string pattern, string remainingText)
        {
            var error = (Error)abcde.Match(pattern);
            Assert.False(abcde.Match(pattern).Success());
            Assert.Equal(4, error.Position());
            Assert.Equal(remainingText, abcde.Match(pattern).RemainingText());

        }

        static Sequence test1 = new Sequence(new Text("abc"), new Character('d'));

        [Theory]
        [InlineData("abcx", "abcx")]

        public void ReturnsFalseWhenTest1isInCorrect(string pattern, string remainingText)
        {
            var error = (Error)abcde.Match(pattern);
            Assert.False(test1.Match(pattern).Success());
            Assert.Equal(3, error.Position());
            Assert.Equal(remainingText, test1.Match(pattern).RemainingText());

        }
    }

}

