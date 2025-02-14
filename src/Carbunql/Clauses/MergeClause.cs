﻿using Carbunql.Tables;

namespace Carbunql.Clauses;

public class MergeClause : IQueryCommandable
{
	public MergeClause(SelectableTable table)
	{
		Table = table;
	}

	public SelectableTable Table { get; init; }

	public IDictionary<string, object?> GetParameters()
	{
		return Table.GetParameters();
	}

	public IEnumerable<SelectQuery> GetInternalQueries()
	{
		foreach (var item in Table.GetInternalQueries())
		{
			yield return item;
		}
	}

	public IEnumerable<PhysicalTable> GetPhysicalTables()
	{
		foreach (var item in Table.GetPhysicalTables())
		{
			yield return item;
		}
	}

	public IEnumerable<CommonTable> GetCommonTables()
	{
		foreach (var item in Table.GetCommonTables())
		{
			yield return item;
		}
	}

	public IEnumerable<Token> GetTokens(Token? parent)
	{
		var t = Token.Reserved(this, parent, "merge into");
		yield return t;
		foreach (var item in Table.GetTokens(t)) yield return item;
	}
}