using System.Collections.Generic;
using System.Threading.Tasks;
using CxcProject.Models;

namespace CxcProject.Interfaces
{
    public interface IContabilidadService
    {
        Task<bool> EnviarAsientoContableAsync(AsientoContable asiento);
    }
}