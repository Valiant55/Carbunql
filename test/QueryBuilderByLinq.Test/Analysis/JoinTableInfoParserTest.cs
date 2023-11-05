﻿using Carbunql;
using QueryBuilderByLinq.Analysis;
using Xunit.Abstractions;
using static QueryBuilderByLinq.Sql;

namespace QueryBuilderByLinq.Test.Analysis;

public class JoinTableInfoParserTest
{
	private readonly QueryCommandMonitor Monitor;

	public JoinTableInfoParserTest(ITestOutputHelper output)
	{
		Monitor = new QueryCommandMonitor(output);
		Output = output;
	}

	private ITestOutputHelper Output { get; set; }

	[Fact]
	public void NoRelationTest()
	{
		var query = from a in FromTable<sale>()
					select a;

		Monitor.Log(query);

		var joins = JoinTableInfoParser.Parse(query.Expression);

		Assert.Empty(joins);
	}

	[Fact]
	public void InnerJoinTest()
	{
		var query = from s in FromTable<sale>()
					from a in InnerJoinTable<article>(x => s.article_id == x.article_id)
					select a;

		Monitor.Log(query);

		var from = FromTableInfoParser.Parse(query.Expression);
		Assert.Equal("sale", from?.ToSelectable().ToText());
		Assert.Equal("s", from?.Alias);

		var joins = JoinTableInfoParser.Parse(query.Expression);

		Assert.Empty(joins);
	}

	[Fact]
	public void LeftJoinTest()
	{
		var query = from s in FromTable<sale>()
					from a in LeftJoinTable<article>(x => s.article_id == x.article_id)
					select a;

		Monitor.Log(query);

		var from = FromTableInfoParser.Parse(query.Expression);
		Assert.Equal("sale", from?.ToSelectable().ToText());
		Assert.Equal("s", from?.Alias);

		var joins = JoinTableInfoParser.Parse(query.Expression);

		Assert.Empty(joins);
	}

	[Fact]
	public void CrossJoinTest()
	{
		var query = from s in FromTable<sale>()
					from a in CrossJoinTable<article>()
					select a;

		Monitor.Log(query);

		var from = FromTableInfoParser.Parse(query.Expression);
		Assert.Equal("sale", from?.ToSelectable().ToText());
		Assert.Equal("s", from?.Alias);

		var joins = JoinTableInfoParser.Parse(query.Expression);

		Assert.Empty(joins);
	}

	public record struct sale(int sales_id, int article_id, int quantity);
	public record struct article(int article_id, string article_name, int price);
}
