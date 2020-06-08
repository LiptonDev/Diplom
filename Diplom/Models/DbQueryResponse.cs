namespace Diplom.Models
{
    /// <summary>
    /// Результат запроса к базе.
    /// </summary>
    class DbQueryResponse<TResult> : DbQueryResponse
    {
        /// <summary>
        /// Результат запроса.
        /// </summary>
        public TResult Result { get; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public DbQueryResponse(TResult result, DbQueryResult queryResult) : base(queryResult)
        {
            Result = result;
        }
    }

    /// <summary>
    /// Результат запроса к базе.
    /// </summary>
    class DbQueryResponse
    {
        /// <summary>
        /// Результат запроса к базе данных.
        /// </summary>
        public DbQueryResult QueryResult { get; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public DbQueryResponse(DbQueryResult queryResult)
        {
            QueryResult = queryResult;
        }

        public static implicit operator bool(DbQueryResponse response) => response.QueryResult == DbQueryResult.Ok;
    }
}
