using NextQuest.DTOs.CompanyDtos;
using NextQuest.Models;

namespace NextQuest.Interfaces;

public interface ICompanyInterface
{
    public Task<(bool Success, string Message)> CreateCompanyAsync(Company company);
    public Company MapCreateCompanyDtoToCompanyModel(CreateCompanyDto company);
    public Task<(bool Success, string Message, List<CompanyDto>? Companies)> GetAllCompaniesAsync();

}