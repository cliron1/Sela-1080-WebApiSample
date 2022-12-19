using ApiSample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace ApiSample.Controllers;

[ApiController]
[Route("pets")]
//[Authorize]
public class PetsController : ControllerBase {
    public PetsController() {
    }

    /// <summary>Return all pets</summary>
    /// <returns>List of Pet model</returns>
    // GET /pets
    [HttpGet]
    public List<Pet> Index() {
        //HttpContext.User.Identity.IsAuthenticated

        var claims = HttpContext.User.Claims;
        var userID = int.Parse(claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
        //var username = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        var username = HttpContext.User.Identity.Name;

        var d = DateTimeOffset.FromUnixTimeSeconds(1671020848).DateTime;

        return new List<Pet> {
            new Pet { Name=$"Tofu and other pets of user: {username} #{userID}"}
        };
    }

    /// <summary> Get a single Pet by its given ID </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    // GET /pets/1
    public PetDTO Get([FromRoute] int id) {
        var petFromDB = new Pet { Id = id, Name = "Tofu" };
        return PetDTO.Create(petFromDB);
    }

    [HttpGet("search")]
    // GET /pets/search?q=mitsy
    public List<Pet> Search([FromQuery(Name = "q")] string search) {
        return new List<Pet> {
            new Pet { Name = $"all pets like {search}*" }
        };
    }

    [HttpPost]
    // POST /pets
    public int Add([FromBody] PetForSave model) {
        // Save the model to the DB
        // and return the created entity ID
        return 77;
    }

    [HttpPut]
    // PUT /pets
    public StatusCodeResult Update([FromBody] Pet model) {
        // Update the DB of ID = model.Id with the model
        return StatusCode(200);
    }

    /*
    [HttpPut("{id}")]
    // PUT /pets/1
    public void Update([FromRoute] int id, [FromBody] PetDTO model) {
        var petForDB = model.ToEntity();
        // Update the DB of ID = id with the model
    }
    */

    [HttpDelete("{id:int}")]
    // DELETE /pets/3
    public void Delete(int id) {
        // Update the model to the DB
    }

}

