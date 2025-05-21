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

    public Company MapCreateCompanyDtoToCompanyModel(CreateCompanyDto company)
    {
        return new Company()
        {
            Name = company.Name
        };
    }
}