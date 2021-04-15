using Common.Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Helpers;
using System.Data.SqlClient;

namespace Repository.Realization
{
    public class WorkerRepository : IRepository<Worker>
    {

        private readonly string connectionString;
        private readonly static string TableName;
        private readonly static string IdField;
        private readonly static string LastNameField;
        private readonly static string FirstNameField;
        private readonly static string PatronymicNameField;
        private readonly static string DateReceiptField;
        private readonly static string PositionField;
        private readonly static string CompanyIDField;

        private readonly static string SelectAllCommand;
        private readonly static string SelectByIdCommand;
        private readonly static string SelectByAllFieldCommand;
        private readonly static string DeleteCommand;
        private readonly static string InsertCommand;
        private readonly static string UpdateCommand;


        static WorkerRepository() {
            TableName = AttributeHelper.GetAttribute<Worker, TableAttribute>().Name;
            IdField = AttributeHelper.GetAttribute<Worker, ColumnAttribute>(nameof(Worker.ID)).Name;
            LastNameField = AttributeHelper.GetAttribute<Worker, ColumnAttribute>(nameof(Worker.LastName)).Name;
            FirstNameField = AttributeHelper.GetAttribute<Worker, ColumnAttribute>(nameof(Worker.FirstName)).Name;
            PatronymicNameField = AttributeHelper.GetAttribute<Worker, ColumnAttribute>(nameof(Worker.Patronymic)).Name;
            DateReceiptField = AttributeHelper.GetAttribute<Worker, ColumnAttribute>(nameof(Worker.DateReceipt)).Name;
            PositionField = AttributeHelper.GetAttribute<Worker, ColumnAttribute>(nameof(Worker.Position)).Name;
            CompanyIDField = AttributeHelper.GetAttribute<Worker, ColumnAttribute>(nameof(Worker.CompanyID)).Name;
            SelectAllCommand = String.Format("select {0}, {1}, {2}, {3}, {4}, {5}, {6} from {7}",
                 IdField, LastNameField, FirstNameField, PatronymicNameField, DateReceiptField, PositionField, CompanyIDField, TableName);
            SelectByIdCommand = String.Format("{0} where {1}={2}", SelectAllCommand, IdField, "{0}");
            SelectByAllFieldCommand = String.Format("{0} where {1}='{2}' and {3}='{4}' and {5}='{6}' and {7}='{8}' and {9}='{10}' and {11}='{12}'", 
                SelectAllCommand, LastNameField, "{0}", FirstNameField, "{1}",PatronymicNameField,"{2}",DateReceiptField,"{3}",PositionField,"{4}",CompanyIDField,"{5}");
            InsertCommand = String.Format("INSERT INTO {0} ({1},{2},{3},{4},{5},{6}) values ('{7}', '{8}', '{9}', '{10}', '{11}', '{12}')", 
                TableName, LastNameField, FirstNameField,PatronymicNameField,DateReceiptField,PositionField,CompanyIDField,
                "{0}", "{1}", "{2}", "{3}", "{4}", "{5}", "{6}");
            DeleteCommand = String.Format("DELETE FROM {0} WHERE {1}='{2}'", TableName, IdField, "{0}");
            UpdateCommand = String.Format("UPDATE {0} SET {1}='{2}', {3}='{4}', {5}='{6}', {7}='{8}', {9}='{10}', {11}='{12}' where {13}='{14}'", 
                TableName, 
                LastNameField, "{0}", FirstNameField, "{1}", PatronymicNameField, "{2}", DateReceiptField, "{3}", PositionField, "{4}", CompanyIDField,"{5}",
                IdField, "{6}");
        }

        public WorkerRepository(string connectionString) {

            this.connectionString = connectionString;
            
        }


        private Worker ToWorker(SqlDataReader sqlData)
        {
            return new Worker()
            {
                ID = (int)sqlData.GetValue(0),
                LastName = (string)sqlData.GetValue(1),
                FirstName = (string)sqlData.GetValue(2),
                Patronymic = (string)sqlData.GetValue(3),
                DateReceipt = DateTime.Parse(sqlData.GetValue(4).ToString()),
                Position = (Position)Enum.ToObject(typeof(Position), sqlData.GetValue(5)),
                CompanyID = (int)sqlData.GetValue(6)
            };
        }

        public Worker Create(Worker item)
        {
            Worker company = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(String.Format(InsertCommand, 
                    item.LastName, item.FirstName,item.Patronymic,item.DateReceipt,(int)item.Position,item.CompanyID),
                    connection);
                command.ExecuteNonQuery();
                command = new SqlCommand(String.Format(SelectByAllFieldCommand,
                    item.LastName, item.FirstName, item.Patronymic, item.DateReceipt, (int)item.Position, item.CompanyID), 
                    connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        company = ToWorker(reader);
                    }
                }
            }
            return company;
        }

        public void Delete(Worker item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(String.Format(DeleteCommand, item.ID), connection);
                command.ExecuteNonQuery();
            }
        }

        public Worker Read(int id)
        {
            Worker worker = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(String.Format(SelectByIdCommand, id), connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        worker = ToWorker(reader);
                    }
                }
            }
            return worker;
        }

        public IEnumerable<Worker> ReadAll()
        {
            List<Worker> workers = new List<Worker>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(SelectAllCommand, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        workers.Add(ToWorker(reader));
                    }
                }
            }
            return workers;
        }

        public Worker Update(Worker item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(String.Format(UpdateCommand,
                    item.LastName, item.FirstName, item.Patronymic, item.DateReceipt, (int)item.Position, item.CompanyID, item.ID), connection);
                command.ExecuteNonQuery();
            }
            return new Worker
            {
                ID = item.ID,
                LastName = item.LastName,
                FirstName = item.FirstName,
                Patronymic= item.Patronymic,
                DateReceipt = item.DateReceipt,
                Position= item.Position,
                CompanyID= item.CompanyID
            };
        }
    }
}
