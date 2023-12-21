using LAB_PROJECT.Data;
using LAB_PROJECT.Models;
using LAB_PROJECT.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class AddressesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AddressesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Addresses
    [HttpGet]
    public ActionResult<IEnumerable<Address>> GetAddresses()
    {
        return _context.Addresses.ToList();
    }

    // GET: api/Addresses/5
    [HttpGet("{id}")]
    public ActionResult<Address> GetAddress(int id)
    {
        var address = _context.Addresses.Find(id);

        if (address == null)
        {
            return NotFound();
        }

        return address;
    }

    // POST: api/Addresses
    [HttpPost]
    public ActionResult<AddressResponseDTO> PostAddress(Address address)
    {
        _context.Addresses.Add(address);
        _context.SaveChanges();
        var dto = new AddressResponseDTO
        {
            Id = address.Id,
            Name = address.Name,
            UserId = address.UserId,
        };

        return CreatedAtAction("GetAddress", new { id = address.Id },dto);
    }

    // PUT: api/Addresses/5
    [HttpPut("{id}")]
    public IActionResult PutAddress(int id, Address address)
    {
        if (id != address.Id)
        {
            return BadRequest();
        }

        _context.Entry(address).State = EntityState.Modified;
        _context.SaveChanges();

        return NoContent();
    }

    // DELETE: api/Addresses/5
    [HttpDelete("{id}")]
    public IActionResult DeleteAddress(int id)
    {
        var address = _context.Addresses.Find(id);

        if (address == null)
        {
            return NotFound();
        }

        _context.Addresses.Remove(address);
        _context.SaveChanges();

        return NoContent();
    }
}
