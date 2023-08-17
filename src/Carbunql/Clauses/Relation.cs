﻿using Carbunql.Extensions;
using Carbunql.Tables;

namespace Carbunql.Clauses;

public class Relation : IQueryCommandable
{
	public Relation(SelectableTable query, string types)
	{
		Table = query;
		TableJoin = types;
	}

	public Relation(SelectableTable query, string types, ValueBase condition)
	{
		Table = query;
		TableJoin = types;
		Condition = condition;
	}

	public string TableJoin { get; init; }

	public ValueBase? Condition { get; set; }

	public SelectableTable Table { get; init; }

	public IEnumerable<SelectQuery> GetInternalQueries()
	{
		foreach (var item in Table.GetInternalQueries())
		{
			yield return item;
		}
		if (Condition != null)
		{
			foreach (var item in Condition.GetInternalQueries())
			{
				yield return item;
			}
		}
	}

	public IEnumerable<PhysicalTable> GetPhysicalTables()
	{
		foreach (var item in Table.GetPhysicalTables())
		{
			yield return item;
		}
		if (Condition != null)
		{
			foreach (var item in Condition.GetPhysicalTables())
			{
				yield return item;
			}
		}
	}

	public IDictionary<string, object?> GetParameters()
	{
		var prm = EmptyParameters.Get();
		prm = prm.Merge(Table.GetParameters());
		return prm.Merge(Condition?.GetParameters());
	}

	public IEnumerable<Token> GetTokens(Token? parent)
	{
		yield return Token.Reserved(this, parent, TableJoin);
		foreach (var item in Table.GetTokens(parent)) yield return item;

		if (Condition != null)
		{
			yield return Token.Reserved(this, parent, "on");
			foreach (var item in Condition.GetTokens(parent)) yield return item;
		}
	}
}