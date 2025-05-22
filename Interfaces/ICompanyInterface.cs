using NextQuest.DTOs.CompanyDtos;
using NextQuest.Models;

namespace NextQuest.Interfaces;

public interface ICompanyInterface
{
    public Task<(bool Success, string Message)> CreateCompanyAsync(Company company);
    public Task<(bool Success, string Message, List<CompanyDto>? Companies)> GetAllCompaniesAsync();
    public Task<(bool Success, string Message, CompanyDto? Company)> GetCompanyByIdAsync(int companyId);
    public Task<(bool Success, string Message)> UpdateCompanyAsync(UpdateCompanyDto companyDto);
    public Task<(bool Success, string Message)> DeleteCompanyAsync(int companyId);
    
    #region DtoMapping
    public Company MapCreateCompanyDtoToCompanyModel(CreateCompanyDto company);
    #endregion
}