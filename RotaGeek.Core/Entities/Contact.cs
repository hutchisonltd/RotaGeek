using RotaGeek.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RotaGeek.Core.Entities
{
    public class Contact : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Message { get; set; }
        public DateTime EnteredDate { get; set; }
    }
}
