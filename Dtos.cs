using System;
using System.ComponentModel.DataAnnotations;

namespace Play.Catalog.Service.Dtos
{
    // Dtos  - objects that carry data between process
    public record ItemDto(Guid Id, string Name, string Description, decimal Price, DateTimeOffset CreatedDate);
    // [Required] is used to check if the name is not null
    public record CreateItemDto([Required] string Name, string Description,[Range(0, 1000)] decimal Price);

    public record UpdateItemDto([Required] string Name, string Description,[Range(0, 1000)] decimal Price);

}
