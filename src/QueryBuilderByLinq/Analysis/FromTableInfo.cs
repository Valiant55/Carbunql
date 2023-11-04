﻿using Carbunql;
using Carbunql.Clauses;
using Carbunql.Tables;

namespace QueryBuilderByLinq.Analysis;

public class FromTableInfo
{
	public FromTableInfo(SelectQuery query, string alias)
	{
		Alias = alias;
		Query = query;
	}

	public FromTableInfo(SelectableTable table, string alias)
	{
		Alias = alias;
		Table = table;
	}

	public FromTableInfo(string physicalName, string alias)
	{
		Alias = alias;
		PhysicalName = physicalName;
	}

	public string Alias { get; private set; }

	public string? PhysicalName { get; private set; }

	public SelectableTable? Table { get; private set; }

	public SelectQuery? Query { get; private set; }

	public SelectableTable ToSelectable()
	{
		if (Table != null) return Table;
		if (Query != null) return new VirtualTable(Query).ToSelectable(Alias);
		if (!string.IsNullOrEmpty(PhysicalName)) return new PhysicalTable(PhysicalName).ToSelectable(Alias);
		throw new InvalidProgramException();
	}
}
