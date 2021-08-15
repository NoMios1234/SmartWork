using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartWork.Core.Entities;
using SmartWork.Core.Specifications;
using SmartWork.Core.ViewModels.CompanyViewModels;
using SmartWork.Core.ViewModels.OfficeViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartWork.Core.Abstractions.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyViewModel>> GetCompaniesAsync(Specification<Company> specification);
        Task<IEnumerable<OfficeViewModel>> GetCompanyOfficesAsync(int id);
        Task<ActionResult> AddCompanyAsync(AddCompanyViewModel model);
        Task<ActionResult> UpdateCompanyAsync(UpdateCompanyViewModel model);
        Task<ActionResult> DeleteCompanyAsync(int id);
        string SaveFile(HttpRequest request);
    }
}
