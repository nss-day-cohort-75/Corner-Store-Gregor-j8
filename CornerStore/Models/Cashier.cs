namespace CornerStore.Models;

using System.ComponentModel.DataAnnotations;

public class Cashier
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }
}