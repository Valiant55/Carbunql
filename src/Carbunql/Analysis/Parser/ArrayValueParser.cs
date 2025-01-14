﻿using Carbunql.Clauses;
using Carbunql.Extensions;
using Carbunql.Values;

namespace Carbunql.Analysis.Parser;

public static class ArrayValueParser
{
	public static ArrayValue Parse(string argument)
	{
		using var r = new TokenReader(argument);
		return Parse(r);
	}

	public static ArrayValue Parse(ITokenReader r)
	{
		r.Read("array");
		using var ir = new BracketInnerTokenReader(r, "[", "]");

		var collection = ValueCollectionParser.Parse(ir);
		return new ArrayValue(collection);
	}
}