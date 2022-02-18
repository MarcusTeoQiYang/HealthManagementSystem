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
    public class DiagnosesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DiagnosesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Diagnoses
        [HttpGet]
        public async Task<ActionResult> GetDiagnosis()
        {
            var diagnosis = await _unitOfWork.Diagnosis.GetAll(includes: q => q.Include(x => x.Appointment));
            return Ok(diagnosis);
        }

        // GET: api/Diagnoses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiagnosis(int id)
        {
            var diagnosis = await _unitOfWork.Diagnosis.Get(q => q.Id == id);

            if (diagnosis == null)
            {
                return NotFound();
            }

            return Ok(diagnosis);
        }

        // PUT: api/Diagnoses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiagnosis(int id, Diagnosis diagnosis)
        {
            if (id != diagnosis.Id)
            {
                return BadRequest();
            }
            _unitOfWork.Diagnosis.Update(diagnosis);
            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DiagnosisExists(id))
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

        // POST: api/Diagnoses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Diagnosis>> PostDiagnosis(Diagnosis diagnosis)
        {
            await _unitOfWork.Diagnosis.Insert(diagnosis);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetDiagnosis", new { id = diagnosis.Id }, diagnosis);
        }

        // DELETE: api/Diagnoses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiagnosis(int id)
        {
            var diagnosis = await _unitOfWork.Diagnosis.Get(q => q.Id == id);
            if (diagnosis == null)
            {
                return NotFound();
            }
            await _unitOfWork.Diagnosis.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

             private async Task<bool> DiagnosisExists(int id)
        {
            var diagnosis = await _unitOfWork.Diagnosis.Get(q => q.Id == id);
            return diagnosis != null;
        }
    }
}
