// Contoller to manage catalog items
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Play.Catalog.Service.Dtos;

namespace Play.Catalog.Service.Controllers
{ // derive from ControllerBase class for method and properties to handle Http requests
    [ApiController] // attributes for use of useful methods
    [Route("items")] // attribute url patterns, so if https://localhost:7031/items/ - than this controller will be used to handle requests
    public class ItemsContoller: ControllerBase
    {
        // static list that is used as a database
        private static readonly List<ItemDto> items = new()
        {
            new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount og HP", 5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Antidote", "Cures poison", 7, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Bronze Sword", "Deals a small amount of damage", 20, DateTimeOffset.UtcNow),
        };
        [HttpGet]
        public IEnumerable<ItemDto> Get()
        {
            return items;
        }

        // GET /items/12345 -> {id}
        [HttpGet("{id}")]        
        public ActionResult<ItemDto> GetById(Guid id)
        {
            var item = items.Where(item => item.Id == id).SingleOrDefault();
            // check if an item exists
            if(item == null)
            {
                return NotFound();
            }
            return item;
        }
        // methods returns a type of http status codes: 200(OK), 400(bad request)
        // whoever calls method must follow create item dto contract
        // POST /items
        [HttpPost]
        public ActionResult<ItemDto> Post(CreateItemDto createItemDto)
        {
            var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);
            items.Add(item);

            return CreatedAtAction(nameof(GetById), new {id = item.Id}, item);
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = items.Where(item => item.Id == id).SingleOrDefault();

            if(existingItem == null)
            {
                return NotFound();
            }

            var updateItem = existingItem with {
                Name = updateItemDto.Name,
                Description = updateItemDto.Description,
                Price = updateItemDto.Price
            };

            var index = items.FindIndex(existingItem => existingItem.Id == id);

            items[index] = updateItem;
        // When you call return NoContent(), it returns the StatusCodeResult NoContentResult,
        // translates to calling StatusCode(204)
            return NoContent();
        }

        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == id);
            if(index < 0)
            {
                return NotFound();
            }
            items.RemoveAt(index);
            return NoContent();

        }
    }
}