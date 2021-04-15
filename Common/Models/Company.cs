using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Models
{
    [Table("companies")]
    public class Company
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int ID { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("OrganizationalForm")]
        public OrganizationalForm OrganizationalForm {get;set;}
    }
}
