// Contoller to manage catalog items
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Repositories;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Controllers
{ // derive from ControllerBase class for method and properties to handle Http requests
    [ApiController] // attributes for use of useful methods
    [Route("items")] // attribute url patterns, so if https://localhost:7031/items/ - than this controller will be used to handle requests
    public class ItemsContoller: ControllerBase
    {
       private readonly ItemRepository itemRepository = new();
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAsync()
        {
            var items = (await itemRepository.GetAllAsync())
                        .Select(item => item.AsDto());

            return items;
        }

        // GET /items/12345 -> {id}
        [HttpGet("{id}")]        
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            var item = await itemRepository.GetAsync(id);
            // check if an item exists
            if(item == null)
            {
                return NotFound();
            }
            return item.AsDto();
        }
        // methods returns a type of http status codes: 200(OK), 400(bad request)
        // whoever calls method must follow create item dto contract
        // POST /items
        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
        {
            // var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);
            // items.Add(item);
            var item = new Item
            {
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow

            };
            await itemRepository.CreateAsync(item);
            return CreatedAtAction(nameof(GetByIdAsync), new {id = item.Id}, item);
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = await itemRepository.GetAsync(id);
            if(existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = updateItemDto.Name;
            existingItem.Description = updateItemDto.Description;
            existingItem.Price = updateItemDto.Price;

            await itemRepository.UpdateAsync(existingItem);
        // When you call return NoContent(), it returns the StatusCodeResult NoContentResult,
        // translates to calling StatusCode(204)
            return NoContent();
        }

        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await itemRepository.GetAsync(id);
            if(item == null)
            {
                return NotFound();
            }

            await itemRepository.RemoveAsync(item.Id);
            return NoContent();

        }
    }
}