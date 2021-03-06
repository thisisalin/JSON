﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JSON
{
    public class ValueFacts
    {
        static Value value = new Value();

        [Theory]
        [InlineData("12.123E-2", "")]
        [InlineData("12.123E", "E")]
        [InlineData("1E2.123E-2", ".123E-2")]
        [InlineData("12.", ".")]
        [InlineData("\"Te+st\"", "")]
        [InlineData("\"Te st *\"", "")]
        [InlineData("\" \\\\ \"", "")]
        [InlineData("\"\"", "")]
        [InlineData("true", "")]
        [InlineData("false", "")]
        [InlineData("[2]", "")]
        [InlineData("[\t]", "")]
        [InlineData("[\n]", "")]
        [InlineData("[\r]", "")]
        [InlineData("[ ]", "")]
        [InlineData("[\r24]", "")]
        [InlineData("[324]", "")]
        [InlineData("[1,2]", "")]
        [InlineData(" [ 1,2 ] ", "")]
        [InlineData("\"d\"", "")]
        [InlineData("\"da\"", "")]
        [InlineData("[true,false]", "")]
        [InlineData("{\"name\":\"John\",\"age\":30,\"car\":null}", "")]
        [InlineData("{\"boolean\":\"true\"}", "")]
        [InlineData("{\"First Number\":\"12\",\"Second Number\":\"12.5\",\"Third Number\":\"12.123E+3\"}", "")]
        [InlineData("{}", "")]
        [InlineData("[true,false,true,false]", "")]
        [InlineData("{\t}", "")]
        [InlineData("[true ]", "")]
        


        public void ReturnTrue(string pattern, string remainingText)
        {
            IMatch match = value.Match(pattern);
            Assert.Equal(remainingText, match.RemainingText());
            Assert.True(match.Success());
        }


        [Theory]
        [InlineData("[truefalse]", 5)]
        [InlineData("[true false]", 6)]
        [InlineData("[true,false true]", 12)]
        [InlineData("[true,false", 11)]
        [InlineData("\"john", 5)]
        [InlineData("john\"", 0)]
        [InlineData("{\"name\"\"John\"}", 7)]
        [InlineData(" { \" name\" \"John\" } ", 11)]
        [InlineData("{\"name\":\"John\"", 14)]
        [InlineData("{\"name\":\"John\",\"age\":30,\"cars\"[\"Ford\", \"BMW\",\"Fiat\"]}", 30)]
        [InlineData("[{\"id\"\"1\"}]",6)]
        [InlineData("{\"menu\":{\"id\":\"file\",\"value\":\"File\",\"popup\":{\"menuitem\":[{\"value\":\"New\",\"onclick\"\"CreateNewDoc()\"]}}}", 81)]
        [InlineData("{\"menu\" : {\"id\" \"file\"}}", 16)]
        [InlineData("{ \"menu\" : { \"id\" \"file\" } }", 18)]
        [InlineData("          [  \"S\\ufefhi]", 20)]

        public void ReturnFalse(string pattern, int position)
        {
            var result = (Error)value.Match(pattern);
            Assert.Equal(position, result.Position());
           
            //Assert.Equal("", result.RemainingText());

        }


    }
}
