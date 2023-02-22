using Business.SystemResult;
using Business.ViewModels.PermissionVM;
using Core.Entites.Concrete;
using Core.Enums;

namespace Business.Services.Abstract
{
    public interface IPermissionService
    {
        ResultService<List<PermissionListItem>> GetPendingPermissionsRequestViaCompany(string companyName);

        ResultService<List<PermissionListItem>> GetDonePermissionsRequestViaCompany(string companyName);

        Task<ResultService<Permission>> ApprovePermissionAsync(Guid id);

        Task<ResultService<Permission>> RejectPermissionRequest(Guid id);

        ResultService<List<PermissionListItem>> GetPersonnelsPermissions(string userName);

        Task<ResultService<Permission>> CancelPermissionAsync(Guid id);

        Task<ResultService<PermissionCreate>> CreateNewPermissionRequestAsync(PermissionCreate permissionCreate,Employee personnel);
    }
}
