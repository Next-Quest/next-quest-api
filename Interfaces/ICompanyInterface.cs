using NextQuest.DTOs.CompanyDtos;
using NextQuest.Models;

namespace NextQuest.Interfaces;

public interface ICompanyInterface
{
    public Task<(bool Success, string Message)> CreateCompanyAsync(Company company);
    public Company MapCreateCompanyDtoToCompanyModel(CreateCompanyDto company);
    public Task<(bool Success, string Message, List<CompanyDto>? Companies)> GetAllCompaniesAsync();
    public Task<(bool Success, string Message, CompanyDto? Company)> GetCompanyByIdAsync(int companyId);
    public Task<(bool Success, string Message)> UpdateCompanyAsync(UpdateCompanyDto companyDto);


}