﻿namespace Carbunql.Clauses;

public class OrderClause : QueryCommandCollection<SortableItem>, IQueryCommand
{
    public OrderClause() : base()
    {
    }

    public OrderClause(List<SortableItem> collection) : base(collection)
    {
    }

    public override IEnumerable<Token> GetTokens(Token? parent)
    {
        if (!Items.Any()) yield break;

        var clause = Token.Reserved(this, parent, "order by");
        yield return clause;

        foreach (var item in base.GetTokens(clause)) yield return item;
    }
}