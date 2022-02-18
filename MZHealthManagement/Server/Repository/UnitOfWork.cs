using MZHealthManagement.Server.Data;
using MZHealthManagement.Server.IRepository;
using MZHealthManagement.Server.Models;
using MZHealthManagement.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MZHealthManagement.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Staff> _Staffs;
        private IGenericRepository<Appointment> _Appointments;
        private IGenericRepository<Department> _Departments;
        private IGenericRepository<Diagnosis> _Diagnosis;
        private IGenericRepository<Prescription> _Prescriptions;
        private IGenericRepository<Patient> _Patients;

        private UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IGenericRepository<Staff> Staffs
            => _Staffs ??= new GenericRepository<Staff>(_context);
        public IGenericRepository<Appointment> Appointments
            => _Appointments ??= new GenericRepository<Appointment>(_context);
        public IGenericRepository<Department> Departments
            => _Departments ??= new GenericRepository<Department>(_context);
        public IGenericRepository<Diagnosis> Diagnosis
            => _Diagnosis ??= new GenericRepository<Diagnosis>(_context);
        public IGenericRepository<Prescription> Prescriptions
            => _Prescriptions ??= new GenericRepository<Prescription>(_context);
        public IGenericRepository<Patient> Patients
            => _Patients ??= new GenericRepository<Patient>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(HttpContext httpContext)
        {
            //To be implemented
            //string user = "System";
            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);

            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);
            await _context.SaveChangesAsync();
        }
    }
}