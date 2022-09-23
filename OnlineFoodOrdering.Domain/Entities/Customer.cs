using OnlineFoodOrdering.Domain.Common;
using OnlineFoodOrdering.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodOrdering.Domain.Entities
{
    public class Customer : Auditable
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = String.Empty;
        public DateOnly BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;

    }
}
