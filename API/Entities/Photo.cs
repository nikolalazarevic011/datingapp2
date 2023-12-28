using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

[Table("Photos")] // da primenujes kako se z tabela u bazi
public class Photo
{
    public int Id { get; set; }
    public string Url { get; set; }
    public bool IsMain { get; set; }
    public string PublicId { get; set; }


    //added for better relationship btw AppUser and photos, so the user ID 
    //can't be nullable, i da ce se se photos obrisati kad obrisemo usera
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}
