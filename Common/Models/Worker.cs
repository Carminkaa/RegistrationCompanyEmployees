using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Models
{

    [Table("workers")]
    public class Worker
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int ID { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("patronymic")]
        public string Patronymic { get; set; }
        [Column("date_receipt")]
        public DateTime DateReceipt { get; set; }
        [Column("position")]
        public Position Position { get; set; }
        [Column("company_id")]
        public int CompanyID { get; set; }
        public Company Company { get; set; }
    }
}
