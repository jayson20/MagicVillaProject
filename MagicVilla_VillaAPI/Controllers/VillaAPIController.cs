using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTOs;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    //[Route("api/controller")]
    [Route("api/VillaAPI")] //hardcode the controller name: So that you wont have to notify the consumer of the endoint if you change the controller name in the future
    [ApiController]
    public class VillaAPIController : Controller
    {
        protected APIResponse _response;
        public readonly ILogger<VillaAPIController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;

        public VillaAPIController(ILogger<VillaAPIController> logger, ApplicationDbContext db, IVillaRepository dbVilla, IMapper imapper)
        {
            this._response = new();
            _logger = logger;
            _db = db;
            _dbVilla = dbVilla;
            _mapper = imapper;
        }


        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            _logger.LogInformation("Getting all villas");

            //IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();

            try
            {
                IEnumerable<Villa> villaList = await _dbVilla.GetAllAsync();
                _response.Result = _mapper.Map<List<VillaDTO>>(villaList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string> { ex.ToString() };
            }
            return _response;
           


            //return Ok(VillaStore.villaList);
        }

        [HttpGet("{id:int}", Name ="GetVilla")]
        //Document the response Types

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        //[ProducesResponseType(200, Type = typeof(VillaDTO))] You can specify the type if the return type is not specified in the action
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {

            try
            {
                var villa = await _dbVilla.GetAsync(u => u.Id == id);
                //var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
                //var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);

                if (id == 0)
                {
                    _logger.LogError("Get villa Error with Id" + id);

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }


                if (villa == null)
                    return NotFound();

                _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string> { ex.ToString() };
            }
            return _response;
           
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody]VillaCreateDTO createDTO)
        {
            try
            {
                // This declaration ([ApiController]) used at the top of the controller makes it possible to for the controller to automatically detect and use data anotations like, [Required], [MaxLength], etc
                // Use ModelState.IsValid if you want to remove [ApiController]

                //if (!ModelState.IsValid)
                //    return BadRequest(ModelState);

                //Custom Validation. Check if villa name exists

                if (await _db.Villas.FirstOrDefaultAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("customError", "Villa already exists!");
                    return BadRequest(ModelState);
                }



                if (createDTO == null)
                    return BadRequest();

                //if (villaDTO.Id > 0)
                //    return StatusCode(StatusCodes.Status500InternalServerError);

                //villaDTO.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;

                //VillaStore.villaList.Add(villaDTO);

                Villa villa = _mapper.Map<Villa>(createDTO);

                //Villa model = new Villa();
                //model.Id = villaDTO.Id;
                //model.Name = createDTO.Name;
                //model.Details = createDTO.Details;
                //model.Amenity = createDTO.Amenity;
                //model.Sqrft = createDTO.Sqrft;
                //model.Occupancy = createDTO.Occupancy;
                //model.Rate = createDTO.Rate;
                //model.ImageUrl = createDTO.ImageUrl;

                //await _db.Villas.AddAsync(model);

                await _dbVilla.CreateAsync(villa);
                _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVilla", new { id = villa.Id }, _response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string> { ex.ToString() };
            }
            return _response;
           

        }


        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest();

                var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);

                if (villa == null)
                    return NotFound();

                //_db.Villas.Remove(villa);
                // await _db.SaveChangesAsync();

                await _dbVilla.RemoveAsync(villa);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string> { ex.ToString() };
            }
            return _response;

        }


        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody]VillaUpdateDTO updateDTO)
        {

            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                    return BadRequest();


                //var villa = _db.Villas?.FirstOrDefault(u => u.Id == id);

                //villa.Name = villaDTO.Name;
                //villa.Sqrft = villaDTO.Sqrft;
                //villa.Occupancy = villaDTO.Sqrft;

                Villa model = _mapper.Map<Villa>(updateDTO);



                //Villa model = new Villa();
                //model.Id = updateDTO.Id;
                //model.Name = updateDTO.Name;
                //model.Details = updateDTO.Details;
                //model.Amenity = updateDTO.Amenity;
                //model.Sqrft = updateDTO.Sqrft;
                //model.Occupancy = updateDTO.Occupancy;
                //model.Rate = updateDTO.Rate;
                //model.ImageUrl = updateDTO.ImageUrl;

                //_db.Villas.Update(model);
                //await _db.SaveChangesAsync();


                await _dbVilla.UpdateAsync(model);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<APIResponse>> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {

            try
            {
                if (patchDTO == null || id == 0)
                    return BadRequest();

                //var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);


                var villa = await _dbVilla.GetAsync(u => u.Id == id, tracked: false);


                VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);

                //VillaUpdateDTO villaDTO = new VillaUpdateDTO();

                //villaDTO.Id = villa.Id;
                //villaDTO.Name = villa.Name;
                //villaDTO.Amenity = villa.Amenity;
                //villaDTO.Details = villa.Details;
                //villaDTO.ImageUrl = villa.ImageUrl;
                //villaDTO.Occupancy = villa.Occupancy;
                //villaDTO.Rate = villa.Rate;
                //villaDTO.Sqrft = villa.Sqrft;

                if (villa == null)
                    return BadRequest();

                patchDTO.ApplyTo(villaDTO, ModelState);

                Villa model = _mapper.Map<Villa>(villaDTO);

                //Villa model = new Villa();
                //model.Id = villaDTO.Id;
                //model.Name = villaDTO.Name;
                //model.Details = villaDTO.Details;
                //model.Amenity = villaDTO.Amenity;
                //model.Sqrft = villaDTO.Sqrft;
                //model.Occupancy = villaDTO.Occupancy;
                //model.Rate = villaDTO.Rate;
                //model.ImageUrl = villaDTO.ImageUrl;

                //patchDTO.ApplyTo(villaDTO, ModelState);

                //_db.Villas.Update(model);
                //await _db.SaveChangesAsync();

                await _dbVilla.UpdateAsync(model);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string> { ex.ToString() };
            }
            return _response;
        }
    }
}
