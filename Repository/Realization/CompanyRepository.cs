using Common.Helpers;
using Common.Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Text;

namespace Repository.Realization
{
    public class CompanyRepository : IRepository<Company>
    {
        private readonly string connectionString;
        private readonly static string TableName;
        private readonly static string IdField;
        private readonly static string TitleField;
        private readonly static string OrganizationalFormField;
        private readonly static string SelectAllCommand;
        private readonly static string SelectByIdCommand;
        private readonly static string SelectByAllFieldCommand;
        private readonly static string DeleteCommand;
        private readonly static string InsertCommand;
        private readonly static string UpdateCommand;


        static CompanyRepository()
        {
            TableName = AttributeHelper.GetAttribute<Company, TableAttribute>().Name;
            IdField = AttributeHelper.GetAttribute<Company, ColumnAttribute>(nameof(Company.ID)).Name;
            TitleField = AttributeHelper.GetAttribute<Company, ColumnAttribute>(nameof(Company.Title)).Name;
            OrganizationalFormField = AttributeHelper.GetAttribute<Company, ColumnAttribute>(nameof(Company.OrganizationalForm)).Name;
            SelectAllCommand = String.Format("select {0}, {1}, {2} from {3}", IdField,TitleField,OrganizationalFormField,TableName);
            SelectByIdCommand = String.Format("{0} where {1}={2}", SelectAllCommand, IdField, "{0}");
            SelectByAllFieldCommand = String.Format("{0} where {1}='{2}' and {3}='{4}'", SelectAllCommand, TitleField, "{0}", OrganizationalFormField, "{1}");
            DeleteCommand = String.Format("DELETE FROM {0} WHERE {1}='{2}'", TableName, IdField, "{0}");
            InsertCommand = String.Format("INSERT INTO {0} ({1},{2}) values ('{3}', '{4}')", TableName, TitleField, OrganizationalFormField, "{0}", "{1}");
            UpdateCommand = String.Format("UPDATE {0} SET {1}='{2}', {3}='{4}' where {5}='{6}'", TableName, TitleField, "{0}", OrganizationalFormField, "{1}", IdField, "{2}");
        }
        public CompanyRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }


        private Company ToCompany(SqlDataReader sqlData) {
            return new Company()
            {
                ID = (int)sqlData.GetValue(0),
                Title = (string)sqlData.GetValue(1),
                OrganizationalForm = (OrganizationalForm)Enum.ToObject(typeof(OrganizationalForm), sqlData.GetValue(2))
            };
        }
        public Company Create(Company item)
        { 
            Company company=null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(String.Format(InsertCommand, item.Title,(int)item.OrganizationalForm), connection);
                command.ExecuteNonQuery();
                command = new SqlCommand(String.Format(SelectByAllFieldCommand, item.Title,(int)item.OrganizationalForm), connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        company = ToCompany(reader);
                    }
                }
            }
            return company;
        }

        public void Delete(Company item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(String.Format(DeleteCommand,item.ID), connection);
                command.ExecuteNonQuery();
            }
        }

        public Company Read(int id)
        {
            Company company=null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(String.Format(SelectByIdCommand,id), connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        company = ToCompany(reader);
                    }
                }
            }
            return company;
        }  

        public IEnumerable<Company> ReadAll()
        {
            List<Company> company = new List<Company>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(SelectAllCommand, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        company.Add(ToCompany(reader));
                    }
                }
            }
            return company;
        }

        public Company Update(Company item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(String.Format(UpdateCommand, item.Title,(int)item.OrganizationalForm,item.ID), connection);
                command.ExecuteNonQuery();
            }
            return new Company
            {
                ID = item.ID,
                OrganizationalForm = item.OrganizationalForm,
                Title = item.Title
            };
        }
    }
}
