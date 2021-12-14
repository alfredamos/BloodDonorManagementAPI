using AutoMapper;
using BloodDonorDataAccessEF.Contracts;
using BloodDonorModels.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BloodDonorManagementAPI.Controllers.Donors
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorsController : ControllerBase
    {
        private readonly IDonorDataRepository _repository;
        private readonly IMapper _mapper;

        public DonorsController(IDonorDataRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/<DonorsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonorData>>> GetAllDonors()
        {
            try
            {
                return Ok(await _repository.GetAll());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // GET api/<DonorsController>/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DonorData>> GetDonor(int id)
        {
            try
            {
                var donor = await _repository.GetById(id);

                if (donor == null)
                {
                    return NotFound($"Donor with Id : {id} not found.");
                }

                return Ok(donor);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // POST api/<DonorsController>
        [HttpPost]
        public async Task<ActionResult<DonorData>> PostDonor(DonorData donor)
        {
            try
            {
                if (donor == null)
                {
                    return BadRequest("Invalid input");
                }

                var createdDonor = await _repository.Create(donor);

                return CreatedAtAction(nameof(GetDonor), new { Id = createdDonor.Id }, createdDonor);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data.");
            }
        }

        // PUT api/<DonorsController>/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<DonorData>> PutDonor(int id, DonorData donor)
        {
            try
            {
                if (id != donor.Id)
                {
                    return BadRequest("Id mismatch");
                }

                var donorToUpdate = await _repository.GetById(id);

                if (donorToUpdate == null)
                {
                    return NotFound($"Donor with Id : {id} not found.");
                }

                _mapper.Map(donor, donorToUpdate);

                return await _repository.Update(donorToUpdate);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data.");
            }
        }

        // DELETE api/<DonorsController>/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<DonorData>> DeleteDonor(int id)
        {
            try
            {
                var donorToDelete = await _repository.GetById(id);

                if (donorToDelete == null)
                {
                    return NotFound($"Donor with Id : {id} not found.");
                }

                return await _repository.Delete(id);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data.");
            }
            
        }
    }
}
