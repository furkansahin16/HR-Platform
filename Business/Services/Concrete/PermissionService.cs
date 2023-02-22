using AutoMapper;
using Business.Services.Abstract;
using Business.SystemResult;
using Business.ViewModels.PermissionVM;
using Core.Entites.Concrete;
using Core.Enums;
using DataAccess.EfRepository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.Concrete
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepo _repo;
        private readonly IMapper _mapper;

        public PermissionService(IPermissionRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Parametre olarak belirtilen şirket çalışanlarının oluşturduğu, bekliyor durumunda olan izin taleplerini getirir.
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public ResultService<List<PermissionListItem>> GetPendingPermissionsRequestViaCompany(string companyName)
        {
            ResultService<List<PermissionListItem>> result = new();

            try
            {
                var permissionList = _repo.GetAll(
                    x => x.Personnel.CompanyInfo == companyName 
                    && x.Status == StatusTypes.Pending
                    && x.Personnel.IsActive,
                    x => x.Personnel)
                    .AsNoTracking()
                    .OrderBy(x => x.RequestTime)
                    .ToList();

                var model = _mapper.Map<List<PermissionListItem>>(permissionList);

                result.ResultData = model;

                result.AddSuccess("Talep listesi başarılı bir şekilde yüklendi.");
            }
            catch (Exception)
            {
                result.AddError("Talepler yüklenirken bir hata oluştu.");
            }

            return result;
        }

        /// <summary>
        /// Parametre olarak belirtilen şirket çalışanlarının oluşturduğu ve yönetici tarafından yanıt verilen izin taleplerinin getirir.
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public ResultService<List<PermissionListItem>> GetDonePermissionsRequestViaCompany(string companyName)
        {
            ResultService<List<PermissionListItem>> result = new();

            try
            {
                var permissionList = _repo
                    .GetAll(
                    x => x.Personnel.CompanyInfo == companyName
                    && (x.Status == StatusTypes.Approved || x.Status == StatusTypes.Rejected)
                    && x.Personnel.IsActive,
                    x => x.Personnel)
                    .AsNoTracking()
                    .OrderBy(x => x.RequestTime)
                    .ToList();

                var model = _mapper.Map<List<PermissionListItem>>(permissionList);

                result.ResultData = model;

                result.AddSuccess("Talep listesi başarılı bir şekilde yüklendi.");
            }
            catch (Exception)
            {
                result.AddError("Talepler yüklenirken bir hata oluştu.");
            }

            return result;
        }

        /// <summary>
        /// Belirtilen kullanıcı mail adresine sahip çalışanın tüm izin taleplerini getirir.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ResultService<List<PermissionListItem>> GetPersonnelsPermissions(string userName)
        {
            ResultService<List<PermissionListItem>> result = new();

            try
            {
                var permissionList = _repo.GetAll(x => x.Personnel.UserName == userName, x => x.Personnel).AsNoTracking().OrderBy(x => x.RequestTime).ToList();

                var model = _mapper.Map<List<PermissionListItem>>(permissionList);

                result.ResultData = model;

                result.AddSuccess("İzin talebi listesi başarılı bir şekilde yüklendi.");
            }
            catch (Exception)
            {
                result.AddError("Talepler yüklenirken bir hata oluştu.");
            }

            return result;
        }

        /// <summary>
        /// Id'si belirtilen izin talebini onaylar.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResultService<Permission>> ApprovePermissionAsync(Guid id)
        {
            ResultService<Permission> result = new();

            try
            {
                var permission = await _repo.GetAsync(x => x.Id == id);

                result.ResultData = permission;

                permission.Status = StatusTypes.Approved;

                permission.ResponseTime = DateTime.Now;

                await _repo.UpdateAsync(permission);

                result.AddSuccess("Talep başarılı bir şekilde onaylandı");
            }
            catch (Exception)
            {
                result.AddError("Talep onaylanırken bir hata oluştu");
            }

            return result;
        }

        /// <summary>
        /// Id'si belirtilen izin talebini onaylamaz.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResultService<Permission>> RejectPermissionRequest(Guid id)
        {
            ResultService<Permission> result = new();

            try
            {
                var permission = await _repo.GetAsync(x => x.Id == id);

                result.ResultData = permission;

                permission.Status = StatusTypes.Rejected;

                permission.ResponseTime = DateTime.Now;

                await _repo.UpdateAsync(permission);

                result.AddSuccess("Talep başarılı bir şekilde iptal edildi");
            }
            catch (Exception)
            {
                result.AddError("İşlem sırasında bir hata oluştu");
            }

            return result;
        }

        /// <summary>
        /// Id'si belirtilen izin talebini iptal eder.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResultService<Permission>> CancelPermissionAsync(Guid id)
        {
            ResultService<Permission> result = new();

            try
            {
                var permission = await _repo.GetAsync(x => x.Id == id);

                result.ResultData = permission;

                permission.Status = StatusTypes.Canceled;

                await _repo.UpdateAsync(permission);

                result.AddSuccess("Talep başarılı bir şekilde iptal edildi");
            }
            catch (Exception)
            {
                result.AddError("Talep iptal edilirken bir hata oluştu");
            }

            return result;
        }

        /// <summary>
        /// Model ve kullanıcı bilgileri alınarak, o kullanıcıya ait bir izin talebi oluşturur.
        /// </summary>
        /// <param name="permissionCreate"></param>
        /// <param name="personnel"></param>
        /// <returns></returns>
        public async Task<ResultService<PermissionCreate>> CreateNewPermissionRequestAsync(PermissionCreate permissionCreate, Employee personnel)
        {
            ResultService<PermissionCreate> result = new();

            result.ResultData = permissionCreate;

            Permission permission = _mapper.Map<Permission>(permissionCreate);
            permission.Personnel = personnel;

            try
            {
                CheckPermissionDay(permission.PermissionTypes, personnel.AnnualPermission, permission.Day);
                CheckPermissionValid(permission.PermissionTypes, personnel, permission.StartTime);

                await _repo.AddAsync(permission);

                result.AddSuccess("İzin talebi başarılı bir şekilde gönderildi.");
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// İzin türlerinin o yıl içerisinde kullanıp kullanılmadığını kontrol eder.
        /// </summary>
        /// <param name="permissionTypes"></param>
        /// <param name="personnel"></param>
        /// <param name="permissionTime"></param>
        /// <exception cref="Exception"></exception>
        private void CheckPermissionValid(PermissionTypes permissionTypes, Employee personnel, DateTime permissionTime)
        {
            if ((permissionTypes == PermissionTypes.Evlilik
                || permissionTypes == PermissionTypes.Babalık
                || permissionTypes == PermissionTypes.Hamilelik)
                && permissionTime.Year == DateTime.Now.Year)
            {
                var result = GetPersonnelsPermissions(personnel.UserName).ResultData
                .Where(x => x.Status == StatusTypes.Approved.ToString() && x.PermissonType == permissionTypes.ToString())
                .ToList();

                if (result.Count != 0) throw new Exception("Bu izin türü yıl içerisinde kullanılmış. Tekrar telep edilemez.");
            }
        }

        /// <summary>
        /// Çalışanın izin talebindeki gün sayısını, çalışanın sahip olduğu izin hakkına göre kontrol eder.
        /// </summary>
        /// <param name="permissionTypes"></param>
        /// <param name="currentDay"></param>
        /// <param name="day"></param>
        /// <exception cref="Exception"></exception>
        private void CheckPermissionDay(PermissionTypes permissionTypes, int currentDay, int day)
        {
            if (day == 0) throw new Exception("Talep ettiğiniz izin en az 1 iş günü içermelidir.");
            switch (permissionTypes)
            {
                case PermissionTypes.Yıllık:
                    if (day > currentDay)
                        throw new Exception($"{day} gün için yıllık izin hakkınız bulunmamaktadır." +
                           $"Yıllık izin hakkınız en fazla : {currentDay} gündür.");
                    else if (day > 20)
                        throw new Exception($"Tek seferde 20 günden fazla izin kullanılamaz.");
                    break;
                case PermissionTypes.Evlilik:
                    if (day > 3)
                        throw new Exception($"3 Günden fazla evlilik izni kullanılamaz.");
                    break;
                case PermissionTypes.Babalık:
                    if (day > 5)
                        throw new Exception($"5 Günden fazla babalık izni kullanılamaz.");
                    break;
                case PermissionTypes.Hamilelik:
                    if (day > 80)
                        throw new Exception($"80 Günden fazla doğum izni kullanılamaz.");
                    break;
                case PermissionTypes.Cenaze:
                    if (day > 3)
                        throw new Exception($"3 Günden fazla doğum izni kullanılamaz.");
                    break;
                default:
                    break;
            }
        }
    }
}
