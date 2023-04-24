using Dapper;
using OnionBaseArchitecture.Application.Abstractions;
using OnionBaseArchitecture.Application.Abstractions.Repositories;
using OnionBaseArchitecture.Domain.Attibutes;
using OnionBaseArchitecture.Domain.Entities.Common;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;

namespace OnionBaseArchitecture.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private IConnectionManager _conn;
        private const int _commandTimeout = 60;

        public WriteRepository(IConnectionManager connectionManager)
        {
            _conn = connectionManager;
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            var columns = GetColumns();
            var stringOfColumns = string.Join(", ", columns.Select(x => $"[{x.Item1}]")).Replace(", [TotalRowCount]", "");
            var stringOfParameters = string.Join(", ", columns.Select(x => "@" + x.Item2)).Replace(", @TotalRowCount", "");
            var tableAttribute = typeof(T).GetCustomAttribute<TableName>();
            //entity.InsertDate = DateTime.Now;

            var query = $@"SET NOCOUNT ON;
                Insert Into [{tableAttribute.SchemeName}].[{tableAttribute.Name}] ({stringOfColumns}) OUTPUT inserted.Id VALUES ({stringOfParameters})";

            using (var con = _conn.Connection)
            {
                entity.Id = await con.QueryFirstOrDefaultAsync<Guid>(query, entity, commandType: CommandType.Text);
            }

            return entity;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            var columns = GetColumns();
            var tableAttribute = typeof(T).GetCustomAttribute<TableName>();

            var query = GenerateUpdateQuery(tableAttribute.Name);

            using (var con = _conn.Connection)
            {
                return await con.ExecuteAsync(query, entity, commandType: CommandType.Text) > 0;
            }
        }

        public virtual async Task<bool> DeleteByIdAsync(string Id)
        {
            var _tableName = typeof(T).GetCustomAttribute<TableName>();

            using (var con = _conn.Connection)
            {
                //return await con.ExecuteAsync($"DELETE FROM {_tableName.Name} WHERE Id = @Id", new { Id = Id });
                return await con.ExecuteAsync($"Update {_tableName.Name} SET IsDeleted = 1 WHERE Id =  @Id", new { Id = Id }) > 0;
            }
        }

        public virtual async Task<Tuple<bool, string>> BulkInsertAsync(List<T> entities)
        {
            Tuple<bool, string> result = new Tuple<bool, string>(false, "");

            var columns = GetColumns();
            var stringOfColumns = string.Join(", ", columns.Select(x => $"[{x.Item1}]")).Replace(", [TotalRowCount]", "");
            var stringOfParameters = string.Join(", ", columns.Select(x => "@" + x.Item2)).Replace(", @TotalRowCount", "");
            var tableAttribute = typeof(T).GetCustomAttribute<TableName>();
            //entity.InsertDate = DateTime.Now;

            var query = $@"Insert Into [{tableAttribute.SchemeName}].[{tableAttribute.Name}] ({stringOfColumns}) VALUES ({stringOfParameters})";

            using (var con = _conn.Connection)
            {
                using (var trans = con.BeginTransaction())
                {
                    try
                    {
                        foreach (var entity in entities)
                        {
                            await con.ExecuteAsync(query, entity, transaction: trans, commandType: CommandType.Text);
                        }

                        trans.Commit();

                        result = new Tuple<bool, string>(true, "");
                    }
                    catch (Exception ex)
                    {
                        result = new Tuple<bool, string>(false, ex.Message);

                        trans.Rollback();
                    }
                }
            }

            return result;
        }

        public virtual async Task<Tuple<bool, string>> BulkUpdateAsync(List<T> entities)
        {
            Tuple<bool, string> result = new Tuple<bool, string>(false, "");

            var tableAttribute = typeof(T).GetCustomAttribute<TableName>();
            var query = GenerateUpdateQuery(tableAttribute.Name);
            query = query.Replace("Key=", "[Key]=");
            using (var con = _conn.Connection)
            {
                using (var trans = con.BeginTransaction())
                {
                    try
                    {
                        foreach (var entity in entities)
                        {
                            await con.ExecuteAsync(query, entity, transaction: trans, commandType: CommandType.Text);
                        }

                        trans.Commit();

                        result = new Tuple<bool, string>(true, "");
                    }
                    catch (Exception ex)
                    {
                        result = new Tuple<bool, string>(false, ex.Message);

                        trans.Rollback();
                    }
                }
            }

            return result;
        }

        private IEnumerable<Tuple<string, string>> GetColumns()
        {
            var columns = new List<Tuple<string, string>>();
            var list = typeof(T).GetProperties().Where(e => e.Name != "Id");
            foreach (var column in list)
            {
                var ignore = column.GetCustomAttribute<IgnoreParameter>();
                var columnInfo = column.GetCustomAttribute<ColumnName>();

                if (ignore == null)
                {
                    if (columnInfo != null)
                    {
                        columns.Add(new Tuple<string, string>(columnInfo.Name, column.Name));
                    }
                    else
                    {
                        columns.Add(new Tuple<string, string>(column.Name, column.Name));
                    }
                }
            }
            return columns;
        }

        private string GenerateUpdateQuery(string _tableName)
        {
            IEnumerable<PropertyInfo> GetProperties = typeof(T).GetProperties();

            var updateQuery = new StringBuilder($"UPDATE [{_tableName}] SET ");
            var properties = GenerateListOfProperties(GetProperties);

            properties.ForEach(property =>
            {
                if (!property.Equals("Id"))
                {
                    updateQuery.Append($"[{property}]=@{property},");
                }
            });

            updateQuery.Remove(updateQuery.Length - 1, 1);
            updateQuery.Append(" WHERE Id=@Id");

            return updateQuery.ToString();
        }

        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where (attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore") && prop.Name != "Id"
                    select prop.Name).ToList();
        }

    }
}
