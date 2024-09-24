﻿using System.Text;

using SqliteDbWrapper.Queries;

namespace SqliteDbWrapper.Queries.SimpleSelectQuery
{
	/// <summary>
	/// The default select query. Supports the following: Distinct, Top, Where, Group By, Order By ASC/DESC.
	/// This uses the Builder pattern, though the client is free to customize however they wish.
	/// </summary>
	public class SimpleSqliteDbSelectQuery : ISqliteDbQuery
	{
		private readonly SimpleSqliteDbSelectQueryState _state = new();

		public SimpleSqliteDbSelectQuery Select(string pFields, bool pIsAppend = false)
		{
			_state.Fields = Utils.StringUtils.SetOrAppend(_state.Fields, pFields, pIsAppend);
			return this;
		}

		public SimpleSqliteDbSelectQuery Distinct(bool pIsDistinct = true)
		{
			_state.IsDistinct = pIsDistinct;
			return this;
		}

		public SimpleSqliteDbSelectQuery Top(int pTop)
		{
			_state.Top = pTop;
			return this;
		}

		public SimpleSqliteDbSelectQuery Where(string pConditions, bool pIsAppend = false)
		{
			_state.Where = Utils.StringUtils.SetOrAppend(_state.Where, pConditions, pIsAppend);
			return this;
		}

		public SimpleSqliteDbSelectQuery GroupBy(string pGroups, bool pIsAppend = false)
		{
			_state.GroupBy = Utils.StringUtils.SetOrAppend(_state.GroupBy, pGroups, pIsAppend);
			return this;
		}

		public SimpleSqliteDbSelectQuery OrderBy(string pOrder, bool pIsAppend = false)
		{
			_state.OrderBy = Utils.StringUtils.SetOrAppend(_state.OrderBy, pOrder, pIsAppend);
			return this;
		}

		public SimpleSqliteDbSelectQuery Asc(bool pIsAsc)
		{
			_state.IsAsc = pIsAsc;
			return this;
		}

		#region "ISqliteDbSelectQuery"
		public string BuildQuery(string pTableName)
		{
			StringBuilder queryBuilder = new();

			// SELECT FIELDS FROM TABLE
			queryBuilder.Append("SELECT ");
			if (_state.IsDistinct != null && _state.IsDistinct.Value)
			{
				queryBuilder.Append($"DISTINCT ");
			}

			if (_state.Top != null && _state.Top > 0)
			{
				queryBuilder.Append($"TOP {_state.Top} ");
			}

			queryBuilder.Append(string.IsNullOrWhiteSpace(_state.Fields) ? "* " : $"{_state.Fields} ");
			queryBuilder.Append($"FROM {pTableName} ");

			// WHERE X GROUP BY Y ORDER BY Z
			if (!string.IsNullOrWhiteSpace(_state.Where))
			{
				queryBuilder.Append($"WHERE {_state.Where} ");
			}

			if (!string.IsNullOrWhiteSpace(_state.GroupBy))
			{
				queryBuilder.Append($"GROUP BY {_state.GroupBy} ");
			}

			if (!string.IsNullOrWhiteSpace(_state.OrderBy))
			{
				queryBuilder.Append($"ORDER BY {_state.OrderBy} ");
				if (_state.IsAsc != null)
				{
					queryBuilder.Append(_state.IsAsc.Value ? "ASC" : "DESC");
				}
			}

			return queryBuilder.ToString();
		}
		#endregion
	}
}
