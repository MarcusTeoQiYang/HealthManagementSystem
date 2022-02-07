using MZHealthManagement.Shared.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MZHealthManagement.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        IGenericRepository<Staff> Staffs { get; }
        IGenericRepository<Appointment> Appointments { get; }
        IGenericRepository<Department> Departments { get; }
        IGenericRepository<Diagnosis> Diagnosis { get; }
        IGenericRepository<Prescription> Prescriptions { get; }
        IGenericRepository<Patient> Patients { get; }
    }
}