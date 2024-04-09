namespace RSMEnterpriseIntegrationsAPI.Domain.Interfaces
{
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartments();
        Task<Department?> GetDepartmentById(int id);
        Task<int> CreateDepartment(Department department);
        Task<int> UpdateDepartment(Department department);
        Task<int> DeleteDepartment(Department department);
    }
}
