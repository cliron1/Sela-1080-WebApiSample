using System.ComponentModel.DataAnnotations;

namespace ApiSample.Models;

public class Pet : PetForSave {
    public int Id { get; set; }
}

public class PetForSave {
    [Required, MinLength(2)]
    public string Name { get; set; }

    public string Nickname { get; set; }

    [Required]
    public string Type { get; set; }

    [Required]
    public float Age { get; set; }
}

public class PetDTO {
    public PetDTO() {
    }
    public static PetDTO Create(Pet item) {
        return new PetDTO {
            Id = item.Id,
            Nickname = item.Nickname,
            Type = item.Type,
            Age = item.Age
        };
    }

    public int Id { get; set; }
    public string Nickname { get; set; }
    public string Type { get; set; }
    public float Age { get; set; }

    public Pet ToEntity() {
        return new Pet {
            Id = this.Id,
            Nickname = this.Nickname,
            Type = this.Type,
            Age = this.Age
        };
    }
}