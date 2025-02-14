﻿using Carbunql.Clauses;
using Carbunql.Extensions;
using Carbunql.Tables;

namespace Carbunql;

public class UpdateQuery : IQueryCommandable, IReturning
{
	public UpdateClause? UpdateClause { get; set; }

	public SetClause? SetClause { get; set; }

	public FromClause? FromClause { get; set; }

	public WhereClause? WhereClause { get; set; }

	public ReturningClause? ReturningClause { get; set; }

	public IEnumerable<SelectQuery> GetInternalQueries()
	{
		if (UpdateClause != null)
		{
			foreach (var item in UpdateClause.GetInternalQueries())
			{
				yield return item;
			}
		}
		if (SetClause != null)
		{
			foreach (var value in SetClause.Items)
			{
				foreach (var item in value.GetInternalQueries())
				{
					yield return item;
				}
			}
		}
		if (FromClause != null)
		{
			foreach (var item in FromClause.GetInternalQueries())
			{
				yield return item;
			}
		}
		if (WhereClause != null)
		{
			foreach (var item in WhereClause.GetInternalQueries())
			{
				yield return item;
			}
		}
		if (ReturningClause != null)
		{
			foreach (var item in ReturningClause.GetInternalQueries())
			{
				yield return item;
			}
		}
	}

	public IEnumerable<PhysicalTable> GetPhysicalTables()
	{
		if (UpdateClause != null)
		{
			foreach (var item in UpdateClause.GetPhysicalTables())
			{
				yield return item;
			}
		}
		if (SetClause != null)
		{
			foreach (var value in SetClause.Items)
			{
				foreach (var item in value.GetPhysicalTables())
				{
					yield return item;
				}
			}
		}
		if (FromClause != null)
		{
			foreach (var item in FromClause.GetPhysicalTables())
			{
				yield return item;
			}
		}
		if (WhereClause != null)
		{
			foreach (var item in WhereClause.GetPhysicalTables())
			{
				yield return item;
			}
		}
		if (ReturningClause != null)
		{
			foreach (var item in ReturningClause.GetPhysicalTables())
			{
				yield return item;
			}
		}
	}

	public IEnumerable<CommonTable> GetCommonTables()
	{
		if (UpdateClause != null)
		{
			foreach (var item in UpdateClause.GetCommonTables())
			{
				yield return item;
			}
		}
		if (SetClause != null)
		{
			foreach (var value in SetClause.Items)
			{
				foreach (var item in value.GetCommonTables())
				{
					yield return item;
				}
			}
		}
		if (FromClause != null)
		{
			foreach (var item in FromClause.GetCommonTables())
			{
				yield return item;
			}
		}
		if (WhereClause != null)
		{
			foreach (var item in WhereClause.GetCommonTables())
			{
				yield return item;
			}
		}
		if (ReturningClause != null)
		{
			foreach (var item in ReturningClause.GetCommonTables())
			{
				yield return item;
			}
		}
	}

	public IDictionary<string, object?>? Parameters { get; set; }

	public virtual IDictionary<string, object?> GetParameters()
	{
		var prm = EmptyParameters.Get();
		prm = prm.Merge(SetClause?.GetParameters());
		prm = prm.Merge(FromClause?.GetParameters());
		prm = prm.Merge(WhereClause?.GetParameters());
		prm = prm.Merge(Parameters);
		prm = prm.Merge(ReturningClause?.GetParameters());
		return prm;
	}

	public IEnumerable<Token> GetTokens(Token? parent)
	{
		if (UpdateClause == null) throw new NullReferenceException();
		if (SetClause == null) throw new NullReferenceException();

		foreach (var item in UpdateClause.GetTokens(parent)) yield return item;
		foreach (var item in SetClause.GetTokens(parent)) yield return item;
		if (FromClause != null) foreach (var item in FromClause.GetTokens(parent)) yield return item;
		if (WhereClause != null) foreach (var item in WhereClause.GetTokens(parent)) yield return item;

		if (ReturningClause == null) yield break;
		foreach (var item in ReturningClause.GetTokens(parent)) yield return item;
	}
}