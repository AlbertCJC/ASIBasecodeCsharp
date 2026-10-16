using ASI.Basecode.Services.ViewModelClasses;
using System.Collections.Generic;

namespace ASI.Basecode.Services.Interfaces
{
    public interface IReturnProcessingService
    {
        ReturnsIndexViewModel GetReturnsIndex(string search, int page, int pageSize);

        ReturnFormContextViewModel GetReturnFormContext(long borrowingId);

        List<LookupViewModel> GetConditionOptions();

        List<LookupViewModel> GetInspectionStatusOptions();

        (bool Success, string Error, long ReturnId) CreateReturn(ReturnFormViewModel form);

        (bool Success, string Error, long InspectionId) CreateInspection(InspectionFormViewModel form, IEnumerable<Microsoft.AspNetCore.Http.IFormFile> photos);
    }
}
