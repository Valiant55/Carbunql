﻿using Carbunql.Clauses;
using Carbunql.Values;

namespace Carbunql.Building;

public static class ValueBaseExtension
{

	public static ValueBase GetLast(this ValueBase source)
	{
		if (source.OperatableValue == null) return source;
		return source.OperatableValue.Value.GetLast();
	}

	public static ValueBase Equal(this ValueBase source, ValueBase operand)
	{
		source.GetLast().AddOperatableValue("=", operand);
		return source;
	}

	public static ValueBase Equal(this ValueBase source, string column)
	{
		return source.Equal(new ColumnValue(column));
	}

	public static ValueBase Equal(this ValueBase source, object value)
	{
		var v = value.ToString();
		if (v == null) return source.IsNull();
		return source.Equal(new LiteralValue(v));
	}

	public static ValueBase Equal(this ValueBase source, string table, string column)
	{
		return source.Equal(new ColumnValue(table, column));
	}

	public static ValueBase Equal(this ValueBase source, SelectableTable table, string column)
	{
		return source.Equal(new ColumnValue(table, column));
	}

	public static ValueBase NotEqual(this ValueBase source, ValueBase operand)
	{
		source.GetLast().AddOperatableValue("<>", operand);
		return source;
	}

	public static ValueBase NotEqual(this ValueBase source, string column)
	{
		return source.NotEqual(new ColumnValue(column));
	}

	public static ValueBase NotEqual(this ValueBase source, string table, string column)
	{
		return source.NotEqual(new ColumnValue(table, column));
	}

	public static ValueBase NotEqual(this ValueBase source, SelectableTable table, string column)
	{
		return source.NotEqual(new ColumnValue(table, column));
	}

	public static ValueBase IsNull(this ValueBase source)
	{
		source.GetLast().AddOperatableValue("is", new LiteralValue("null"));
		return source;
	}

	public static ValueBase IsNotNull(this ValueBase source)
	{
		source.GetLast().AddOperatableValue("is not", new LiteralValue("null"));
		return source;
	}

	public static ValueBase True(this ValueBase source)
	{
		source.GetLast().AddOperatableValue("=", new LiteralValue("true"));
		return source;
	}

	public static ValueBase False(this ValueBase source)
	{
		source.GetLast().AddOperatableValue("=", new LiteralValue("false"));
		return source;
	}

	public static ValueBase Expression(this ValueBase source, string @operator, ValueBase operand)
	{
		source.GetLast().AddOperatableValue(@operator, operand);
		return source;
	}

	public static ValueBase Expression(this ValueBase source, string @operator, Func<ValueBase> builder)
	{
		source.GetLast().AddOperatableValue(@operator, builder());
		return source;
	}

	public static ValueBase And(this ValueBase source, ValueBase operand)
	{
		source.GetLast().AddOperatableValue("and", operand);
		return source;
	}

	public static ValueBase And(this ValueBase source, Func<ValueBase> builder)
	{
		return source.And(builder());
	}

	public static ValueBase Or(this ValueBase source, ValueBase operand)
	{
		source.GetLast().AddOperatableValue("or", operand);
		return source;
	}

	public static ValueBase Or(this ValueBase source, Func<ValueBase> builder)
	{
		return source.Or(builder());
	}

	public static ValueBase ToGroup(this ValueBase source)
	{
		return new BracketValue(source);
	}

	public static SortableItem ToSortable(this ValueBase source, bool isAscending = true)
	{
		return new SortableItem(source) { IsAscending = isAscending };
	}

	public static ValueBase Merge(this IEnumerable<ValueBase> source, string @operator)
	{
		ValueBase? v = null;

		foreach (var item in source)
		{
			if (v == null)
			{
				v = item;
				continue;
			}
			v.GetLast().AddOperatableValue(@operator, item);
		}

		if (v == null) throw new NullReferenceException();
		return v;
	}

	public static ValueBase MergeAnd(this IEnumerable<ValueBase> source)
	{
		return Merge(source, "and");
	}

	public static ValueBase MergeOr(this IEnumerable<ValueBase> source)
	{
		return Merge(source, "or");
	}
}