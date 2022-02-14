using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MZHealthManagement.Server.Data;
using MZHealthManagement.Server.IRepository;
using MZHealthManagement.Shared.Domain;

namespace MZHealthManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PrescriptionsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Prescriptions
        [HttpGet]
        public async Task<IActionResult> GetPrescriptions()
        {
            var prescriptions = await _unitOfWork.Prescriptions.GetAll(includes: q => q.Include(x => x.Diagnosis));
            return Ok(prescriptions);
        }

        // GET: api/Prescriptions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrescription(int id) 
        {
            var prescription = await _unitOfWork.Prescriptions.Get(q => q.Id == id);
            if (prescription == null)
            {
                return NotFound();
            }

            return Ok(prescription);
        }

        // PUT: api/Prescriptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrescription(int id, Prescription prescription)
        {
            if (id != prescription.Id)
            {
                return BadRequest();
            }
            _unitOfWork.Prescriptions.Update(prescription);
            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PrescriptionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Prescriptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Prescription>> PostPrescription(Prescription prescription)
        {
            await _unitOfWork.Prescriptions.Insert(prescription);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetPrescription", new { id = prescription.Id }, prescription);
        }

        // DELETE: api/Prescriptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            var prescription = await _unitOfWork.Prescriptions.Get(q => q.Id == id);
            if (prescription == null)
            {
                return NotFound();
            }
            await _unitOfWork.Prescriptions.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }
        private async Task<bool> PrescriptionExists(int id)
        {
            var prescription = await _unitOfWork.Prescriptions.Get(q => q.Id == id);
            return prescription != null;
        }
    }
}
