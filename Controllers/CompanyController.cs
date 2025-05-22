using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextQuest.DTOs.CompanyDtos;
using NextQuest.Interfaces;
using NextQuest.Models;

namespace NextQuest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyInterface _companyInterface;
    private readonly IUserInterface _userInterface;

    public CompanyController(
        ICompanyInterface companyInterface,
        IUserInterface userInterface)
    {
        _companyInterface = companyInterface;
        _userInterface = userInterface;
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateCompanyDto request)
    {
        var tokenId = User.FindFirst("id")?.Value;
        if (!int.TryParse(tokenId, out var userId))
            return Unauthorized();

        var isAdmin = await _userInterface.IsAdminAsync(userId); 
        
        if (!isAdmin.Success)
        {
            return Unauthorized();
        }

        var company = _companyInterface.MapCreateCompanyDtoToCompanyModel(request);

        var response  = await _companyInterface.CreateCompanyAsync(company);

        if (!response.Success)
        {
            return BadRequest(response.Message);
        }
            
        return Ok(response.Message);
    }

    [HttpGet("listAll")]
    public async Task<IActionResult> List()
    {
        var response = await _companyInterface.GetAllCompaniesAsync();
        
        if (!response.Success)
        {
            return BadRequest(response.Message);
        }
            
        return Ok(new
        {
            Message = response.Message,
            Companies = response.Companies
        });
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetCompanyById(int id)
    {
        var response = await _companyInterface.GetCompanyByIdAsync(id);

        if (!response.Success)
        {
            return BadRequest(response.Message);
        }

        if (response.Company is null)
        {
            return Ok(response.Message);
        }
            
        return Ok(new
        {
            Message = response.Message,
            Company = response.Company
        });
    }
}