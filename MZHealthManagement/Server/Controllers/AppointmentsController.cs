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
    public class AppointmentsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<IActionResult> GetAppointment()
        {
            //return await _context.Patients.ToListAsync();
            var appointments = await _unitOfWork.Appointments.GetAll(includes: q => q.Include(x => x.Patient).Include(x => x.Staff));
            return Ok(appointments);
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            var appointment = await _unitOfWork.Appointments.Get(q => q.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment);
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Appointments.Update(appointment);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AppointmentExists(id))
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

        // POST: api/Patients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
            //_context.Patients.Add(patient);
            //await _context.SaveChangesAsync();

            await _unitOfWork.Appointments.Insert(appointment);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetPatient", new { id = appointment.Id }, appointment);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            //var patient = await _context.Patients.FindAsync(id);
            var appointment = await _unitOfWork.Appointments.Get(q => q.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            //_context.Patients.Remove(patient);
            //await _context.SaveChangesAsync();

            await _unitOfWork.Appointments.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> AppointmentExists(int id)
        {
            var appointment = await _unitOfWork.Appointments.Get(q => q.Id == id);
            return appointment != null;
        }
    }
}
