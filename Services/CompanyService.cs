using Microsoft.EntityFrameworkCore;
using NextQuest.Data;
using NextQuest.DTOs.CompanyDtos;
using NextQuest.Interfaces;
using NextQuest.Models;

namespace NextQuest.Services;

public class CompanyService : ICompanyInterface
{
    private readonly AppDbContext _context;

    public CompanyService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<(bool Success, string Message)> CreateCompanyAsync(Company company)
    {
        try
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
        
            return (true, "Empresa criada com sucesso.");
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool Success, string Message, List<CompanyDto>? Companies)> GetAllCompaniesAsync()
    {
        try
        {
            var response = await _context.Companies.ToListAsync();

            if (response.Count == 0)
            {
                return (true, "Nenhuma empresa encontrada.", null);
            }

            var companies = MapCompanyModelToCompanyDtoList(response);
            
            return (true, "Empresas encontradas com sucesso.", companies);
        }
        catch (Exception e)
        {
            return(false, e.Message, null);
        }
    }

    public async Task<(bool Success, string Message, CompanyDto? Company)> GetCompanyByIdAsync(int companyId)
    {
        try
        {
            var response = await _context.Companies.FindAsync(companyId);

            if (response == null)
            {
                return (true, "Nenhum empresa encontrada.", null);
            }

            var company = MapCompanyModelToCompanyDto(response);

            return (true, "Empresa encontrada com sucesso.", company);
        }
        catch (Exception e)
        {
            return (false, e.Message, null);
        }
    }
    
    #region DtoMapping
    public Company MapCreateCompanyDtoToCompanyModel(CreateCompanyDto company)
    {
        return new Company()
        {
            Name = company.Name
        };
    }

    private CompanyDto MapCompanyModelToCompanyDto(Company company)
    {
        return new CompanyDto()
        {
            Id = company.Id,
            Name = company.Name
        };
    }
    
    private List<CompanyDto> MapCompanyModelToCompanyDtoList(List<Company> companies)
    {
        var companyDtoList = new List<CompanyDto>();

        foreach (var company in companies)
        {
            companyDtoList.Add(MapCompanyModelToCompanyDto(company));
        }
        
        return companyDtoList;
    }
    #endregion
}